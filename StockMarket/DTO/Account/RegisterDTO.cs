﻿using System.ComponentModel.DataAnnotations;

namespace StockMarket.DTO.Account
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName {  get; set; }
        [Required]
        [EmailAddress]
        public string? Email {  get; set; }

        [Required] 
        public string? Password { get; set; }
    }
}
