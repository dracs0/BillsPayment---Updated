using Microsoft.AspNetCore.Mvc;

namespace BillsPaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PaymentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Payments.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound(new { message = "Payment not found." });
            return Ok(payment);
        }

        [HttpGet("user/{username}")]
        public IActionResult GetByUsername(string username)
        {
            var payments = _db.Payments.Where(p => p.Username == username).ToList();
            return Ok(payments);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = new Payment
            {
                Username = request.Username,
                Bank = request.Bank,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                BillType = request.BillType
            };

            _db.Payments.Add(payment);
            _db.SaveChanges();

            var receipt = new
            {
                payment.PaymentId,
                payment.Username,
                payment.Bank,
                payment.Amount,
                payment.PaymentMethod,
                payment.BillType,
                payment.CreatedAt,
                Message = $"Successfully paid {payment.BillType} bill of {payment.Amount:C} via {payment.PaymentMethod} ({payment.Bank})."
            };

            return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, receipt);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdatePaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound(new { message = "Payment not found." });

            payment.Bank = request.Bank;
            payment.Amount = request.Amount;
            payment.PaymentMethod = request.PaymentMethod;
            payment.BillType = request.BillType;

            _db.SaveChanges();
            return Ok(new { message = "Payment updated successfully.", data = payment });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound(new { message = "Payment not found." });

            _db.Payments.Remove(payment);
            _db.SaveChanges();
            return Ok(new { message = "Payment deleted successfully." });
        }

        [HttpGet("{id}/receipt")]
        public IActionResult GetReceipt(Guid id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
                return NotFound(new { message = "Payment not found." });

            return Ok(new
            {
                payment.PaymentId,
                payment.Username,
                payment.Bank,
                payment.Amount,
                payment.PaymentMethod,
                payment.BillType,
                payment.CreatedAt,
                Message = $"Successfully paid {payment.BillType} bill of {payment.Amount:C} via {payment.PaymentMethod} ({payment.Bank})."
            });
        }
    }
}