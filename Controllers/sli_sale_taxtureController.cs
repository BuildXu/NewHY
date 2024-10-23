using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_SY.Entity;
using WebApi_SY.Models;

namespace WebApi_SY.Controllers
{
    public class sli_sale_taxtureController : ApiController
    {
        public sli_sale_taxtureController()
        {
            // _context = context;

        }
        public class InsertRequestDto
        {
            public sli_sale_taxture Taxture { get; set; }
            public List<sli_sale_taxturebill> Taxturebill { get; set; }
            public List<sli_sale_taxturebillEntry> TaxturebillEntry { get; set; }
        }
        public async Task<object> Insert(InsertRequestDto requestDto)
        {
            try
            {
                var context = new YourDbContext();
                // 将 DTO 转换为实体对象
                var tableHeader = new sli_sale_taxture
                {
                    // 根据 DTO 的属性设置实体对象的属性
                    FNumber = requestDto.Taxture.FNumber,
                    FDate = requestDto.Taxture.FDate,
                    FCustomer = requestDto.Taxture.FCustomer,
                    FDeliveryDate = requestDto.Taxture.FDeliveryDate
                };

                var tableBody1Entities = requestDto.Taxturebill.Select(dto => new sli_sale_taxturebill
                {
                    FTexture = dto.FTexture,
                    FSaleTechId = dto.FSaleTechId,
                    FTechId = dto.FTechId,
                    FTechName = dto.FTechName,
                    FNum = dto.FNum,
                    FWeight = dto.FWeight,
                    FTechNo = dto.FTechNo,
                    FSaleTechNo = dto.FSaleTechNo,
                    Updatetime = dto.Updatetime,
                    FMainId = tableHeader.ID
                }).ToList();

                var tableBody2Entities = requestDto.TaxturebillEntry.Select(dto => new sli_sale_taxturebillEntry
                {
                    FOrderEntryId = dto.FOrderEntryId,
                    FTexture = dto.FTexture,
                    FSaleTechId = dto.FSaleTechId,
                    FTechId = dto.FTechId,
                    FMaterialNo = dto.FMaterialNo,
                    FBillNo = dto.FBillNo,
                    FRowNo = dto.FRowNo,
                    FTechNo = dto.FTechNo,
                    FModel = dto.FModel,
                    FSaleTechNo = dto.FSaleTechNo,
                    FBillId = tableHeader.ID
                }).ToList();
                InsertDataToTables insert = new InsertDataToTables();
                // 调用数据访问层方法插入数据
                await insert.InsertData(context, tableHeader, tableBody1Entities, tableBody2Entities);

                var dataNull = new
                {
                    code = 200,
                    msg = "ok",
                    Id = tableHeader.ID,
                    date = tableHeader.ID.ToString() + "保存成功"

                };
                return dataNull;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        
    }
}