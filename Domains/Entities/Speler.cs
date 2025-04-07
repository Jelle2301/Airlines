using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Speler
{
    public int Id { get; set; }

    public string Naam { get; set; } = null!;

    public int Leeftijd { get; set; }

    public int Team { get; set; }

    public int Positie { get; set; }
}
