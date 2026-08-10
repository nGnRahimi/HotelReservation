using Domain.BaseRepository;
using Domain.Models.Hotels;
using Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HotelRepository : BaseRepository<HotelRepository, int>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
