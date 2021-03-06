﻿using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class DuesList
    {
        public int DuesListId { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsRecovered { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime? RecoveryDate { get; set; }
        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }

        public bool IsPartialRecovery { get; set; }

        public virtual ICollection<DueRecoverd> Recoverds { get; set; }
    }


}
