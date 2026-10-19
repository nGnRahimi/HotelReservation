using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Command.Validation
{
    public class ReserveValidator : AbstractValidator<ReserveCommand>
    {
        public ReserveValidator()
        {
            RuleForEach(s => s.Rooms).ChildRules(room =>
            {
                room.RuleForEach(a => a.Guests)
                .SetValidator(new GuestValidator());
            });
        }
    }


    public class GuestValidator : AbstractValidator<RoomGuest>
    {
        public GuestValidator()
        {
            RuleFor(s => s.Fname)
                .NotNull().WithMessage("نام نمیتواند خالی باشد");

            RuleFor(s => s.Fname)
             .NotNull().WithMessage("نام خانوادگی نمیتواند خالی باشد");

            RuleFor(s => s.Code)
            .NotNull().WithMessage("کد نمیتواند خالی باشد");
        }
    }
}
