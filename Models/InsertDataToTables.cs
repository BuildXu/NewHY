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

        public async Task InsertData_document_tech_sale(YourDbContext context, sli_document_tech_sale tech_sale, List<sli_document_tech_saleBill> tech_saleBill, List<sli_document_tech_saleBillEntry> tech_saleBillEntry, List<sli_document_tech_saleAttachment> tech_saleAttachment)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Sli_document_tech_sale.Add(tech_sale);
                    await context.SaveChangesAsync();

                    tech_saleBill.ForEach(entity => entity.fmainID = tech_sale.Id);
                    context.Sli_document_tech_saleBill.AddRange(tech_saleBill);
                    await context.SaveChangesAsync();

                    tech_saleBillEntry.ForEach(entity => entity.fbillID = tech_sale.Id);
                    context.Sli_document_tech_saleBillEntry.AddRange(tech_saleBillEntry);
                    await context.SaveChangesAsync();

                    tech_saleAttachment.ForEach(entity => entity.fmainID = tech_sale.Id);
                    context.Sli_document_tech_saleAttachment.AddRange(tech_saleAttachment);
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