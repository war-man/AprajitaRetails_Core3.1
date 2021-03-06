﻿//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{

    //Banking Section
    public class Bank
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public ICollection<AccountNumber> Accounts { get; set; }
        public ICollection<BankAccountInfo> BankAccounts { get; set; }
    }


    //public enum Modes { ON, OFF, Play, Pause, Stop, Upload, Delete}

    //public class PlaySongs
    //{
    //    public int PlaySongsId { get; set; }
    //    public string SongName { get; set; }
    //    public Modes PlayModes { get; set; }
    //}

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
