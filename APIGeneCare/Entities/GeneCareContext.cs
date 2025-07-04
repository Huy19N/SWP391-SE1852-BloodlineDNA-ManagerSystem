using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIGeneCare.Entities;

public partial class GeneCareContext : DbContext
{
    public GeneCareContext()
    {
    }

    public GeneCareContext(DbContextOptions<GeneCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CollectionMethod> CollectionMethods { get; set; }

    public virtual DbSet<Duration> Durations { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<KeyVersion> KeyVersions { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentIpnlog> PaymentIpnlogs { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentReturnLog> PaymentReturnLogs { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sample> Samples { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServicePrice> ServicePrices { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TestProcess> TestProcesses { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TestStep> TestSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerifyEmail> VerifyEmails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name = DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E5069090D3F");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Blog__UserID__4E88ABD4");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD3DE97FBA");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Duration).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.DurationId)
                .HasConstraintName("FK__Booking__Duratio__35BCFE0A");

            entity.HasOne(d => d.Method).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__Booking__MethodI__37A5467C");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Booking__Service__36B12243");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Booking__StatusI__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserID__34C8D9D1");
        });

        modelBuilder.Entity<CollectionMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__Collecti__FC681FB17160F283");

            entity.ToTable("CollectionMethod");

            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.MethodName).HasMaxLength(100);
        });

        modelBuilder.Entity<Duration>(entity =>
        {
            entity.HasKey(e => e.DurationId).HasName("PK__Duration__AF77E81663E035D4");

            entity.ToTable("Duration");

            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.DurationName).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF62D62D135");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Service).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Servic__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__UserID__4222D4EF");
        });

        modelBuilder.Entity<KeyVersion>(entity =>
        {
            entity.HasKey(e => e.KeyVersionId).HasName("PK__KeyVersi__94C8E3DC90C86338");

            entity.ToTable("KeyVersion");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.TmnCode).HasMaxLength(50);
            entity.Property(e => e.Version).HasMaxLength(20);

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.KeyVersions)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KeyVersio__Payme__534D60F1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC3465ADDD0D7");

            entity.ToTable("Patient");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.HasTestedDna).HasColumnName("HasTestedDNA");
            entity.Property(e => e.IdentifyId)
                .HasMaxLength(50)
                .HasColumnName("IdentifyID");
            entity.Property(e => e.Relationship).HasMaxLength(100);
            entity.Property(e => e.SampleId).HasColumnName("SampleID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Patients)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient__Booking__4AB81AF0");

            entity.HasOne(d => d.Sample).WithMany(p => p.Patients)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK__Patient__SampleI__4BAC3F29");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38F16330E3");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BankCode).HasMaxLength(50);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.OrderInfo).HasMaxLength(256);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RawData).HasMaxLength(500);
            entity.Property(e => e.ResponseCode).HasMaxLength(20);
            entity.Property(e => e.TransactionId).HasMaxLength(255);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Booking__5629CD9C");

            entity.HasOne(d => d.KeyVersion).WithMany(p => p.Payments)
                .HasForeignKey(d => d.KeyVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__KeyVers__571DF1D5");
        });

        modelBuilder.Entity<PaymentIpnlog>(entity =>
        {
            entity.HasKey(e => e.IpnlogId).HasName("PK__PaymentI__956F903E3A310F46");

            entity.ToTable("PaymentIPNLog");

            entity.Property(e => e.IpnlogId).HasColumnName("IPNLogId");
            entity.Property(e => e.ReceivedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentIpnlogs)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentIP__Payme__59FA5E80");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D3907A0A75");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EndpointUrl)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EndpointURL");
            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IconURL");
            entity.Property(e => e.MethodName).HasMaxLength(10);
        });

        modelBuilder.Entity<PaymentReturnLog>(entity =>
        {
            entity.HasKey(e => e.ReturnLogId).HasName("PK__PaymentR__AA90652E2E7A9349");

            entity.ToTable("PaymentReturnLog");

            entity.Property(e => e.ReturnedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentReturnLogs)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__Payme__5CD6CB2B");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEE8A6D00C07A");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.IssueAt).HasColumnType("datetime");
            entity.Property(e => e.JwtId).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__5FB337D6");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AE7DA6329");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.HasKey(e => e.SampleId).HasName("PK__Samples__8B99EC0A4A928A9F");

            entity.Property(e => e.SampleId).HasColumnName("SampleID");
            entity.Property(e => e.SampleName).HasMaxLength(200);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA76D7E9D8");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(200);
            entity.Property(e => e.ServiceType).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__ServiceP__4957584FC9C27A07");

            entity.ToTable("ServicePrice");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Duration).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.DurationId)
                .HasConstraintName("FK__ServicePr__Durat__2E1BDC42");

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServicePr__Servi__2D27B809");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2043F751DCE0");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<TestProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__TestProc__1B39A9765332A18E");

            entity.ToTable("TestProcess");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__TestProce__Booki__3D5E1FD2");

            entity.HasOne(d => d.Status).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__TestProce__Statu__3F466844");

            entity.HasOne(d => d.Step).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("FK__TestProce__StepI__3E52440B");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__TestResu__97690228BAC78D25");

            entity.ToTable("TestResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__TestResul__Booki__45F365D3");
        });

        modelBuilder.Entity<TestStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__TestStep__243433375AFCAF82");

            entity.ToTable("TestStep");

            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.StepName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD9FBFBB3");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D17E2956").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IdentifyId).HasColumnName("IdentifyID");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__267ABA7A");
        });

        modelBuilder.Entity<VerifyEmail>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__VerifyEm__A9D10535EC0416B2");

            entity.ToTable("VerifyEmail");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
