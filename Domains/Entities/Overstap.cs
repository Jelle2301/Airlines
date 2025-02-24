using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Overstap
{
    public int OverstapId { get; set; }

    public int PlaatsId { get; set; }

    public virtual Plaat Plaats { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
