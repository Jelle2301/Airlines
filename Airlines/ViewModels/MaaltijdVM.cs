﻿namespace Airlines.ViewModels
{
    public class MaaltijdVM
    {
        public string SoortMaaltijd { get; set; } = null!;

        public string Naam { get; set; } = null!;

        public double ExtraPrijs { get; set; }
        public int MaaltijdId { get; set; }
        public string? ExtraOmschrijving { get; set; } = null!;

    }
}
