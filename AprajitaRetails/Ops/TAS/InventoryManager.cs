﻿using System;
using System.Collections.Generic;
using System.Linq;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Sales.Models;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Areas.Voyager.Data;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Ops.TAS
{
    /// <summary>
    /// This class help to manage voyager data in perfect and correct manger
    /// </summary>
    public class InventoryManger
    {
        private readonly int StoreID = 1;

        public InventoryManger(int storeid)
        {
            StoreID = storeid;
        }

        #region HelperFunctions

        public void UpdateHSNCode(VoyagerContext db, string HSNCode, int itemCode)
        {
        }

        public int GetSalesPersonId(VoyagerContext db, string salesman)
        {
            try
            {
                var id = db.SalesPerson.Where (c => c.SalesmanName == salesman).FirstOrDefault ().SalesPersonId;
                if ( id > 0 )
                {
                    return id;
                }
                else
                {
                    SalesPerson sm = new SalesPerson { SalesmanName = salesman };
                    db.SalesPerson.Add (sm);
                    db.SaveChanges ();
                    return sm.SalesPersonId;
                }
            }
            catch ( Exception )
            {
                SalesPerson sm = new SalesPerson { SalesmanName = salesman };
                db.SalesPerson.Add (sm);
                db.SaveChanges ();
                return sm.SalesPersonId;
            }
        }

        public int GetCustomerId(VoyagerContext db, ImportSaleItemWise item)
        {
            if ( item.PaymentType == "CAS" ) //TODO: Check For Actual Data.
                return 1;
            else
                return 2;
            //return 1;
        }

        private List<Category> GetCategory(VoyagerContext db, string pCat, string sCat, string tCat)
        {
            List<Category> CatIdList = new List<Category> ();

            Category Cid = db.Categories.Where (c => c.CategoryName == pCat && c.IsPrimaryCategory).FirstOrDefault ();

            if ( Cid == null )
            {
                Category cat1 = new Category { CategoryName = pCat, IsPrimaryCategory = true };
                db.Categories.Add (cat1);
                db.SaveChanges ();
                CatIdList.Add (cat1);
            }
            else if ( Cid != null )
            {
                CatIdList.Add (Cid);
            }
            else
            { }

            //id = 0;
            Category Cid2 = db.Categories.Where (c => c.CategoryName == sCat && c.IsSecondaryCategory).FirstOrDefault ();
            if ( Cid2 == null )
            {
                Category cat2 = new Category { CategoryName = sCat, IsSecondaryCategory = true };
                db.Categories.Add (cat2);
                db.SaveChanges ();
                CatIdList.Add (cat2);
            }
            else if ( Cid2 != null )
            {
                CatIdList.Add (Cid2);
            }
            else
            { }

            Category Cid3 = db.Categories.Where (c => c.CategoryName == tCat && !c.IsPrimaryCategory && !c.IsSecondaryCategory).FirstOrDefault ();
            if ( Cid3 == null )
            {
                Category cat3 = new Category { CategoryName = tCat };
                db.Categories.Add (cat3);
                db.SaveChanges ();
                CatIdList.Add (cat3);
            }
            else if ( Cid3 != null )
            {
                CatIdList.Add (Cid3);
            }
            else
            { }

            return CatIdList;
        }

        private List<int> GetCategoryId(VoyagerContext db, string pCat, string sCat, string tCat)
        {
            List<int> CatIdList = new List<int> ();
            int id = (int?) db.Categories.Where (c => c.CategoryName == pCat && c.IsPrimaryCategory).FirstOrDefault ().CategoryId ?? -1;
            if ( id == -1 )
            {
                Category cat1 = new Category { CategoryName = pCat, IsPrimaryCategory = true };
                db.Categories.Add (cat1);
                db.SaveChanges ();
                CatIdList.Add (cat1.CategoryId);
            }
            else if ( id > 0 )
            {
                CatIdList.Add (id);
            }
            else
            { }

            id = 0;
            id = (int?) db.Categories.Where (c => c.CategoryName == sCat && c.IsSecondaryCategory).FirstOrDefault ().CategoryId ?? -1;
            if ( id == -1 )
            {
                Category cat2 = new Category { CategoryName = sCat, IsSecondaryCategory = true };
                db.Categories.Add (cat2);
                db.SaveChanges ();
                CatIdList.Add (cat2.CategoryId);
            }
            else if ( id > 0 )
            {
                CatIdList.Add (id);
            }
            else
            { }
            id = 0;

            id = (int?) db.Categories.Where (c => c.CategoryName == tCat && c.IsPrimaryCategory && !c.IsSecondaryCategory).FirstOrDefault ().CategoryId ?? -1;
            if ( id == -1 )
            {
                Category cat3 = new Category { CategoryName = tCat };
                db.Categories.Add (cat3);
                db.SaveChanges ();
                CatIdList.Add (cat3.CategoryId);
            }
            else if ( id > 0 )
            {
                CatIdList.Add (id);
            }
            else
            { }
            id = 0;

            return CatIdList;
        }

        private int GetBrandID(VoyagerContext db, string code)
        {
            //TODO: Null Object is found
            //TODO: create if not exsits
            try
            {
                int ids = (int?) db.Brands.Where (c => c.BCode == code).FirstOrDefault ().BrandId ?? -1;
                return ids;
            }
            catch ( Exception ex )
            {
                Brand brand = new Brand
                {
                    BCode = code,
                    BrandName = code
                };
                db.Brands.Add (brand);
                db.SaveChanges ();
                return brand.BrandId;
            }
        }

        private int GetBrand(VoyagerContext db, string StyleCode)
        {
            if ( StyleCode.StartsWith ("U") )
            {
                //USPAit
                return GetBrandID (db, "USPA");
            }
            else if ( StyleCode.StartsWith ("AR") )
            {
                //Arvind RTW
                return GetBrandID (db, "RTW");
            }
            else if ( StyleCode.StartsWith ("A") )
            {
                //Arrow
                return GetBrandID (db, "ARW");
            }
            else if ( StyleCode.StartsWith ("FM") )
            {
                //FM
                return GetBrandID (db, "FM");
            }
            else
            {
                // Arvind Store
                return GetBrandID (db, "AS");
            }
        }

        private int GetSupplierIdOrAdd(VoyagerContext db, string sup)
        {
            try
            {
                int ids = (int?) db.Suppliers.Where (c => c.SuppilerName == sup).FirstOrDefault ().SupplierID ?? -1;
                if ( ids > 0 )
                    return ids;
                else if ( ids == -1 )
                {
                    Supplier supplier = new Supplier
                    {
                        SuppilerName = sup,
                        Warehouse = sup
                    };
                    db.Suppliers.Add (supplier);
                    db.SaveChanges ();
                    return supplier.SupplierID;
                }
                else
                    return 1;// Suspense Supplier
            }
            catch ( Exception )
            {
                Supplier supplier = new Supplier
                {
                    SuppilerName = sup,
                    Warehouse = sup
                };
                db.Suppliers.Add (supplier);
                db.SaveChanges ();
                return supplier.SupplierID;
            }
        }

        #endregion HelperFunctions

        #region Purchase

        // Converting purchase items to stock

        public int ProcessPurchaseInward(VoyagerContext db, DateTime inDate, bool IsLocal)
        {
            int ctr = 0;
            var data = db.ImportPurchases.Where (c => c.IsDataConsumed == false && ( c.GRNDate.Date ) == ( inDate.Date )).OrderBy (c => c.InvoiceNo).ToList ();

            if ( data != null && data.Count > 0 )
            {
                ProductPurchase PurchasedProduct = null;

                foreach ( var item in data )
                {
                    int pid = CreateProductItem (db, item);
                    if ( pid != -999 )
                        CreateStockItem (db, item, pid);
                    PurchasedProduct = CreatePurchaseInWard (db, item, PurchasedProduct);
                    PurchasedProduct.PurchaseItems.Add (CreatePurchaseItem (db, item, pid, IsLocal));
                    item.IsDataConsumed = true;
                    db.Entry (item).State = EntityState.Modified;
                    ctr++;
                }
                if ( PurchasedProduct != null )
                    db.ProductPurchases.Add (PurchasedProduct);
                db.SaveChanges ();
            }
            return ctr;
        }

        public int CreateProductItem(VoyagerContext db, ImportPurchase purchase)
        {
            int barc = db.ProductItems.Where (c => c.Barcode == purchase.Barcode).Count ();

            if ( barc <= 0 )
            {
                ProductItem item = new ProductItem
                {
                    Barcode = purchase.Barcode,
                    Cost = purchase.Cost,
                    MRP = purchase.MRP,
                    StyleCode = purchase.StyleCode,
                    ProductName = purchase.ProductName,
                    ItemDesc = purchase.ItemDesc,
                    BrandId = GetBrand (db, purchase.StyleCode),
                };

                //splinting ProductName
                string [] PN = purchase.ProductName.Split ('/');

                // Apparel / Work / Blazers
                if ( PN [0] == "Apparel" )
                {
                    item.Units = Units.Pcs;
                    item.Categorys = ProductCategorys.ReadyMade;
                }
                else if ( PN [0] == "Suiting" || PN [0] == "Shirting" )
                {
                    item.Units = Units.Meters;
                    item.Categorys = ProductCategorys.Fabric;
                }
                else if ( PN [0] == "Tailoring" )
                {
                    item.Units = Units.Nos;
                    item.Categorys = ProductCategorys.Tailoring;
                }
                //TODO: include promo items and suit cover
                else
                {
                    item.Units = Units.Nos;
                    item.Categorys = ProductCategorys.Others; //TODO: For time being
                }

                List<Category> catIds = GetCategory (db, PN [0], PN [1], PN [2]);

                item.MainCategory = catIds [0];
                item.ProductCategory = catIds [1];
                item.ProductType = catIds [2];

                db.ProductItems.Add (item);
                db.SaveChanges ();
                return item.ProductItemId;
            }
            else if ( barc > 0 )
            {
                barc = db.ProductItems.Where (c => c.Barcode == purchase.Barcode).First ().ProductItemId;

                return barc;
                // Already Added
            }
            else
            {
                return -999;//TODO: Handel this options
                            // See ever here come.
            }
        }

        public void CreateStockItem(VoyagerContext db, ImportPurchase purchase, int pItemId)
        {
            Stock stcks = db.Stocks.Where (c => c.ProductItemId == pItemId).FirstOrDefault ();
            if ( stcks != null )
            {
                stcks.PurchaseQty += purchase.Quantity;
                stcks.Quantity += purchase.Quantity;
                db.Entry (stcks).State = EntityState.Modified;
            }
            else
            {
                Stock stock = new Stock
                {
                    PurchaseQty = purchase.Quantity,
                    Quantity = purchase.Quantity,
                    ProductItemId = pItemId,
                    SaleQty = 0,
                    Units = db.ProductItems.Find (pItemId).Units,
                    StoreId = StoreID
                };
                db.Stocks.Add (stock);
            }
            db.SaveChanges ();
        }

        public ProductPurchase CreatePurchaseInWard(VoyagerContext db, ImportPurchase purchase, ProductPurchase product)
        {
            if ( product != null )
            {
                if ( purchase.InvoiceNo == product.InvoiceNo )
                {
                    product.TotalAmount += ( purchase.CostValue + purchase.TaxAmt );
                    product.ShippingCost += 0;//TODO: add option for adding shipping cost for fabric
                    product.TotalBasicAmount += purchase.CostValue;
                    product.TotalTax += purchase.TaxAmt;
                    product.TotalQty += purchase.Quantity;
                }
                else
                {
                    db.ProductPurchases.Add (product);
                    db.SaveChanges ();
                    product = new ProductPurchase
                    {
                        InvoiceNo = purchase.InvoiceNo,
                        InWardDate = purchase.GRNDate,
                        InWardNo = purchase.GRNNo,
                        IsPaid = false,
                        PurchaseDate = purchase.InvoiceDate,
                        ShippingCost = 0,//TODO: add option for adding shipping cost for fabric
                        TotalBasicAmount = purchase.CostValue,
                        TotalTax = purchase.TaxAmt,
                        TotalQty = purchase.Quantity,
                        TotalAmount = purchase.CostValue + purchase.TaxAmt,// TODO: Check for actual DATA.
                        Remarks = "",
                        SupplierID = GetSupplierIdOrAdd (db, purchase.SupplierName),
                        StoreId = StoreID
                    };
                    product.PurchaseItems = new List<PurchaseItem> ();
                }
            }
            else
            {
                product = new ProductPurchase
                {
                    InvoiceNo = purchase.InvoiceNo,
                    InWardDate = purchase.GRNDate,
                    InWardNo = purchase.GRNNo,
                    IsPaid = false,
                    PurchaseDate = purchase.InvoiceDate,
                    ShippingCost = 0,//TODO: add option for adding shipping cost for fabric
                    TotalBasicAmount = purchase.CostValue,
                    TotalTax = purchase.TaxAmt,
                    TotalQty = purchase.Quantity,
                    TotalAmount = purchase.CostValue + purchase.TaxAmt,// TODO: Check for actual DATA.
                    Remarks = "",
                    SupplierID = GetSupplierIdOrAdd (db, purchase.SupplierName),
                    StoreId = StoreID
                };
                product.PurchaseItems = new List<PurchaseItem> ();
            }
            return product;
        }

        public PurchaseItem CreatePurchaseItem(VoyagerContext db, ImportPurchase purchase, int productId, bool IsLocal = false)
        {
            PurchaseItem item = new PurchaseItem
            {
                Barcode = purchase.Barcode,
                Cost = purchase.Cost,
                CostValue = purchase.CostValue,
                Qty = purchase.Quantity,
                TaxAmout = purchase.TaxAmt,
                Unit = db.ProductItems.Find (productId).Units,
                PurchaseTaxTypeId = CreatePurchaseTaxType (db, purchase, IsLocal),
                ProductItemId = productId
            };
            return item;
        }

        public int CreatePurchaseTaxType(VoyagerContext db, ImportPurchase purchase, bool IsLocal = false)
        {
            //Calculate tax rate
            int taxRate = 0;
            try
            {
                taxRate = (int) ( ( purchase.TaxAmt * 100 ) / purchase.CostValue );
            }
            catch ( Exception )
            {
                taxRate = 0;
            }

            //TODO:BUG:  error can come if tax amount is zero.
            if ( IsLocal )
            {
                try
                {
                    int id = db.PurchaseTaxTypes.Where (c => c.CompositeRate == taxRate && c.TaxType == TaxType.GST).Select (c => c.PurchaseTaxTypeId).FirstOrDefault ();
                    if ( id == 0 )
                    {
                        PurchaseTaxType taxType = new PurchaseTaxType { CompositeRate = taxRate, TaxType = TaxType.GST, TaxName = "Input Tax GST(SGST+CGST) @" + taxRate };
                        db.PurchaseTaxTypes.Add (taxType);
                        db.SaveChanges ();
                        return taxType.PurchaseTaxTypeId;
                    }
                    return id;
                }
                catch ( Exception )
                {
                    PurchaseTaxType taxType = new PurchaseTaxType { CompositeRate = taxRate, TaxType = TaxType.GST, TaxName = "Input Tax GST(SGST+CGST) @" + taxRate };
                    db.PurchaseTaxTypes.Add (taxType);
                    db.SaveChanges ();
                    return taxType.PurchaseTaxTypeId;
                }
            }
            else
            {
                try
                {
                    int id = db.PurchaseTaxTypes.Where (c => c.CompositeRate == taxRate && c.TaxType == TaxType.IGST).Select (c => c.PurchaseTaxTypeId).FirstOrDefault ();
                    if ( id == 0 )
                    {
                        PurchaseTaxType taxType = new PurchaseTaxType { CompositeRate = taxRate, TaxType = TaxType.IGST, TaxName = "Input Tax IGST @" + taxRate };
                        db.PurchaseTaxTypes.Add (taxType);
                        db.SaveChanges ();
                        return taxType.PurchaseTaxTypeId;
                    }
                    return id;
                }
                catch ( Exception )
                {
                    PurchaseTaxType taxType = new PurchaseTaxType { CompositeRate = taxRate, TaxType = TaxType.IGST, TaxName = "Input Tax IGST @" + taxRate };
                    db.PurchaseTaxTypes.Add (taxType);
                    db.SaveChanges ();
                    return taxType.PurchaseTaxTypeId;
                }
            }
        }

        #endregion Purchase

        #region Sale

        public int CreateSaleEntry(VoyagerContext db, DateTime onDate, AprajitaRetailsContext arDB)
        {
            int ctr = 0;
            bool isVat = false;
            if ( onDate < new DateTime (2017, 7, 1) )
            {
                isVat = true;
                return -1;// TODO: Temp implement for vat system
            }
            RegularInvoice saleInvoice = null;
            var data = db.ImportSaleItemWises.Where (c => c.IsDataConsumed == false && c.InvoiceDate.Date == onDate.Date).OrderBy (c => c.InvoiceNo).ToList ();
            if ( data != null )
            {
                foreach ( var item in data )
                {
                    saleInvoice = CreateSaleInvoice (db, arDB, item, saleInvoice);  //Create SaleInvoice
                    saleInvoice.SaleItems.Add (CreateSaleItem (db, item)); // Create SaleItems
                    ctr++;
                }
                if ( saleInvoice != null )
                {
                    db.RegularInvoices.Add (saleInvoice); // Save Last Sale Invoice
                    db.SaveChanges ();
                    CreateDailySale (arDB, db, saleInvoice); // Create DailySale Entry
                }
                return ctr;
            }
            else
            {
                return ctr;
            }
        }

        //Invoice No	Invoice Date	Invoice Type
        //Brand Name	Product Name	Item Desc
        //HSN Code	BAR CODE	Style Code	Quantity
        //MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off
        //Bill Amt	Payment Mode	SalesMan Name
        private int StockItem(VoyagerContext db, ImportSaleItemWise item, out Units unit)
        {
            ProductItem pItem = new ProductItem
            {
                Barcode = item.Barcode,
                Cost = -999,
                MRP = item.MRP,
                StyleCode = item.StyleCode,
                ProductName = item.ProductName,
                ItemDesc = item.ItemDesc,
                BrandId = GetBrand (db, item.StyleCode),
            };

            //spliting ProductName
            string [] PN = item.ProductName.Split ('/');

            // Apparel / Work / Blazers
            if ( PN [0] == "Apparel" )
            {
                pItem.Units = Units.Pcs;
                pItem.Categorys = ProductCategorys.ReadyMade;
            }
            else if ( PN [0] == "Suiting" || PN [0] == "Shirting" )
            {
                pItem.Units = Units.Meters;
                pItem.Categorys = ProductCategorys.Fabric;
            }
            else if ( PN [0] == "Tailoring" )
            {
                pItem.Units = Units.Nos;
                pItem.Categorys = ProductCategorys.Tailoring;
            }
            else
            {
                pItem.Units = Units.Nos;
                pItem.Categorys = ProductCategorys.Others; //TODO: For time being
            }

            List<Category> catIds = GetCategory (db, PN [0], PN [1], PN [2]);

            pItem.MainCategory = catIds [0];
            pItem.ProductCategory = catIds [1];
            pItem.ProductType = catIds [2];

            db.ProductItems.Add (pItem);
            db.SaveChanges ();

            unit = pItem.Units;
            return pItem.ProductItemId;
        }

        private RegularPaymentDetail CreatePaymentDetails(VoyagerContext db, ImportSaleItemWise item)
        {
            RegularPaymentDetail payment = null;
           
            if ( string.IsNullOrEmpty (item.PaymentType)  /*== null || item.PaymentType == "" */)
            {
                return null;
            }
            else if ( item.PaymentType == "CAS" )
            {
                //cash Payment
                 payment = new RegularPaymentDetail
                 {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.Cash
                };

                //return payment;
            }
            else if ( item.PaymentType == "CRD" )
            {
                payment = new RegularPaymentDetail
                {
                    CardAmount = item.BillAmnt,
                    PayMode = SalePayMode.Card
                };
                //Mix Payment
                //return payment;
            }
            else if ( item.PaymentType == "MIX" )
            {
                payment = new RegularPaymentDetail
                {
                    MixAmount = item.BillAmnt,
                    PayMode = SalePayMode.Mix
                };
                //CASH
                //return payment;
            }
            else if ( item.PaymentType == "SR" )
            {
                payment = new RegularPaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.SR
                };
              //  return payment;
            }
            //else
                return payment;
        }

        private RegularInvoice CreateSaleInvoice(VoyagerContext db, AprajitaRetailsContext arDB, ImportSaleItemWise item, RegularInvoice invoice)
        {
            if ( invoice != null )
            {
                if ( invoice.InvoiceNo == item.InvoiceNo )
                {
                    //invoice.InvoiceNo = item.InvoiceNo;
                    //invoice.OnDate = item.InvoiceDate;
                    invoice.TotalDiscountAmount += item.Discount;
                    invoice.TotalBillAmount += item.LineTotal;   // Check for if it is empty
                    invoice.TotalItems += 1;//TODO: Check for count
                    invoice.TotalQty += item.Quantity;
                    invoice.RoundOffAmount += item.RoundOff;
                    invoice.TotalTaxAmount += ( item.SGST + item.CGST ); //TODO: Check Future make it tax
                    invoice.StoreId = item.StoreId;
                    if ( invoice.PaymentDetail == null )
                        invoice.PaymentDetail = CreatePaymentDetails (db, item); //Create Payment details
                    invoice.CustomerId = GetCustomerId (db, item);  // CustomerId
                }
                else
                {
                    db.RegularInvoices.Add (invoice);
                    db.SaveChanges ();
                    CreateDailySale (arDB, db, invoice);

                    invoice = new RegularInvoice
                    {
                        InvoiceNo = item.InvoiceNo,
                        OnDate = item.InvoiceDate,
                        TotalDiscountAmount = item.Discount,
                        TotalBillAmount = item.LineTotal,
                        TotalItems = 1,//TODO: Check for count
                        TotalQty = item.Quantity,
                        RoundOffAmount = item.RoundOff,
                        TotalTaxAmount = ( item.SGST + item.CGST ), //TODO: Check
                        PaymentDetail = CreatePaymentDetails (db, item),
                        CustomerId = GetCustomerId (db, item),
                        StoreId = item.StoreId,
                        SaleItems = new List<RegularSaleItem> ()
                    };
                }
            }
            else
            {
                invoice = new RegularInvoice
                {
                    InvoiceNo = item.InvoiceNo,
                    OnDate = item.InvoiceDate,
                    TotalDiscountAmount = item.Discount,
                    TotalBillAmount = item.LineTotal,
                    TotalItems = 1,//TODO: Check for count
                    TotalQty = item.Quantity,
                    RoundOffAmount = item.RoundOff,
                    TotalTaxAmount = ( item.SGST + item.CGST ), //TODO: Check
                    PaymentDetail = CreatePaymentDetails (db, item),
                    CustomerId = GetCustomerId (db, item),
                    StoreId = item.StoreId,
                    SaleItems = new List<RegularSaleItem> ()
                };
            }

            return invoice;
        }

        private RegularSaleItem CreateSaleItem(VoyagerContext db, ImportSaleItemWise item)
        {
            var pi = db.ProductItems.Where (c => c.Barcode == item.Barcode).Select (c => new { c.ProductItemId, c.Units }).FirstOrDefault ();
            if ( pi == null )
            {
                //TODO: Handle for ProductItem Doesn't Exists.
                //create item and stock
                int id = StockItem (db, item, out Units UNTS);
                pi = new { ProductItemId = id, Units = UNTS };
            }

            RegularSaleItem saleItem = new RegularSaleItem
            {
                BarCode = item.Barcode,
                MRP = item.MRP,
                BasicAmount = item.BasicRate,
                Discount = item.Discount,
                Qty = item.Quantity,
                TaxAmount = item.SGST + item.CGST,
                BillAmount = item.LineTotal,
                Units = pi.Units,
                ProductItemId = pi.ProductItemId,
                SalesPersonId = GetSalesPersonId (db, item.Saleman),
                SaleTaxTypeId = CreateSaleTax (db, item)
            };
            if ( !SalePurchaseManager.UpDateStock (db, pi.ProductItemId, item.Quantity, false, item.StoreId) )
            {
                //TODO: Create Stock and update
                CreateStockItem (db, saleItem.Qty, saleItem.ProductItemId, saleItem.Units);
            }
            item.IsDataConsumed = true;
            db.Entry (item).State = EntityState.Modified;
            return saleItem;
        }

        /// <summary>
        /// For Creating Stock list for Sale . which can be later adjusted
        /// </summary>
        /// <param name="db">Database Context</param>
        /// <param name="qty"> Sale Qty</param>
        /// <param name="pItemId">Item Code</param>
        /// <param name="unts">Units</param>
        private void CreateStockItem(VoyagerContext db, double qty, int pItemId, Units unts)
        {
            Stock stock = new Stock
            {
                PurchaseQty = 0,
                Quantity = 0 - qty,
                ProductItemId = pItemId,
                SaleQty = qty,
                Units = unts
            };
            db.Stocks.Add (stock);

            db.SaveChanges ();
        }

        private int CreateSaleTax(VoyagerContext db, ImportSaleItemWise item)
        {
            // Always GST and with Local Sale

            //Calculate Rate
            decimal rate = 0;
            try
            {
                rate = ( ( item.SGST + item.CGST ) * 100 ) / item.BasicRate;
            }
            catch ( DivideByZeroException )
            {
                rate = 0;
            }

            int taxId = 1;

            try
            {
                taxId = db.SaleTaxTypes.Where (c => c.CompositeRate == rate).FirstOrDefault ().SaleTaxTypeId;
                return taxId;
            }
            catch ( Exception )
            {
                SaleTaxType taxType = new SaleTaxType { CompositeRate = rate, TaxName = "OutPut Tax  GST(CGST+SGST) @" + rate, TaxType = TaxType.GST };
                db.SaleTaxTypes.Add (taxType);
                db.SaveChanges ();
                return taxType.SaleTaxTypeId;
            }
        }

        #endregion Sale

        #region DailySale

        private void CreateDailySale(AprajitaRetailsContext db, VoyagerContext voyager, RegularInvoice inv)
        {
            var dSale = db.DailySales.Where (c => c.InvNo == inv.InvoiceNo).FirstOrDefault ();
            if ( dSale != null )
            {
                dSale.IsMatchedWithVOy = true;
                if ( dSale.Amount != inv.TotalBillAmount )
                {
                    dSale.Remarks += "Amount doesn't match";
                    dSale.IsMatchedWithVOy = false;
                    db.Entry (dSale).State = EntityState.Modified;
                }

                if ( dSale.PayMode == PayModes.Cash )
                {
                    if ( inv.PaymentDetail.PayMode != SalePayMode.Cash )
                    {
                        dSale.Remarks += "payment mode doesn't match";
                        dSale.IsMatchedWithVOy = false;
                        db.Entry (dSale).State = EntityState.Modified;
                    }

                    if ( dSale.CashAmount != inv.TotalBillAmount )
                    {
                        dSale.Remarks += "cash amount doesn't match";
                        dSale.IsMatchedWithVOy = false;
                        db.Entry (dSale).State = EntityState.Modified;
                    }
                }
                else if ( dSale.PayMode == PayModes.Card )
                {
                    if ( inv.PaymentDetail.PayMode != SalePayMode.Card )
                    {
                        dSale.Remarks += "payment mode doesn't match";
                        dSale.IsMatchedWithVOy = false;
                        db.Entry (dSale).State = EntityState.Modified;
                    }
                }

                if ( dSale.SaleDate != inv.OnDate )
                {
                    dSale.Remarks += "Date doesn't match";
                    dSale.IsMatchedWithVOy = false;
                    db.Entry (dSale).State = EntityState.Modified;
                }
            }
            else
            {
                DailySale dailySale = new DailySale
                {
                    Amount = inv.TotalBillAmount,
                    InvNo = inv.InvoiceNo,
                    SaleDate = inv.OnDate,
                    IsDue = false,
                    IsManualBill = false,
                    Remarks = "AutoGenerated",
                    CashAmount = 0,
                    IsMatchedWithVOy = true,
                    IsSaleReturn = false,
                    IsTailoringBill = false
                };
                dailySale.SalesmanId = db.Salesmen.Where (c => c.SalesmanName == inv.SaleItems.First ().Salesman.SalesmanName).Select (c => c.SalesmanId).FirstOrDefault ();
                if ( dailySale.SalesmanId <= 0 )
                {
                    dailySale.SalesmanId = CreateSalesman (db, inv.SaleItems.First ().Salesman.SalesmanName);
                }
                // Payment Mode.
                if ( inv.PaymentDetail.PayMode == SalePayMode.Cash )
                {
                    dailySale.PayMode = PayModes.Cash;
                    dailySale.CashAmount = inv.TotalBillAmount;
                }
                else if ( inv.PaymentDetail.PayMode == SalePayMode.Card )
                {
                    dailySale.PayMode = PayModes.Card;
                }
                else if ( inv.PaymentDetail.PayMode == SalePayMode.Mix )
                {
                    dailySale.PayMode = PayModes.MixPayments;
                    dailySale.Remarks += " Mix Payments. Update based on real Data";
                }
                else if ( inv.PaymentDetail.PayMode == SalePayMode.SR )
                { // Sale Return flag is marked
                    dailySale.PayMode = PayModes.Cash;
                    dailySale.CashAmount = inv.TotalBillAmount;
                    dailySale.IsSaleReturn = true;
                }
                //IsTailoring
                if ( inv.SaleItems.First ().ProductItem != null && inv.SaleItems.First ().ProductItem.Categorys == ProductCategorys.Tailoring )
                {
                    dailySale.IsTailoringBill = true;
                }
                else if ( inv.SaleItems.First ().ProductItem == null )
                {
                    if ( voyager.ProductItems.Find (inv.SaleItems.First ().ProductItemId).Categorys == ProductCategorys.Tailoring )
                    {
                        dailySale.IsTailoringBill = true;
                    }
                }
                db.DailySales.Add (dailySale);
            }
            db.SaveChanges ();
        }

        private int CreateSalesman(AprajitaRetailsContext db, string name)
        {
            Salesman sm = new Salesman { SalesmanName = name };
            db.Salesmen.Add (sm);
            db.SaveChanges ();
            return sm.SalesmanId;
        }

        #endregion DailySale
    }

    //Table for processing imported data
    internal class PurchaseProcess
    {
        public int PurchaseProcessId { get; set; }
        public string RefId { get; set; }
        public bool IsStockCreated { get; set; }
        public bool IsItemCreated { get; set; }
        public bool IsPurchaseEntry { get; set; }
        public bool IsAccoutingEntry { get; set; }
    }

    public static class SalePurchaseManager
    {
        /// <summary>
        /// UpDate Stock when Sale or Purchase Happen
        /// </summary>
        /// <param name="db"></param>
        /// <param name="ItemCode"></param>
        /// <param name="Qty"></param>
        /// <param name="IsPurchased"></param>
        /// <returns></returns>
        public static bool UpDateStock(VoyagerContext db, int ItemCode, double Qty, bool IsPurchased, int StoreId)
        {
            var stock = db.Stocks.Where (c => c.ProductItemId == ItemCode && c.StoreId == StoreId).FirstOrDefault ();

            if ( stock != null )
            {
                if ( IsPurchased )
                {
                    // Purchase Stock;
                    stock.PurchaseQty += Qty;
                    stock.Quantity += Qty;
                }
                else
                {
                    //Sale Stock.
                    stock.SaleQty += Qty;
                    stock.Quantity -= Qty;
                }
                db.Entry (stock).State = EntityState.Modified;

                return true;
            }
            else
                return false;
        }
    }

    #region VatSalePurchase

    public class VatSalePurchase
    {
        public int GetSalesPersonId(VoyagerContext db, string salesman)
        {
            try
            {
                var id = db.SalesPerson.Where (c => c.SalesmanName == salesman).FirstOrDefault ().SalesPersonId;
                if ( id > 0 )
                {
                    return id;
                }
                else
                {
                    SalesPerson sm = new SalesPerson { SalesmanName = salesman };
                    db.SalesPerson.Add (sm);
                    db.SaveChanges ();
                    return sm.SalesPersonId;
                }
            }
            catch ( Exception )
            {
                SalesPerson sm = new SalesPerson { SalesmanName = salesman };
                db.SalesPerson.Add (sm);
                db.SaveChanges ();
                return sm.SalesPersonId;
            }
        }

        public int GetCustomerId(VoyagerContext db, ImportSaleItemWise item)
        {
            return 1;
        }

        private RegularPaymentDetail CreatePaymentDetails(VoyagerContext db, ImportSaleItemWise item)
        {
            RegularPaymentDetail payment = null;
            if ( string.IsNullOrEmpty (item.PaymentType)/* item.PaymentType == null || item.PaymentType == "" */)
            {
                return null;
            }
            else if ( item.PaymentType == "CAS" )
            {
                //cash Payment
                payment = new RegularPaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.Cash
                };

               // return payment;
            }
            else if ( item.PaymentType == "CRD" )
            {
                payment = new RegularPaymentDetail
                {
                    CardAmount = item.BillAmnt,
                    PayMode = SalePayMode.Card
                };
                //Mix Payment
                //return payment;
            }
            else if ( item.PaymentType == "MIX" )
            {
                payment = new RegularPaymentDetail
                {
                    MixAmount = item.BillAmnt,
                    PayMode = SalePayMode.Mix
                };
                //CASH
              //  return payment;
            }
            else if ( item.PaymentType == "SR" )
            {
                payment = new RegularPaymentDetail
                {
                    CashAmount = item.BillAmnt,
                    PayMode = SalePayMode.SR
                };
               // return payment;
            }
           // else
                return payment;
        }

        private RegularInvoice CreateSaleInvoice(VoyagerContext db, ImportSaleItemWise item, RegularInvoice invoice)
        {
            if ( invoice != null )
            {
                if ( invoice.InvoiceNo == item.InvoiceNo )
                {
                    // invoice.InvoiceNo = item.InvoiceNo;
                    //invoice.OnDate = item.InvoiceDate;
                    invoice.TotalDiscountAmount += item.Discount;
                    invoice.TotalBillAmount += item.LineTotal;
                    invoice.TotalItems += 1;//TODO: Check for count
                    invoice.TotalQty += item.Quantity;
                    invoice.RoundOffAmount += item.RoundOff;
                    invoice.TotalTaxAmount += item.SGST; //TODO: Check
                    invoice.StoreId = item.StoreId;
                    invoice.PaymentDetail = CreatePaymentDetails (db, item);
                    invoice.CustomerId = GetCustomerId (db, item);
                }
                else
                {
                    db.RegularInvoices.Add (invoice);
                    db.SaveChanges ();

                    invoice = new RegularInvoice
                    {
                        InvoiceNo = item.InvoiceNo,
                        OnDate = item.InvoiceDate,
                        TotalDiscountAmount = item.Discount,
                        TotalBillAmount = item.LineTotal,
                        TotalItems = 1,//TODO: Check for count
                        TotalQty = item.Quantity,
                        RoundOffAmount = item.RoundOff,
                        TotalTaxAmount = item.SGST, //TODO: Check
                        PaymentDetail = CreatePaymentDetails (db, item),
                        CustomerId = GetCustomerId (db, item),
                        StoreId=item.StoreId,
                        SaleItems = new List<RegularSaleItem> ()
                    };
                }
            }
            else
            {
                invoice = new RegularInvoice
                {
                    InvoiceNo = item.InvoiceNo,
                    OnDate = item.InvoiceDate,
                    TotalDiscountAmount = item.Discount,
                    TotalBillAmount = item.LineTotal,
                    TotalItems = 1,//TODO: Check for count
                    TotalQty = item.Quantity,
                    RoundOffAmount = item.RoundOff,
                    TotalTaxAmount = item.SGST, //TODO: Check
                    PaymentDetail = CreatePaymentDetails (db, item),
                    CustomerId = GetCustomerId (db, item),
                    StoreId=item.StoreId,
                    SaleItems = new List<RegularSaleItem> ()
                };
            }

            return invoice;
        }

        private RegularSaleItem CreateSaleItem(VoyagerContext db, ImportSaleItemWise item, int StoreId)
        {
            var pi = db.ProductItems.Where (c => c.Barcode == item.Barcode).Select (c => new { c.ProductItemId, c.Units }).FirstOrDefault ();

            RegularSaleItem saleItem = new RegularSaleItem
            {
                BarCode = item.Barcode,
                MRP = item.MRP,
                BasicAmount = item.BasicRate,
                Discount = item.Discount,
                Qty = item.Quantity,
                TaxAmount = item.SGST,
                BillAmount = item.LineTotal,
                Units = pi.Units,
                ProductItemId = pi.ProductItemId,
                SalesPersonId = GetSalesPersonId (db, item.Saleman),
                SaleTaxTypeId = CreateSaleTax (db, item)
            };
            SalePurchaseManager.UpDateStock (db, pi.ProductItemId, item.Quantity, false, StoreId);// TODO: Check for this working
            return saleItem;
        }

        private int CreateSaleTax(VoyagerContext db, ImportSaleItemWise item, bool isIGST = false)
        {
            if ( item.Tax != 0 && item.SGST != 0 )
            {
                //GST Bill
            }
            else if ( item.Tax == 0 && item.SGST == 0 )
            {
                //TODO: Tax implementation
            }
            else
            {
            }

            return 1;
        }
    }

    #endregion VatSalePurchase
}