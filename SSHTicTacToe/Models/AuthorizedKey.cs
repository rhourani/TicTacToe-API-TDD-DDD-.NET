using System;
using System.Collections.Generic;

namespace SSHTicTacToe.Models;

public partial class AuthorizedKey
{
    public string? Options { get; set; }

    public string KeyType { get; set; } = null!;

    public string KeyData { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public int KeyId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UserId { get; set; }

    public string? HostId { get; set; }

    public DateTime? LastUsed { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public string? FingerPrint { get; set; }

    public string? Ipadresses { get; set; }

    public int Status { get; set; }
}
