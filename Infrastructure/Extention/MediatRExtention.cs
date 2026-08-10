using Domain.BaseEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extention
{
    public static class MediatRExtention
    {
        public static async Task DispachDomainEvent(this IMediator mediator , DbContext db)
        {
            var domainEntities =db.ChangeTracker
                .Entries<BaseEntity<int>>()
                //معنی این خطو سرچ کن
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();


            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());


            foreach (var entity in domainEvents)
                await mediator.Publish(domainEvents);

        }

    }
}
