using System;
using System.Collections.Generic;

namespace Reserva.Models;

public partial class Reserva
{
    public int? idReserva { get; set; }

    public string? Especialidad { get; set; }

    public DateTime DiaReserva { get; set; }

}