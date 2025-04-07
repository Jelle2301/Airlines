using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamNaam { get; set; } = null!;
}
