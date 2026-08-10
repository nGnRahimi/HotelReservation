using Domain.BaseEntity;
using Domain.Models.Hotels;
using Domain.Models.Roles;
using Domain.Models.Users;
using Infrastructure.Extention;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StackExchange.Redis;
using Stripe;
using System.Security.Cryptography.X509Certificates;
using Role = Domain.Models.Roles.Role;
using User = Domain.Models.Users.User;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int> , IUnitOfWork
    {

        private readonly IMediator _mediator;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Hotel> Hotels { get; set; }


        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispachDomainEvent(this);


            var date = DateTime.Now;
            var entries = ChangeTracker.Entries<IBaseEntity<int>>();

            foreach (var entry in entries)
            { 
              if (entry.State == EntityState.Added)
              {

               entry.Entity.Created = date;
                    entry.Entity.CreatedBy = 0;
              }
              if(entry.State == EntityState.Added || entry.State == EntityState.Modified)
              {
               entry.Entity.Modified = date;
                    entry.Entity.ModifiedBy = 0;

              }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );
        }
    }
}