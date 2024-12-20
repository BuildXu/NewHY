using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi_SY.Entity;

namespace WebApi_SY.Models
{
    public class maxfproductNumber
    {
       public static string IncrementAfterLastSpecialCharacter(string fproductNumber)
        {
            var context = new YourDbContext();
            string newfproductNumber = fproductNumber+"-1";
            //DbSet Sli_workOrderListall = context.Sli_workOrderList;
            //var Sli_workOrderListAll= context.Sli_workOrderList;
            var fproductNumberSQL = context.Sli_workOrderList.FirstOrDefault(p => p.Fproductno == newfproductNumber);
            context.Dispose();
            if (fproductNumberSQL != null)
            {

                //var count = newfproductNumber.Count(c => c == '-');  //获取字符串中-的个数
                //var prefix = newfproductNumber.Split('-')[count - 1];
                var maxValue = "";
                using (var context1 = new YourDbContext())
                {
                     maxValue = context1.Sli_workOrderList
                   .Where(e => e.Fproductno.Contains(fproductNumber))
                   .Max(e => e.Fproductno) ;

                }
                    


                int lastDashIndex = maxValue.LastIndexOf('-');
                if (lastDashIndex == -1)
                    return maxValue;

                string numberPart = maxValue.Substring(lastDashIndex + 1);
                int number = int.Parse(numberPart);
                return maxValue.Substring(0, lastDashIndex + 1) + (number + 1).ToString();
                //return ;


            }
            else
            {
                return newfproductNumber;
            }

            
        }
    }
}