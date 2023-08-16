using System;
using System.Collections.Generic;

namespace StiQr_SIMTEL.Server.Models;

public partial class RolesPermission
{
    public int Id { get; set; }

    public int IdRole { get; set; }

    public int IdPermission { get; set; }

    public virtual Permission IdPermissionNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
