using System;
using System.Collections.Generic;

namespace StiQr_SIMTEL.Server.Models;

public partial class Wallet
{
    public int Id { get; set; }

    public int IdUserDriver { get; set; }

    public int Amount { get; set; }

    public bool State { get; set; }

    public virtual User IdUserDriverNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
