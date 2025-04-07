using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Posity
{
    public int PositieId { get; set; }

    public string SoortPositie { get; set; } = null!;
}
