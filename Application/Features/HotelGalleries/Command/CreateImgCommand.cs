using Application.Common.MediatR;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.HotelGalleries.Command
{
    public class CreateImgCommand : BaseCommandRequest , IRequest<bool>
    {
        public IFormFile file {  get; set; }

    }
}
