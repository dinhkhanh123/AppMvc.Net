using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnLapTrinhWebNC.Models.Contacts
{
    public class Contact
    {
        [Key]

        public int Id { set; get; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Name")]
        public string FullName { set; get; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { set; get; }
        [StringLength(50)]
        [Phone]
        public string Phone { set; get; }
        public string Message { set; get; }
        public DateTime DateSent { set; get; }
    }
}