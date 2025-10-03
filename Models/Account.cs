using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Account number is required")]
        [Display(Name = "Account Number")]
        [StringLength(20)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account holder name is required")]
        [Display(Name = "Account Holder Name")]
        [StringLength(100)]
        public string AccountHolderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account type is required")]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Balance")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be positive")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Balance { get; set; }

        [Display(Name = "Date Opened")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOpened { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public bool IsActive { get; set; } = true;
    }
}