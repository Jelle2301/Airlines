using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Maaltijd
{
    public string Soort { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public double Prijs { get; set; }

    public int MaaltijdId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
