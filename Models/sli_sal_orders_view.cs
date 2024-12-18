using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_SY.Models
{
    public class sli_sal_orders_view
    {
        [Key]
        public int Id { get; set; }
        public string BillNo { get; set; }
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public int EntryId1 { get; set; }
        public int EntryId2 { get; set; }
        public int Sequence { get; set; }
        public decimal Quantity { get; set; }
        public decimal? StockQuantity { get; set; }
        public string DeliveryDate { get; set; }
        public decimal WeightMaterial { get; set; }
        public int MaterialId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal OuterDiameter { get; set; }
        public decimal InnerDiameter { get; set; }
        public decimal Height { get; set; }
        public decimal AllowanceOD { get; set; }
        public decimal AllowanceID { get; set; }
        public decimal AllowanceH { get; set; }
        public decimal WeightForging { get; set; }
        public decimal WeightGoods { get; set; }
        public string DrawingNo { get; set; }
        public string Metal { get; set; }
        public string GoodsStatus { get; set; }
        public string ProcessingMethod { get; set; }
        public string DeliveryMethod { get; set; }
        public string BlankModel { get; set; }
        public string PunchingModel { get; set; }
        public decimal TemperatureBegin { get; set; }
        public decimal TemperatureEnd { get; set; }
        public string Mould { get; set; }
        public string Roller { get; set; }
        public int HeatingTimes { get; set; }
        public string Grade { get; set; }
        public string OrderNote { get; set; }
    }
}