﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.DataAnnotations.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [InverseProperty("User")]
        public List<Note> Notes { get; set; }
    }
}
