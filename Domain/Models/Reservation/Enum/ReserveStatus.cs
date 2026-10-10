using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation.Enum
{
    public enum ReserveStatus : byte
    {
        Free = 0,
        Book = 1,
        Cancel = 2,
    }
}
