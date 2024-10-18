using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi_SY.Entity;

namespace WebApi_SY.Models
{
    public class InsertDataToTables
    {
        public async Task InsertData(YourDbContext context, sli_sale_taxture taxture, List<sli_sale_taxturebill> taxturebill, List<sli_sale_taxturebillEntry> taxturebillEntry)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Sli_sale_taxture.Add(taxture);
                    await context.SaveChangesAsync();

                    taxturebill.ForEach(entity => entity.FMainId = taxture.ID);
                    context.Sli_sale_taxturebill.AddRange(taxturebill);
                    await context.SaveChangesAsync();

                    taxturebillEntry.ForEach(entity => entity.FBillId = taxture.ID);
                    context.Sli_sale_taxturebillEntry.AddRange(taxturebillEntry);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}