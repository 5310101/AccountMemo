using System;
using System.Collections.Generic;

namespace AccountMemo_Domain.Models;

public partial class UserStore : BaseModel
{

    public string? Name { get; set; }
    public int Age { get; set; }

    public string? AppPassword { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
