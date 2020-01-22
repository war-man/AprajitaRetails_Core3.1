﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToExcel.Attributes;

namespace AprajitaRetails.Areas.Uploader.Models
{

    public class ImportInWard
    {
        //Inward No	Inward Date	Invoice No	Invoice Date	Party Name	Total Qty	Total MRP Value	Total Cost

        public int ImportInWardId { get; set; }

        [ExcelColumn("Inward No")]
        public string InWardNo { get; set; }

        // 4/4/2018  5:34:56 PM
        // [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Inward Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InWardDate { get; set; }

        [ExcelColumn("Invoice No")]
        public string InvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Invoice Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }

        [ExcelColumn("Party Name")]
        public string PartyName { get; set; }

        [ExcelColumn("Total Qty")]
        public decimal TotalQty { get; set; }

        [ExcelColumn("Total MRP Value")]
        public decimal TotalMRPValue { get; set; }

        [ExcelColumn("Total Cost")]
        public decimal TotalCost { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportDate { get; set; } = DateTime.Now;


    }
}
