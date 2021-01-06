using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Children
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Age).NotEmpty();
                RuleFor(x => x.Birthday).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUserAccessor UserAccessor;
            private readonly DataContext _context;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                UserAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var child = new Child()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Age = request.Age,
                    Birthday = request.Birthday,
                };

                _context.Children.Add(child);

                // get current user if needed
                var user = await _context.Users.SingleOrDefaultAsync(x =>
                    x.UserName == UserAccessor.GetCurrentUsername());

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}