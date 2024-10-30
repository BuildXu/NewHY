using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi_SY.Models;




namespace WebApi_SY.Entity
{
    public class YourDbContext:DbContext
    {
       
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
        public DbSet<sli_bd_tech_option> Sli_bd_tech_option { get; set; }   //技术选项

        public DbSet<sli_bd_material_view> Sli_bd_material_view { get; set; }


        public DbSet<sli_document_tech_sale> Sli_document_tech_sale { get; set; }//产品技术档案表头
        public DbSet<sli_document_tech_saleBill> Sli_document_tech_saleBill { get; set; }//产品技术档案表体1
        public DbSet<sli_document_tech_saleBillEntry> Sli_document_tech_saleBillEntry { get; set; }//产品技术档案表体2
        public DbSet<sli_document_tech_saleAttachment> Sli_document_tech_saleAttachment { get; set; }//产品技术档案附件

        public DbSet<sli_bd_customer_view> Sli_bd_customer_view { get; set; }//客户模糊查询表

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConnectionStrings
            optionsBuilder.UseSqlServer("Data Source=61.174.243.28,45047;Initial Catalog=AIS20241011165800;User ID=sa;Password=pct258258!;TrustServerCertificate=True;");
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


        }

    }
}