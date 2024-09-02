﻿using System.ComponentModel.DataAnnotations;

namespace Demomvc.Models
{
    public class Login
    {
        [Key] //primary key
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
