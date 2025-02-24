using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Reisklasse
{
    public string SoortReisklasse { get; set; } = null!;

    public int ReisklasseId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
