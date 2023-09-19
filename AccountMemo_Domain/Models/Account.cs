using System;
using System.Collections.Generic;

namespace AccountMemo_Domain.Models;

public class Account : BaseModel
{
    public UserStore? AccountHolder { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? AccountType { get; set; }
}
