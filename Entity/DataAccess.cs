using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_SY.Models;




namespace WebApi_SY.Entity
{
    public class YourDbContext:DbContext
    {
        //public DbSet<sli_test> Sli_test { get; set; }

        public DbSet<sli_plan_model> Sli_plan_model { get; set; }
        public DbSet<sli_plan_modelEntry> Sli_plan_modelEntry { get; set; }

        public DbSet<sli_user> Sli_user { get; set; }
        public DbSet<t_stock> T_stock { get; set; }
        public DbSet<sli_workOrderList> Sli_workOrderList { get; set; }
        public DbSet<t_sal_orderEntry> T_sal_orderEntry { get; set; }

        public DbSet<sli_plan_bill> Sli_plan_bill { get; set; }                 // 交付计划 Plan  交付计划
        public DbSet<sli_plan_billlEntry> Sli_plan_billlEntry { get; set; }

        public DbSet<sli_prd_prudcutionPlanB> Sli_prd_prudcutionPlanB { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_pruductionPlanEntryB> Sli_prd_pruductionPlanEntryB { get; set; }

        public DbSet<sli_prd_processReport> Sli_prd_processReport { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_processReportEntry> Sli_prd_processReportEntry { get; set; }

        public DbSet<sli_prd_processTicket> Sli_prd_processTicket { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_prd_processTicketEntry> Sli_prd_processTicketEntry { get; set; }

        public DbSet<sli_quality_request> Sli_quality_request { get; set; }  //  生产车间Prd  投产计划  周计划
        public DbSet<sli_quality_requestEntry> Sli_quality_requestEntry { get; set; }

        public DbSet<sli_workorder> Sli_workorder { get; set; }        // 生产  prd   生产订单 
        public DbSet<sli_workorderentry> Sli_workorderentry { get; set; }

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
        public DbSet<sli_document_quality_standardBillEntry> Sli_document_quality_standardBillEntry { get; set; }//质量标准表体2
        public DbSet<sli_document_quality_standardAttachment> Sli_document_quality_standardAttachment { get; set; }//质量标准附件


        public DbSet<sli_document_quality_standard_view> Sli_document_quality_standard_view { get; set; }//质量标准表头视图
        public DbSet<sli_document_quality_standardBill_view> Sli_document_quality_standardBill_view { get; set; }//质量标准表体1视图
        public DbSet<sli_document_quality_standardBillEntry_view> Sli_document_quality_standardBillEntry_view { get; set; }//质量标准表体2视图
        public DbSet<sli_document_quality_standardAttachment_view> Sli_document_quality_standardAttachment_view { get; set; }//质量标准附件视图

        public DbSet<sli_sale_orderImportentry> Sli_sale_orderImportentry { get; set; }//销售订单导入表体
        public DbSet<sli_sale_orderImport> Sli_sale_orderImport { get; set; }//销售订单导入表头
        public DbSet<sli_sale_orderImport_view> Sli_sale_orderImport_view { get; set; }//销售订单导入视图

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
            modelBuilder.Entity<sli_plan_model>()
               .HasMany(h => h.sli_plan_modelEntry)
               .WithOne(d => d.sli_plan_model)
               .HasForeignKey(d => d.fmodelID);

            modelBuilder.Entity<sli_plan_bill>()
               .HasMany(h => h.sli_plan_billlEntry)
               .WithOne(d => d.sli_plan_bill)
               .HasForeignKey(d => d.fplanBillId);

            modelBuilder.Entity<sli_workorder>()
               .HasMany(h => h.sli_workorderentry)
               .WithOne(d => d.sli_workorder)
               .HasForeignKey(d => d.ForderEntryid);

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

        }

    }
}