using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Overstap
{
    public int OverstapId { get; set; }

    public int PlaatsId { get; set; }

    public int VluchtId { get; set; }

    public virtual Plaats Plaats { get; set; } = null!;

    public virtual Vlucht Vlucht { get; set; } = null!;
}
