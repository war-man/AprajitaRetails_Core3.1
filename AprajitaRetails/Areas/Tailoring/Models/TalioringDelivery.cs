﻿using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class TalioringDelivery
    {
        public int TalioringDeliveryId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Booking ID")]
        public int TalioringBookingId { get; set; }
        public TalioringBooking Booking { get; set; }

        [Display(Name = "Voy Inv No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string Remarks { get; set; }
    }
}