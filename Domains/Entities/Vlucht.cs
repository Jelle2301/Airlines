﻿using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Vlucht
{
    public int VluchtId { get; set; }

    public double Prijs { get; set; }

    public int VertrekplaatsId { get; set; }

    public int BestemmingId { get; set; }

    public int? OverstapId { get; set; }

    public DateTime TijdVertrek { get; set; }

    public DateTime TijdAankomst { get; set; }

    public int VliegtuigId { get; set; }

    public virtual Bestemming Bestemming { get; set; } = null!;

    public virtual Overstap? Overstap { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Vertrekplaats Vertrekplaats { get; set; } = null!;

    public virtual Vliegtuig Vliegtuig { get; set; } = null!;
}
