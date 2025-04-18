using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public int VliegtuidId { get; set; }

    public string Zitnummer { get; set; } = null!;

    public int ReisklasseId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
