using Application.Common.MediatR;
using Domain.Models.HotelGalleries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.HotelGalleries.Query
{
    public class GetHotelImagesQuery : BaseQueryRequest , IRequest<List<HotelGallery>>
    {

    }
}
