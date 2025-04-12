using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Vliegtuig
{
    public int VliegtuigId { get; set; }

    public string VliegtuigNaam { get; set; } = null!;

    public int AantalTotalePlaatsen { get; set; }

    public int AantalGewonePlaatsen { get; set; }

    public int AantalBusinessPlaatsen { get; set; }

    public int AantalEconomyPlaatsen { get; set; }

    public virtual ICollection<Vlucht> Vluchts { get; set; } = new List<Vlucht>();
}
