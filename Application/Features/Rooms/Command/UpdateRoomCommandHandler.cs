using Application.Common.Exceptions;
using Application.FileUpload;
using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Command
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IFileUploadService _fileUploadService;
        public UpdateRoomCommandHandler(IRoomRepository roomRepository, IFileUploadService fileUploadService)
        {
            _roomRepository = roomRepository;
            _fileUploadService = fileUploadService;
        }
        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindAsync(request.Id);
            if (room == null)
            {
                throw new CustomException("داده یافت نشد");
            }

            if (request.Path != null)
            {
                room.Path = await _fileUploadService.UploadFileAsync(request.Path);
            }

            room.Name = request.Name;
            room.View = request.View;
            room.Qty = request.Qty;
            room.MainCapacity = request.MainCapacity;
            room.ExtraCapacity = request.ExtraCapacity;





            _roomRepository.Update(room);
            await _roomRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;




        }
    }
}
