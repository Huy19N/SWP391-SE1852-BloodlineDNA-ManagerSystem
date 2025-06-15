using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIGeneCare.Data;

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

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Duration> Durations { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.;database=GeneCare;User Id=sa;Password=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E50F16116FA");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Blog__UserID__4F7CD00D");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACDF818090B");

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
                .HasConstraintName("FK__Booking__Duratio__37A5467C");

            entity.HasOne(d => d.Method).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__Booking__MethodI__398D8EEE");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Booking__Service__38996AB5");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Booking__StatusI__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking__UserID__36B12243");
        });

        modelBuilder.Entity<CollectionMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__Collecti__FC681FB18BB4A81B");

            entity.ToTable("CollectionMethod");

            entity.Property(e => e.MethodId).HasColumnName("MethodID");
            entity.Property(e => e.MethodName).HasMaxLength(100);
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.DeliveryMethodId).HasName("PK__Delivery__7B03A3A29B0F2440");

            entity.ToTable("DeliveryMethod");

            entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");
            entity.Property(e => e.DeliveryMethodName).HasMaxLength(100);
        });

        modelBuilder.Entity<Duration>(entity =>
        {
            entity.HasKey(e => e.DurationId).HasName("PK__Duration__AF77E8161445EFC6");

            entity.ToTable("Duration");

            entity.Property(e => e.DurationId).HasColumnName("DurationID");
            entity.Property(e => e.DurationName).HasMaxLength(100);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6E49EAC7C");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Service).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Feedback__Servic__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__UserID__440B1D61");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A583B6AB34F");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payment__Booking__52593CB8");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEE8AE7365DBC");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.IssueAt).HasColumnType("datetime");
            entity.Property(e => e.JwtId).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(500);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__5535A963");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AA3C02C96");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.HasKey(e => e.SampleId).HasName("PK__Samples__8B99EC0A1E2086C5");

            entity.Property(e => e.SampleId).HasColumnName("SampleID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");
            entity.Property(e => e.SampleVariant).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Samples)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Samples__Booking__4AB81AF0");

            entity.HasOne(d => d.CollectByNavigation).WithMany(p => p.Samples)
                .HasForeignKey(d => d.CollectBy)
                .HasConstraintName("FK__Samples__Collect__4BAC3F29");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Samples)
                .HasForeignKey(d => d.DeliveryMethodId)
                .HasConstraintName("FK__Samples__Deliver__4CA06362");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA615320E1");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(200);
            entity.Property(e => e.ServiceType).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__ServiceP__4957584FCFC87A91");

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
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2043668296AB");

            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<TestProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__TestProc__1B39A976291D232F");

            entity.ToTable("TestProcess");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__TestProce__Booki__3F466844");

            entity.HasOne(d => d.Status).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__TestProce__Statu__412EB0B6");

            entity.HasOne(d => d.Step).WithMany(p => p.TestProcesses)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("FK__TestProce__StepI__403A8C7D");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__TestResu__976902282A8320A2");

            entity.ToTable("TestResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__TestResul__Booki__47DBAE45");
        });

        modelBuilder.Entity<TestStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__TestStep__24343337C4ABD6C5");

            entity.ToTable("TestStep");

            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.StepName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC0C342607");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341EDA859C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(150);
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
            entity.HasKey(e => e.Email).HasName("PK__VerifyEm__A9D105351E8B5452");

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
