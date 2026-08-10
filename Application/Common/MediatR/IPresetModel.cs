using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.MediatR
{
    public interface IPresetModel
    {
        int? UserId { get; set; }
        int? HotelId { get; set; }
        bool IsAdmin {  get; set; }
    }
}
