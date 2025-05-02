using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Plaats
{
    public int PlaatsId { get; set; }

    public string Naam { get; set; } = null!;
    public string Code { get; set; } = null!;

    public virtual ICollection<Bestemming> Bestemmings { get; set; } = new List<Bestemming>();

    public virtual ICollection<Overstap> Overstaps { get; set; } = new List<Overstap>();

    public virtual ICollection<Vertrekplaats> Vertrekplaats { get; set; } = new List<Vertrekplaats>();
}
