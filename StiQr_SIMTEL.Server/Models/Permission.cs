﻿using System;
using System.Collections.Generic;
namespace StiQr_SIMTEL.Server.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolesPermission> RolesPermissions { get; set; } = new List<RolesPermission>();
}
