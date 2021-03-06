﻿using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Chat.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string UserID { get; set; }
        public Message()
        {
            When = DateTime.Now;
        }
    }
}
