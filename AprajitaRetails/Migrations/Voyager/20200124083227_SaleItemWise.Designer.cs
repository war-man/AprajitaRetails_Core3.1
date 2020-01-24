﻿// <auto-generated />
using System;
using AprajitaRetails.Areas.Voyager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AprajitaRetails.Migrations.Voyager
{
    [DbContext(typeof(VoyagerContext))]
    [Migration("20200124083227_SaleItemWise")]
    partial class SaleItemWise
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimaryCategory")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSecondaryCategory")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.ProductItem", b =>
                {
                    b.Property<int>("ProductItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Categorys")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MRP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MainCategoryCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductCategoryCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductTypeCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("StyleCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("ProductItemId");

                    b.HasIndex("BrandId");

                    b.HasIndex("MainCategoryCategoryId");

                    b.HasIndex("ProductCategoryCategoryId");

                    b.HasIndex("ProductTypeCategoryId");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.ProductPurchase", b =>
                {
                    b.Property<int>("ProductPurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InWardDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InWardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ShippingCost")
                        .HasColumnType("money");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalBasicAmount")
                        .HasColumnType("money");

                    b.Property<double>("TotalQty")
                        .HasColumnType("float");

                    b.Property<decimal>("TotalTax")
                        .HasColumnType("money");

                    b.HasKey("ProductPurchaseId");

                    b.HasIndex("SupplierID");

                    b.ToTable("ProductPurchases");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.PurchaseItem", b =>
                {
                    b.Property<int>("PurchaseItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<decimal>("CostValue")
                        .HasColumnType("money");

                    b.Property<int>("ProductItemId")
                        .HasColumnType("int");

                    b.Property<int>("ProductPurchaseId")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseTaxTypeId")
                        .HasColumnType("int");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.Property<decimal>("TaxAmout")
                        .HasColumnType("money");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("PurchaseItemId");

                    b.HasIndex("ProductItemId");

                    b.HasIndex("ProductPurchaseId");

                    b.HasIndex("PurchaseTaxTypeId");

                    b.ToTable("PurchaseItems");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.PurchaseTaxType", b =>
                {
                    b.Property<int>("PurchaseTaxTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CompositeRate")
                        .HasColumnType("money");

                    b.Property<string>("TaxName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxType")
                        .HasColumnType("int");

                    b.HasKey("PurchaseTaxTypeId");

                    b.ToTable("PurchaseTaxTypes");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductItemId")
                        .HasColumnType("int");

                    b.Property<double>("PurchaseQty")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SaleQty")
                        .HasColumnType("float");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("StockID");

                    b.HasIndex("ProductItemId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SuppilerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Warehouse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.CardPaymentDetail", b =>
                {
                    b.Property<int>("CardPaymentDetailId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("AuthCode")
                        .HasColumnType("int");

                    b.Property<int>("CardType")
                        .HasColumnType("int");

                    b.Property<int>("LastDigit")
                        .HasColumnType("int");

                    b.Property<int>("SaleInvoiceId")
                        .HasColumnType("int");

                    b.HasKey("CardPaymentDetailId");

                    b.ToTable("CardPaymentDetails");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SaleInvoice", b =>
                {
                    b.Property<int>("SaleInvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OnDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RoundOffAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalBillAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalDiscountAmount")
                        .HasColumnType("money");

                    b.Property<int>("TotalItems")
                        .HasColumnType("int");

                    b.Property<double>("TotalQty")
                        .HasColumnType("float");

                    b.Property<decimal>("TotalTaxAmount")
                        .HasColumnType("money");

                    b.HasKey("SaleInvoiceId");

                    b.ToTable("SaleInvoices");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SaleItem", b =>
                {
                    b.Property<int>("SaleItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BasicAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("BillAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("Discount")
                        .HasColumnType("money");

                    b.Property<decimal>("MRP")
                        .HasColumnType("money");

                    b.Property<int>("ProductItemId")
                        .HasColumnType("int");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.Property<int>("SaleInvoiceId")
                        .HasColumnType("int");

                    b.Property<int?>("SaleTaxTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SalesPersonId")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("money");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("SaleItemId");

                    b.HasIndex("ProductItemId");

                    b.HasIndex("SaleInvoiceId");

                    b.HasIndex("SaleTaxTypeId");

                    b.HasIndex("SalesPersonId");

                    b.ToTable("SaleItems");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SalePaymentDetail", b =>
                {
                    b.Property<int>("SalePaymentDetailId")
                        .HasColumnType("int");

                    b.Property<decimal>("CardAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("CashAmount")
                        .HasColumnType("money");

                    b.Property<decimal>("MixAmount")
                        .HasColumnType("money");

                    b.Property<int>("PayMode")
                        .HasColumnType("int");

                    b.HasKey("SalePaymentDetailId");

                    b.ToTable("SalePaymentDetails");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SaleTaxType", b =>
                {
                    b.Property<int>("SaleTaxTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CompositeRate")
                        .HasColumnType("money");

                    b.Property<string>("TaxName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxType")
                        .HasColumnType("int");

                    b.HasKey("SaleTaxTypeId");

                    b.ToTable("SaleTaxTypes");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SalesPerson", b =>
                {
                    b.Property<int>("SalesPersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SalesmanName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalesPersonId");

                    b.ToTable("SalesPerson");

                    b.HasData(
                        new
                        {
                            SalesPersonId = 1,
                            SalesmanName = "Sanjeev Mishra"
                        },
                        new
                        {
                            SalesPersonId = 2,
                            SalesmanName = "Mukesh Mandal"
                        },
                        new
                        {
                            SalesPersonId = 3,
                            SalesmanName = "Manager"
                        });
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Uploader.Models.ImportInWard", b =>
                {
                    b.Property<int>("ImportInWardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ImportDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InWardDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("InWardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDataConsumed")
                        .HasColumnType("bit");

                    b.Property<string>("PartyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalMRPValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalQty")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ImportInWardId");

                    b.ToTable("ImportInWards");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Uploader.Models.ImportPurchase", b =>
                {
                    b.Property<int>("ImportPurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<decimal>("CostValue")
                        .HasColumnType("money");

                    b.Property<DateTime>("GRNDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GRNNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ImportTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDataConsumed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLocal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVatBill")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MRP")
                        .HasColumnType("money");

                    b.Property<decimal>("MRPValue")
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("StyleCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TaxAmt")
                        .HasColumnType("money");

                    b.HasKey("ImportPurchaseId");

                    b.ToTable("ImportPurchases");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Uploader.Models.ImportSaleItemWise", b =>
                {
                    b.Property<int>("ImportSaleItemWiseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BasicRate")
                        .HasColumnType("money");

                    b.Property<decimal>("BillAmnt")
                        .HasColumnType("money");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CGST")
                        .HasColumnType("money");

                    b.Property<decimal>("Discount")
                        .HasColumnType("money");

                    b.Property<string>("HSNCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ImportTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDataConsumed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLocal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVatBill")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LineTotal")
                        .HasColumnType("money");

                    b.Property<decimal>("MRP")
                        .HasColumnType("money");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<decimal>("RoundOff")
                        .HasColumnType("money");

                    b.Property<decimal>("SGST")
                        .HasColumnType("money");

                    b.Property<string>("Saleman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StyleCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("money");

                    b.HasKey("ImportSaleItemWiseId");

                    b.ToTable("ImportSaleItemWises");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Uploader.Models.ImportSaleRegister", b =>
                {
                    b.Property<int>("ImportSaleRegisterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BasicRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BillAmnt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ImportTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConsumed")
                        .HasColumnType("bit");

                    b.Property<decimal>("MRP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<decimal>("RoundOff")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ImportSaleRegisterId");

                    b.ToTable("ImportSaleRegisters");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Voyager.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfBills")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Voyager.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GSTNO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfEmployees")
                        .HasColumnType("int");

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PanNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PinCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StoreCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreManagerPhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.ProductItem", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.Brand", "BrandName")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.Category", "MainCategory")
                        .WithMany()
                        .HasForeignKey("MainCategoryCategoryId");

                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.Category", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryCategoryId");

                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.Category", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeCategoryId");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.ProductPurchase", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.Supplier", "Supplier")
                        .WithMany("ProductPurchases")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.PurchaseItem", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.ProductItem", "ProductItem")
                        .WithMany("PurchaseItems")
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.ProductPurchase", "ProductPurchase")
                        .WithMany("PurchaseItems")
                        .HasForeignKey("ProductPurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.PurchaseTaxType", "PurchaseTaxType")
                        .WithMany("PurchaseItems")
                        .HasForeignKey("PurchaseTaxTypeId");
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Purchase.Models.Stock", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.ProductItem", "ProductItem")
                        .WithMany()
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.CardPaymentDetail", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Sales.Models.SalePaymentDetail", "SalePaymentDetail")
                        .WithOne("CardDetails")
                        .HasForeignKey("AprajitaRetails.Areas.Sales.Models.CardPaymentDetail", "CardPaymentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SaleItem", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Purchase.Models.ProductItem", "ProductItem")
                        .WithMany()
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AprajitaRetails.Areas.Sales.Models.SaleInvoice", "SaleInvoice")
                        .WithMany("SaleItems")
                        .HasForeignKey("SaleInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AprajitaRetails.Areas.Sales.Models.SaleTaxType", "SaleTaxType")
                        .WithMany("SaleItems")
                        .HasForeignKey("SaleTaxTypeId");

                    b.HasOne("AprajitaRetails.Areas.Sales.Models.SalesPerson", "Salesman")
                        .WithMany("SaleItems")
                        .HasForeignKey("SalesPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AprajitaRetails.Areas.Sales.Models.SalePaymentDetail", b =>
                {
                    b.HasOne("AprajitaRetails.Areas.Sales.Models.SaleInvoice", "SaleInvoice")
                        .WithOne("PaymentDetail")
                        .HasForeignKey("AprajitaRetails.Areas.Sales.Models.SalePaymentDetail", "SalePaymentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
