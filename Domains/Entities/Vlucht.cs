using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Vlucht
{
    public int VliegtuigId { get; set; }

    public int AantalGewonePlaatsen { get; set; }

    public int AantalEconomyPlaatsen { get; set; }

    public int AantalBusinessPlaatsen { get; set; }

    public string NaamVliegtuig { get; set; } = null!;

    public double Prijs { get; set; }

    public int VertrekplaatsId { get; set; }

    public int BestemmingId { get; set; }

    public int? OverstapId { get; set; }

    public DateOnly TijdVertrek { get; set; }

    public DateOnly TijdAankomst { get; set; }

    public virtual Bestemming Bestemming { get; set; } = null!;

    public virtual Overstap? Overstap { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Vertrekplaats Vertrekplaats { get; set; } = null!;
}
