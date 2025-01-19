using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_SY.Models;




namespace WebApi_SY.Entity
{
    public class YourDbContext:DbContext
    {
        //public DbSet<sli_test> Sli_test { get; set; }

        public DbSet<Sli_plan_model> Sli_plan_model { get; set; }
        public DbSet<Sli_plan_modelEntry> Sli_plan_modelEntry { get; set; }

        public DbSet<sli_user> Sli_user { get; set; }
        public DbSet<t_stock> T_stock { get; set; }
        //12.5 部署  查询，新增
        public DbSet<sli_workOrderList> Sli_workOrderList { get; set; }
        public DbSet<sli_intpo_Import> sli_intpo_Import { get; set; }// **************
        public DbSet<sli_workorderlist_view> sli_workorderlist_view { get; set; }
        public DbSet<t_sal_orderEntry> T_sal_orderEntry { get; set; }

        public DbSet<sli_plan_bill> Sli_plan_bill { get; set; }                 // 交付计划 Plan  交付计划
        public DbSet<sli_plan_billEntry> Sli_plan_billEntry { get; set; }
        public DbSet<sli_plan_billorder> Sli_plan_billorder { get; set; }

        public DbSet<sli_prd_prudcutionPlanB> Sli_prd_prudcutionPlanB { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_pruductionPlanEntryB> Sli_prd_pruductionPlanEntryB { get; set; }

        public DbSet<sli_prd_processReport> Sli_prd_processReport { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_processReportEntry> Sli_prd_processReportEntry { get; set; }

        public DbSet<sli_prd_processTicket> Sli_prd_processTicket { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_processTicketEntry> Sli_prd_processTicketEntry { get; set; }

        public DbSet<sli_quality_request> Sli_quality_request { get; set; }  //  检验申请单
        public DbSet<sli_quality_requestEntry> Sli_quality_requestEntry { get; set; }

        public DbSet<sli_quality_request_view> Sli_quality_request_view { get; set; }
        public DbSet<sli_quality_requestentry_view> Sli_quality_requestentry_view { get; set; }

        //生产订单  查询、新增12.5部署 
        public DbSet<sli_work_order> Sli_work_order { get; set; }        // 生产  prd   生产订单 
        public DbSet<sli_work_orderEntry> Sli_work_orderEntry { get; set; }
        public DbSet<sli_work_orders_view> Sli_work_orders_view { get; set; }  //关联表体的FentryID
        public DbSet<sli_sale_taxture> Sli_sale_taxture { get; set; }
        public DbSet<sli_sale_taxturebill> Sli_sale_taxturebill { get; set; }
        public DbSet<sli_sale_taxturebillEntry> Sli_sale_taxturebillEntry { get; set; }
        public DbSet<sli_dept_info> Sli_dept_info { get; set; }
        public DbSet<sli_bd_employ> Sli_bd_employ { get; set; }
        public DbSet<sli_bd_planOption> Sli_bd_planOption { get; set; }
        public DbSet<sli_bd_customer_view> Sli_bd_customer_view { get; set; }//客户模糊查询表
        public DbSet<sli_bd_tech_option> Sli_bd_tech_option { get; set; }   //技术档案选项
        public DbSet<sli_bd_tech_option_view> Sli_bd_tech_option_view { get; set; }   //技术选项视图
        public DbSet<sli_bd_materials_view> Sli_bd_materials_view { get; set; }   //物料代码视图
        public DbSet<sli_bd_tech_object> Sli_bd_tech_object { get; set; }   //技术档案参数
        public DbSet<sli_bd_quality_option> Sli_bd_quality_option { get; set; }   //质量标准选项
        public DbSet<sli_bd_quality_object> Sli_bd_quality_object { get; set; }   //质量标准参数
        public DbSet<sli_bd_process_option> Sli_bd_process_option { get; set; }   //工艺流程选项
        public DbSet<sli_bd_process_object> Sli_bd_process_object { get; set; }   //工艺流程参数

        public DbSet<sli_document_tech_sale_view> Sli_document_tech_sale_view { get; set; }//产品技术档案表头视图
        public DbSet<sli_document_tech_saleBill_view> Sli_document_tech_saleBill_view { get; set; }//产品技术档案表头视图
        public DbSet<sli_document_tech_saleBillEntry_view> Sli_document_tech_saleBillEntry_view { get; set; }//产品技术档案表体2视图
        public DbSet<sli_document_tech_saleAttachment_view> Sli_document_tech_saleAttachment_view { get; set; }//产品技术档案附件视图

        public DbSet<sli_document_tech_sale> Sli_document_tech_sale { get; set; }//产品技术档案表头
        public DbSet<sli_document_tech_saleBill> Sli_document_tech_saleBill { get; set; }//产品技术档案表体1
        public DbSet<sli_document_tech_saleBillEntry> Sli_document_tech_saleBillEntry { get; set; }//产品技术档案表体2
        public DbSet<sli_document_tech_saleAttachment> Sli_document_tech_saleAttachment { get; set; }//产品技术档案附件


        public DbSet<sli_document_quality_standard> Sli_document_quality_standard { get; set; }//质量标准表头
        public DbSet<sli_document_quality_standardBill> Sli_document_quality_standardBill { get; set; }//质量标准表体1

        //public DbSet<sli_witnessing_object> sli_witnessing_object { get; set; }//见证计划表头
        public DbSet<sli_witnessing_objectbill> sli_witnessing_objectbill { get; set; }//见证计划表体
        public DbSet<sli_witnessing_objectbill_view> sli_witnessing_objectbill_view { get; set; }//见证计划视图

        public DbSet<sli_document_quality_standardBillEntry> Sli_document_quality_standardBillEntry { get; set; }//质量标准表体2
        public DbSet<sli_document_quality_standardAttachment> Sli_document_quality_standardAttachment { get; set; }//质量标准附件


        public DbSet<sli_document_quality_standard_view> Sli_document_quality_standard_view { get; set; }//质量标准表头视图
        public DbSet<sli_document_quality_standardBill_view> Sli_document_quality_standardBill_view { get; set; }//质量标准表体1视图
        public DbSet<sli_document_quality_standardBillEntry_view> Sli_document_quality_standardBillEntry_view { get; set; }//质量标准表体2视图
        public DbSet<sli_document_quality_standardAttachment_view> Sli_document_quality_standardAttachment_view { get; set; }//质量标准附件视图

        public DbSet<sli_document_process_model> Sli_document_process_model { get; set; }//产品工艺档案表头
        public DbSet<sli_document_process_modelBill> Sli_document_process_modelBill { get; set; }//产品工艺档案表体1
        public DbSet<sli_document_process_modelBillEntry> Sli_document_process_modelBillEntry { get; set; }//产品工艺档案表体2
        public DbSet<sli_document_process_modelAttachment> Sli_document_process_modelAttachment { get; set; }//产品工艺档案附件


        public DbSet<sli_document_mp_rolling> sli_document_mp_rolling { get; set; }//产品工艺文件

        public DbSet<sli_document_process_model_view> Sli_document_process_model_view { get; set; }//产品工艺档案表头
        public DbSet<sli_document_process_modelBill_view> Sli_document_process_modelBill_view { get; set; }//产品工艺档案表体1
        public DbSet<sli_document_process_modelBillEntry_view> Sli_document_process_modelBillEntry_view { get; set; }//产品工艺档案表体2
        public DbSet<sli_document_process_modelAttachment_view> Sli_document_process_modelAttachment_view { get; set; }//产品工艺档案附件

        public DbSet<sli_sale_orderImportentry> Sli_sale_orderImportentry { get; set; }//销售订单导入表体
        public DbSet<sli_sale_orderImport> Sli_sale_orderImport { get; set; }//销售订单导入表头
        public DbSet<sli_sale_orderImport_view> Sli_sale_orderImport_view { get; set; }//销售订单导入视图

        public DbSet<sli_sal_order_buss_view> Sli_sal_order_buss_view { get; set; }//销售订单关联客户视图


        public DbSet<sli_sal_order_view> Sli_sal_order_view { get; set; }//销售订单关联客户视图

        public DbSet<sli_sal_orderEntry_view> Sli_sal_orderEntry_view { get; set; }//销售订单关联客户视图

        public DbSet<sli_sal_orders_view> Sli_sal_orders_view { get; set; }//销售订单关联客户视图

        public DbSet<sli_sal_orderDocument> Sli_sal_orderDocument { get; set; }//销售订单表单合并

        public DbSet<sli_work_processBill> Sli_work_processBill { get; set; }//工艺流转卡表头
        public DbSet<sli_work_processBillEntry> Sli_work_processBillEntry { get; set; }//工艺流转卡表体
        public DbSet<sli_work_processBill_view> Sli_work_processBill_view { get; set; }//工艺路线视图
        public DbSet<sli_work_processBillEntry_view> Sli_work_processBillEntry_view { get; set; }//工艺路线表体视图



        public DbSet<sli_mes_lauchbill> sli_mes_lauchbill { get; set; }//投产计划

        public DbSet<sli_mes_lauchbill_view> sli_mes_lauchbill_view { get; set; }//投产计划

        public DbSet<sli_mes_orderoption> Sli_mes_orderoption { get; set; }//投产清单表
        public DbSet<sli_mes_orderoption_view> Sli_mes_orderoption_view { get; set; }//投产清单表

        public DbSet<sli_mes_objectreport> Sli_mes_objectreport { get; set; }//工步汇报
        public DbSet<sli_mes_objectreport_view> Sli_mes_objectreport_view { get; set; }//工步汇报

        public DbSet<sli_mes_optionreport> sli_mes_optionreport { get; set; }//工序流转
        public DbSet<sli_mes_optionreport_view> sli_mes_optionreport_view { get; set; }//工序流转

        public DbSet<sli_mes_furnace> Sli_mes_furnace { get; set; }//装炉单
        public DbSet<sli_mes_furnace_view> Sli_mes_furnace_view { get; set; }//装炉单

        public DbSet<sli_quality_report> Sli_quality_report { get; set; }//质量报告表头
        public DbSet<sli_quality_reportentry> Sli_quality_reportentry { get; set; }//质量报告表体
        public DbSet<sli_quality_report_view> Sli_quality_report_view { get; set; }//质量报告表头视图
        public DbSet<sli_quality_reportentry_view> Sli_quality_reportentry_view { get; set; }//质量报告表体视图

        public DbSet<sli_witnessing_order> Sli_witnessing_order { get; set; }//见证任务单表头
        public DbSet<sli_witnessing_orderbill> Sli_witnessing_orderbill { get; set; }//见证任务单表体
        public DbSet<sli_witnessing_orderbill_view> Sli_witnessing_orderbill_view { get; set; }//见证任务单视图




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConnectionStrings 19vs7gv47690.vicp.fun,46716
            //optionsBuilder.UseSqlServer("Data Source=61.174.243.28,45047;Initial Catalog=AIS20241011165800;User ID=sa;Password=pct258258!;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer("Data Source=19vs7gv47690.vicp.fun,16819;Initial Catalog=AIS20241011165800;User ID=sa;Password=kingdee123*;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer("ConnectionStrings");
            //1111111

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sli_plan_model>()
               .HasMany(h => h.Sli_plan_modelEntry)
               .WithOne(d => d.Sli_plan_model)
               .HasForeignKey(d => d.Fmodelid);

            modelBuilder.Entity<sli_sal_order_view>()
              .HasMany(h => h.sli_sal_orderEntry_view)
              .WithOne(d => d.sli_sal_order_view)
              .HasForeignKey(d => d.Fid);

            modelBuilder.Entity<sli_plan_bill>()
               .HasMany(h => h.sli_plan_billEntry)
               .WithOne(d => d.sli_plan_bill)
               .HasForeignKey(d => d.Fplanbillid);

            modelBuilder.Entity<sli_plan_bill>()
               .HasMany(h => h.sli_plan_billorder)
               .WithOne(d => d.sli_plan_bill)
               .HasForeignKey(d => d.Fplanbillid);

            //modelBuilder.Entity<sli_plan_bill>()
            // .HasMany(h => h.sli_plan_billorder)
            // .WithOne()
            //  .HasForeignKey("Fplanbillid"); // 外键列名



            // modelBuilder.Entity<sli_work_orderEntry>()
            //.Property(p => p.Fentryid)
            //.ValueGeneratedOnAdd();   //字段自增

            modelBuilder.Entity<sli_sale_taxturebill>()
               .HasOne(h => h.sli_sale_taxture)
               .WithMany(d => d.sli_sale_taxturebill)
               .HasForeignKey(d => d.FMainId);

            modelBuilder.Entity<sli_sale_taxturebillEntry>()
               .HasOne(h => h.sli_sale_taxture)
               .WithMany(d => d.sli_sale_taxturebillEntry)
               .HasForeignKey(d => d.FBillId);


            //技术档案外键关联
            modelBuilder.Entity<sli_document_tech_saleBill>()
               .HasOne(h => h.sli_document_tech_sale)
               .WithMany(d => d.sli_document_tech_saleBill)
               .HasForeignKey(d => d.fmainID);

            modelBuilder.Entity<sli_document_tech_saleBillEntry>()
               .HasOne(h => h.sli_document_tech_sale)
               .WithMany(d => d.sli_document_tech_saleBillEntry)
               .HasForeignKey(d => d.fbillID);

            modelBuilder.Entity<sli_document_tech_saleAttachment>()
               .HasOne(h => h.sli_document_tech_sale)
               .WithMany(d => d.sli_document_tech_saleAttachment)
               .HasForeignKey(d => d.fmainID);

            modelBuilder.Entity<sli_sale_orderImportentry>()
               .HasOne(h => h.sli_sale_orderImport)
               .WithMany(d => d.sli_sale_orderImportentry)
               .HasForeignKey(d => d.fid);


            //技术档案视图查询
            modelBuilder.Entity<sli_document_tech_saleBill_view>()
               .HasOne(h => h.sli_document_tech_sale_view)
               .WithMany(d => d.sli_document_tech_saleBill_view)
               .HasForeignKey(d => d.fmainID);

            modelBuilder.Entity<sli_document_tech_saleBillEntry_view>()
               .HasOne(h => h.sli_document_tech_sale_view)
               .WithMany(d => d.sli_document_tech_saleBillEntry_view)
               .HasForeignKey(d => d.fbillID);

            modelBuilder.Entity<sli_document_tech_saleAttachment_view>()
               .HasOne(h => h.sli_document_tech_sale_view)
               .WithMany(d => d.sli_document_tech_saleAttachment_view)
               .HasForeignKey(d => d.fmainID);


            //质量标准管理外键关联
            modelBuilder.Entity<sli_document_quality_standardBill>()
               .HasOne(h => h.sli_document_quality_standard)
               .WithMany(d => d.sli_document_quality_standardBill)
               .HasForeignKey(d => d.fmainID);

            modelBuilder.Entity<sli_document_quality_standardBillEntry>()
               .HasOne(h => h.sli_document_quality_standard)
               .WithMany(d => d.sli_document_quality_standardBillEntry)
               .HasForeignKey(d => d.fbillID);

            modelBuilder.Entity<sli_document_quality_standardAttachment>()
               .HasOne(h => h.sli_document_quality_standard)
               .WithMany(d => d.sli_document_quality_standardAttachment)
               .HasForeignKey(d => d.fmainID);

            //质量标准管理视图外键关联
            modelBuilder.Entity<sli_document_quality_standardBill_view>()
               .HasOne(h => h.sli_document_quality_standard_view)
               .WithMany(d => d.sli_document_quality_standardBill_view)
               .HasForeignKey(d => d.fmainID);

            modelBuilder.Entity<sli_document_quality_standardBillEntry_view>()
               .HasOne(h => h.sli_document_quality_standard_view)
               .WithMany(d => d.sli_document_quality_standardBillEntry_view)
               .HasForeignKey(d => d.fbillID);

            modelBuilder.Entity<sli_document_quality_standardAttachment_view>()
               .HasOne(h => h.sli_document_quality_standard_view)
               .WithMany(d => d.sli_document_quality_standardAttachment_view)
               .HasForeignKey(d => d.fmainID);

            //产品工艺档案外键关联
            modelBuilder.Entity<sli_document_process_modelBill>()
               .HasOne(h => h.sli_document_process_model)
               .WithMany(d => d.sli_document_process_modelBill)
               .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_document_process_modelBillEntry>()
               .HasOne(h => h.sli_document_process_modelBill)
               .WithMany(d => d.sli_document_process_modelBillEntry)
               .HasForeignKey(d => d.Fbillid);

            modelBuilder.Entity<sli_document_process_modelAttachment>()
               .HasOne(h => h.sli_document_process_model)
               .WithMany(d => d.sli_document_process_modelAttachment)
               .HasForeignKey(d => d.fmainid);

            //产品工艺档案视图外键关联
            modelBuilder.Entity<sli_document_process_modelBill_view>()
              .HasOne(h => h.sli_document_process_model_view)
              .WithMany(d => d.sli_document_process_modelBill_view)
              .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_document_process_modelBillEntry_view>()
               .HasOne(h => h.sli_document_process_modelBill_view)
               .WithMany(d => d.sli_document_process_modelBillEntry_view)
               .HasForeignKey(d => d.Fbillid);

            modelBuilder.Entity<sli_document_process_modelAttachment_view>()
               .HasOne(h => h.sli_document_process_model_view)
               .WithMany(d => d.sli_document_process_modelAttachment_view)
               .HasForeignKey(d => d.fmainid);


            modelBuilder.Entity<sli_work_processBillEntry>()
               .HasOne(h => h.sli_work_processBill)
               .WithMany(d => d.sli_work_processBillEntry)
               .HasForeignKey(d => d.Fbillid);

            modelBuilder.Entity<sli_work_processBillEntry_view>()
               .HasOne(h => h.sli_work_processBill_view)
               .WithMany(d => d.sli_work_processBillEntry_view)
               .HasForeignKey(d => d.Fbillid);


            modelBuilder.Entity<sli_work_orderEntry>()
               .HasOne(h => h.sli_work_order)
               .WithMany(d => d.sli_work_orderEntry)
               .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_quality_requestEntry>()
              .HasOne(h => h.sli_quality_request)
              .WithMany(d => d.sli_quality_requestEntry)
              .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_quality_requestentry_view>()
              .HasOne(h => h.sli_quality_request_view)
              .WithMany(d => d.sli_quality_requestentry_view)
              .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_quality_reportentry>()
              .HasOne(h => h.sli_quality_report)
              .WithMany(d => d.sli_quality_reportentry)
              .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_quality_reportentry_view>()
              .HasOne(h => h.sli_quality_report_view)
              .WithMany(d => d.sli_quality_reportentry_view)
              .HasForeignKey(d => d.Id);

            modelBuilder.Entity<sli_witnessing_orderbill>()
             .HasOne(h => h.sli_witnessing_order)
             .WithMany(d => d.sli_witnessing_orderbill)
             .HasForeignKey(d => d.Id);

        }

    }
}