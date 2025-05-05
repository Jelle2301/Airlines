using System;
using System.Collections.Generic;

namespace Domains.Entities;

public partial class Boeking
{
    public int BoekingId { get; set; }

    public int TicketId { get; set; }

    public DateOnly DatumBoeking { get; set; }

    public string VoornaamBoeking { get; set; } = null!;

    public string AchternaamBoeking { get; set; } = null!;

    public double TotalePrijs { get; set; }

    public string UserId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
