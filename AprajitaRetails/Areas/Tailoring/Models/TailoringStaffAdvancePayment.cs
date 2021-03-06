﻿using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class TailoringStaffAdvancePayment
    {
        public int TailoringStaffAdvancePaymentId { get; set; }

        [Display(Name = "Tailor Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        public string Details { get; set; }
    }
}