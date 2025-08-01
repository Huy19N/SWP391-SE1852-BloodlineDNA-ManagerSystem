﻿using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<AccessTokenBlacklist> AccessTokenBlacklists { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CollectionMethod> CollectionMethods { get; set; }

    public virtual DbSet<Duration> Durations { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<LogLogin> LogLogins { get; set; }

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
        modelBuilder.Entity<AccessTokenBlacklist>(entity =>
        {
            entity.HasKey(e => e.JwtId).HasName("PK__AccessTo__32FE98A232DAF29B");

            entity.ToTable("AccessTokenBlacklist");

            entity.Property(e => e.JwtId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AccessTokenBlacklists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__AccessTok__UserI__35BCFE0A");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E504E516931");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Blog__UserID__5EBF139D");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD3FC1FFFD");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Method).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__Booking__MethodI__49C3F6B7");

            entity.HasOne(d => d.Price).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PriceId)
                .HasConstraintName("FK__Booking__PriceID__48CFD27E");

            entity.HasOne(d => d.Result).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ResultId)
                .HasConstraintName("FK__Booking__ResultI__4AB81AF0");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Booking__StatusI__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserID__47DBAE45");
        });

        modelBuilder.Entity<CollectionMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__Collecti__FC681FB14107A6CD");

            entity.ToTable("CollectionMethod");

            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.MethodName).HasMaxLength(100);
        });

        modelBuilder.Entity<Duration>(entity =>
        {
            entity.HasKey(e => e.DurationId).HasName("PK__Duration__AF77E8164FF270B7");

            entity.ToTable("Duration");

            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.DurationName).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Feedback__73951ACDB7DB0E37");

            entity.ToTable("Feedback");

            entity.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("BookingID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithOne(p => p.Feedback)
                .HasForeignKey<Feedback>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Booking");
        });

        modelBuilder.Entity<LogLogin>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogLogin__5E548648F41311ED");

            entity.ToTable("LogLogin");

            entity.Property(e => e.FailReason).HasMaxLength(255);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(255)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.RefreshToken).WithMany(p => p.LogLogins)
                .HasForeignKey(d => d.RefreshTokenId)
                .HasConstraintName("FK__LogLogin__Refres__30F848ED");

            entity.HasOne(d => d.User).WithMany(p => p.LogLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__LogLogin__UserID__300424B4");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC346C13D637D");

            entity.ToTable("Patient");

            entity.Property(e => e.PatientId).HasColumnName("PatientID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.HasTestedDna).HasColumnName("HasTestedDNA");
            entity.Property(e => e.IdentifyId)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("IdentifyID");
            entity.Property(e => e.Relationship).HasMaxLength(100);
            entity.Property(e => e.SampleId).HasColumnName("SampleID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Patients)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Patient__Booking__5AEE82B9");

            entity.HasOne(d => d.Sample).WithMany(p => p.Patients)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FK__Patient__SampleI__5BE2A6F2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38017FA17B");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BankTranNo).HasMaxLength(50);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.OrderInfo).HasMaxLength(256);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.ResponseCode).HasMaxLength(50);
            entity.Property(e => e.TransactionNo).HasMaxLength(255);
            entity.Property(e => e.TransactionStatus).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Payment__Booking__6383C8BA");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Payment__6477ECF3");
        });

        modelBuilder.Entity<PaymentIpnlog>(entity =>
        {
            entity.HasKey(e => e.IpnlogId).HasName("PK__PaymentI__956F903E4036C033");

            entity.ToTable("PaymentIPNLog");

            entity.Property(e => e.IpnlogId).HasColumnName("IPNLogId");
            entity.Property(e => e.PaymentId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ReceivedAt).HasColumnType("datetime");
            entity.Property(e => e.ResponseCode).HasMaxLength(50);
            entity.Property(e => e.TransactionStatus).HasMaxLength(50);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentIpnlogs)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentIP__Payme__6754599E");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D3D29C2DFC");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId).ValueGeneratedNever();
            entity.Property(e => e.IconUrl)
                .IsUnicode(false)
                .HasColumnName("IconURL");
            entity.Property(e => e.MethodName).HasMaxLength(10);
        });

        modelBuilder.Entity<PaymentReturnLog>(entity =>
        {
            entity.HasKey(e => e.ReturnLogId).HasName("PK__PaymentR__AA90652E0F245207");

            entity.ToTable("PaymentReturnLog");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ResponseCode).HasMaxLength(50);
            entity.Property(e => e.ReturnedAt).HasColumnType("datetime");
            entity.Property(e => e.TransactionStatus).HasMaxLength(50);

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentReturnLogs)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__Payme__6A30C649");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__F5845E39D015702D");

            entity.ToTable("RefreshToken");

            entity.HasIndex(e => e.Token, "UQ__RefreshT__1EB4F81759B4672E").IsUnique();

            entity.HasIndex(e => e.JwtId, "UQ__RefreshT__32FE98A38C2601FF").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(255)
                .HasColumnName("IPAddress");
            entity.Property(e => e.JwtId).HasMaxLength(100);
            entity.Property(e => e.Token).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RefreshTo__UserI__2C3393D0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A819925CC");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.HasKey(e => e.SampleId).HasName("PK__Samples__8B99EC0A5ABF90B4");

            entity.Property(e => e.SampleId).HasColumnName("SampleID");
            entity.Property(e => e.SampleName).HasMaxLength(200);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EAEF0D1D83");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(200);
            entity.Property(e => e.ServiceType).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__ServiceP__4957584F0C1A42B3");

            entity.ToTable("ServicePrice");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Duration).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.DurationId)
                .HasConstraintName("FK__ServicePr__Durat__3E52440B");

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServicePr__Servi__3D5E1FD2");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2043F476F808");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<TestProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__TestProc__1B39A9768F487371");

            entity.ToTable("TestProcess");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TestProce__Booki__5441852A");

            entity.HasOne(d => d.Status).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__TestProce__Statu__5629CD9C");

            entity.HasOne(d => d.Step).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("FK__TestProce__StepI__5535A963");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__TestResu__97690228C2EF167E");

            entity.ToTable("TestResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.Date).HasColumnType("datetime");
        });

        modelBuilder.Entity<TestStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__TestStep__243433376DD1CE85");

            entity.ToTable("TestStep");

            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.StepName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC1DA73615");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D8C6C122").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IdentifyId)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("IdentifyID");
            entity.Property(e => e.LastPwdChange)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__267ABA7A");
        });

        modelBuilder.Entity<VerifyEmail>(entity =>
        {
            entity.HasKey(e => new { e.Otp, e.Email }).HasName("PK__VerifyEm__81A4138BE55180F2");

            entity.ToTable("VerifyEmail");

            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("OTP");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
