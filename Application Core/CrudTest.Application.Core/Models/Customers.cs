using System;
using System.ComponentModel.DataAnnotations;

namespace CrudTest.Application.Core.Models
{

    public partial class Customers
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(20)]
        [Key]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [Key]
        public string LastName { get; set; }
        [Required]
        [Key]
        public DateTime DateOfBirth { get; set; }
        public short? DialCode { get; set; }
        public long? PhoneNumber { get; set; }
        [EmailAddress]
        [MaxLength(20)]
        [Required]
        public string Email { get; set; }
        public long? BankAccountNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
        public string GetFullPhoneNumber()
        {
            if (DialCode != null)
                return $"+{DialCode}{PhoneNumber}";
            else return PhoneNumber?.ToString();
        }
    }
}
