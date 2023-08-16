using System;
using System.Collections.Generic;

namespace StiQr_SIMTEL.Server.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int IdWallet { get; set; }

    public int IdUserTransmitter { get; set; }

    public string Type { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int Amount { get; set; }

    public string? Observations { get; set; }

    public virtual User IdUserTransmitterNavigation { get; set; } = null!;

    public virtual Wallet IdWalletNavigation { get; set; } = null!;

    public virtual ICollection<MarkTime> MarkTimes { get; set; } = new List<MarkTime>();
}
