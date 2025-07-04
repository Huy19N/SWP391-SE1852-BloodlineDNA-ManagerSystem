using System;
using System.Collections.Generic;

namespace APIGeneCare.Model.DTO;

public class PaymentDTO
{
    public long PaymentId { get; set; }

    public int BookingId { get; set; }

    public long KeyVersionId { get; set; }

    public string TransactionId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public string? BankCode { get; set; }

    public string OrderInfo { get; set; } = null!;

    public string? ResponseCode { get; set; }

    public string SecureHash { get; set; } = null!;

    public string RawData { get; set; } = null!;

    public bool HavePaid { get; set; }
}
