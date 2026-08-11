using Domain.BaseEntity;
using Domain.Models.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.HotelGalleries
{
    public class HotelGallery: BaseEntity<int>
    {
        public string Path { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

    }
}
