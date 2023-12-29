﻿using System.ComponentModel.DataAnnotations;


namespace WebApp1.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreateddateTime { get; set; } = DateTime.Now;

    }
}
