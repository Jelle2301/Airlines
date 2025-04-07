using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Bestemming
{
    public int BestemmingId { get; set; }

    public int PlaatsId { get; set; }

    public virtual Plaats Plaats { get; set; } = null!;

    public virtual ICollection<Vlucht> Vluchts { get; set; } = new List<Vlucht>();
}
