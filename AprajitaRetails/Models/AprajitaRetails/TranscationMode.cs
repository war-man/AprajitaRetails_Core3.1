﻿//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    //TODO: Add Support for Mix Payment


    public class TranscationMode
    {
        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }
        //[Index(IsUnique = true)]
        [Display(Name = "Transcation Mode")]
        public string Transcation { get; set; }

        public virtual ICollection<CashReceipt> CashReceipts { get; set; }
        public virtual ICollection<CashPayment> CashPayments { get; set; }
        //Modes Name  write Seed 
        // Amit Kumar , Mukesh, HomeExp, OtherHomeExpenses,CashInOut
    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}