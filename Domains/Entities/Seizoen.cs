using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Seizoen
{
    public double Tarief { get; set; }

    public string SoortPeriode { get; set; } = null!;

    public int SeizoenId { get; set; }

    public DateOnly BeginDatum { get; set; }

    public DateOnly EindDatum { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
