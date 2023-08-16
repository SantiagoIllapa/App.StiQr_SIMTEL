using System;
using System.Collections.Generic;

namespace StiQr_SIMTEL.Server.Models;

public partial class MarkTime
{
    public int Id { get; set; }

    public int IdLabel { get; set; }

    public int IdTransaction { get; set; }

    public DateTime TimeAdmission { get; set; }

    public DateTime TimeDeparture { get; set; }

    public virtual Label IdLabelNavigation { get; set; } = null!;

    public virtual Transaction IdTransactionNavigation { get; set; } = null!;
}
