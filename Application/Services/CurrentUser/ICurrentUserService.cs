using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        int? UserId { get;}
        int? HotelId { get; }
        bool IsAdmin { get;}
    }
}
