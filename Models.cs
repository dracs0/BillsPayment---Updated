using System.ComponentModel.DataAnnotations;

namespace BillsPaymentAPI
{
    public class Payment
    {
        public Guid PaymentId { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = "";
        public string Bank { get; set; } = "";
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string BillType { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = "";
    }

    public class CreatePaymentRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Bank is required.")]
        public string Bank { get; set; } = "";

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [RegularExpression("Credit|Debit", ErrorMessage = "PaymentMethod must be Credit or Debit.")]
        public string PaymentMethod { get; set; } = "";

        [Required(ErrorMessage = "Bill type is required.")]
        [RegularExpression("Water|Electricity|Internet", ErrorMessage = "BillType must be Water, Electricity, or Internet.")]
        public string BillType { get; set; } = "";
    }

    public class UpdatePaymentRequest
    {
        [Required(ErrorMessage = "Bank is required.")]
        public string Bank { get; set; } = "";

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [RegularExpression("Credit|Debit", ErrorMessage = "PaymentMethod must be Credit or Debit.")]
        public string PaymentMethod { get; set; } = "";

        [Required(ErrorMessage = "Bill type is required.")]
        [RegularExpression("Water|Electricity|Internet", ErrorMessage = "BillType must be Water, Electricity, or Internet.")]
        public string BillType { get; set; } = "";
    }
}