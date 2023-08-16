using System;
using System.Collections.Generic;
namespace StiQr_SIMTEL.Server.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public string Plate { get; set; } = null!;

    public string? Alias { get; set; }

    public string? Description { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
}
