using System;
using System.Collections.Generic;

namespace AccountMemo_EFCore.Models;

public partial class Account
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
    public string? AccountType { get; set; }

    public virtual UserStore? User { get; set; }

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}
