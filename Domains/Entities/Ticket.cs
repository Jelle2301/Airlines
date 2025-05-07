using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Achternaam { get; set; } = null!;

    public double Prijs { get; set; }

    public int? MaaltijdId { get; set; }

    public int ReisklasseId { get; set; }

    public int? SeizoenId { get; set; }

    public int VluchtId { get; set; }

    public int ZitplaatsId { get; set; }

    public virtual ICollection<Boeking> Boekings { get; set; } = new List<Boeking>();

    public virtual Maaltijd? Maaltijd { get; set; }

    public virtual Reisklasse Reisklasse { get; set; } = null!;

    public virtual Seizoen? Seizoen { get; set; }

    public virtual Vlucht Vlucht { get; set; } = null!;

    public virtual Zitplaat Zitplaats { get; set; } = null!;
}
