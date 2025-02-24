using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Achternaam { get; set; } = null!;

    public double Prijs { get; set; }

    public DateOnly TijdVertrek { get; set; }

    public DateOnly TijdAankomst { get; set; }

    public int VertrekplaatsId { get; set; }

    public int BestemmingId { get; set; }

    public int? OverstapId { get; set; }

    public int? MaaltijdId { get; set; }

    public int ReisklasseId { get; set; }

    public int SeizoenId { get; set; }

    public virtual Bestemming Bestemming { get; set; } = null!;

    public virtual ICollection<Boeking> Boekings { get; set; } = new List<Boeking>();

    public virtual Maaltijd? Maaltijd { get; set; }

    public virtual Overstap? Overstap { get; set; }

    public virtual Reisklasse Reisklasse { get; set; } = null!;

    public virtual Seizoen Seizoen { get; set; } = null!;

    public virtual Vertrekplaat Vertrekplaats { get; set; } = null!;
}
