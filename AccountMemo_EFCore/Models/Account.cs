using System;
using System.Collections.Generic;

namespace AccountMemo_EFCore.Models;

public partial class Account : BaseModel
{
    public string? Username { get; set; }

    public string? Password { get; set; }
    public string? AccountType { get; set; }

    public virtual UserStore? User { get; set; }

}
