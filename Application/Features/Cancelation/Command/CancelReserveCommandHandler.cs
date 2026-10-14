using Application.Common.Exceptions;
using Domain.Models.Cancelation;
using Domain.Models.Reservation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cancelation.Command
{
    public class CancelReserveCommandHandler : IRequestHandler<CancelReserveCommand, int>
    {
        private readonly IReserveRepository _reserveRepository;
        private readonly ICancelationRepository _cancelationRepository;
        public CancelReserveCommandHandler(IReserveRepository reserveRepository, ICancelationRepository cancelationRepository)
        {
            _reserveRepository = reserveRepository;
            _cancelationRepository = cancelationRepository;
        }

        public async Task<int> Handle(CancelReserveCommand request, CancellationToken cancellationToken)
        {
            var reserve = await _reserveRepository.FindAsync(request.Id);
            if (reserve == null)
            {
                throw new CustomException("رزرو یافت نشد");
            }

            if (reserve.IsCanceled)
            {
                throw new CustomException("رزرو کنسل شده است ");
            }

            DateTime date = DateTime.Now;

            var policy = await _cancelationRepository.Get(a => a.Enable && a.HotelId == reserve.HotelId
            && a.From <= date && a.To > date)
                .Include(a => a.CancelationRules)
                .OrderByDescending(s => s.Created)
                .FirstOrDefaultAsync();


            if (policy == null)
            {
                throw new CustomException("قانون کنسلی یافت نشد");
            }


            var HoursBeforeCheckIn = (reserve.From.AddHours(14).AddMinutes(0).AddSeconds(0) - DateTime.Now).TotalHours;


            if (HoursBeforeCheckIn < 0)
            {
                throw new CustomException("امکان کنسلی وجود ندارد");
            }

            var Rules = policy.CancelationRules
                .OrderByDescending(s => s.Hours)
                .FirstOrDefault(s => HoursBeforeCheckIn >= s.Hours);


            if (Rules == null)
            {
                throw new CustomException("قانون کنسلی یافت نشد");
            }



            var Refund = reserve.TotalPrice * Rules.RefundPercent / 100;


            reserve.CancelAmount = Refund;
            reserve.IsCanceled = true;
            reserve.Status = Domain.Models.Reservation.Enum.ReserveStatus.Cancel;

            await _reserveRepository.UnitOfWork.SaveEntitiesAsync();


            // Add Record for refund Amount For User

            return reserve.Id;

        }
    }
}
