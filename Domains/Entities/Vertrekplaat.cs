using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Vertrekplaat
{
    public int VertrekplaatsId { get; set; }

    public int PlaatsId { get; set; }

    public virtual Plaat Plaats { get; set; } = null!;

    public virtual ICollection<Vlucht> Vluchts { get; set; } = new List<Vlucht>();
}
