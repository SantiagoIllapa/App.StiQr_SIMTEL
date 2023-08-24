using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Data;


namespace StiQr_SIMTEL.Server.Context;

public partial class StiQrDbContext : IdentityDbContext<User>
{

    public StiQrDbContext(DbContextOptions<StiQrDbContext> options)
        : base(options)
    {
    }

    public DbSet<LabelQr> LabelsQr { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    



}
