using System;
using System.Collections.Generic;

namespace StiQr_SIMTEL.Server.Models;
public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string Cidentity { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<RolesUser> RolesUsers { get; set; } = new List<RolesUser>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
