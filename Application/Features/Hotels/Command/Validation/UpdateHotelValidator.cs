using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Command.Validation
{
    public class UpdateHotelValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("نام نمیتواند خالی باشد");
               
        }
    }
}
