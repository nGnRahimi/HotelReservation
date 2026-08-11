using Domain.Models.HotelGalleries;
using Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HotelGalleryRepository : BaseRepository<HotelGallery, int>, IHotelGalleryRepository
    {
        public HotelGalleryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
