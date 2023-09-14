using System;
using System.Collections.Generic;

namespace AccountMemo_EFCore.Models;

public partial class UserStore 
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? AppPassword { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
