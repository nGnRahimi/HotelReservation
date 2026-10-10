using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public class ReserveGuest : BaseEntity<int>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string NationalCode { get; set; }

        public int ReserveDetailId { get; set; }
        public ReserveDetail ReserveDetail { get; set; }
    }
}
