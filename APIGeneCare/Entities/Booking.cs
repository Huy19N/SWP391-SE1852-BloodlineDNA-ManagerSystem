﻿namespace APIGeneCare.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? PriceId { get; set; }

    public int? MethodId { get; set; }

    public int? ResultId { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public int? StatusId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Feedback? Feedback { get; set; }

    public virtual CollectionMethod? Method { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ServicePrice? Price { get; set; }

    public virtual TestResult? Result { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ICollection<TestProcess> TestProcesses { get; set; } = new List<TestProcess>();

    public virtual User? User { get; set; }
}
