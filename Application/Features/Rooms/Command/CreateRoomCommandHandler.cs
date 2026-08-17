using Application.Common.Exceptions;
using Application.FileUpload;
using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFileUploadService _fileUploadService;
        public CreateRoomCommandHandler(IRoomRepository roomRepository, IFileUploadService fileUploadService)
        {
            _roomRepository = roomRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            if (request.Path == null)
            {
                throw new CustomException("به عکس نیاز است");
            }

            var room = new Room()
            {
                Name = request.Name,
                Qty = request.Qty,
                Path = await _fileUploadService.UploadFileAsync(request.Path),
                HotelId = request.HotelId.Value,
                MainCapacity = request.MainCapacity,
                ExtraCapacity = request.ExtraCapacity,
                View = request.View,
            };

            _roomRepository.Add(room);
            await _roomRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;

        }
    }
}
