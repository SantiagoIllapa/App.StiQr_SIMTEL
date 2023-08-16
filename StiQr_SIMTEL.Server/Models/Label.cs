namespace StiQr_SIMTEL.Server.Models;

public partial class Label
{
    public int Id { get; set; }

    public int IdVehicle { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Vehicle IdVehicleNavigation { get; set; } = null!;

    public virtual ICollection<MarkTime> MarkTimes { get; set; } = new List<MarkTime>();
}
