﻿namespace APIGeneCare.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string? FullName { get; set; }

    public string? IdentifyId { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public DateTime LastPwdChange { get; set; }

    public virtual ICollection<AccessTokenBlacklist> AccessTokenBlacklists { get; set; } = new List<AccessTokenBlacklist>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<LogLogin> LogLogins { get; set; } = new List<LogLogin>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual Role Role { get; set; } = null!;
}
