using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public string Zitnummer { get; set; } = null!;

    public int VluchtId { get; set; }

    public bool IsBezet { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Vlucht Vlucht { get; set; } = null!;
}
