using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation.Enum
{
    public enum ReserveStatus : byte
    {
        [Description("آزاد")]
        Free = 0,
        [Description("رزرو")]
        Book = 1,
        [Description("کنسل")]
        Cancel = 2,
        Lock = 4,
    }
}
