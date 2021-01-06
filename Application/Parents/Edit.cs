using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Parents
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command> {
            public CommandValidator(){
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Age).NotEmpty();
                RuleFor(x => x.Birthday).NotEmpty();
            }
        } 

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // handler logic
                var parent = await _context.Parents.FindAsync(request.Id);

                if (parent == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { parent = "Not found" });
                }

                parent.Name = request.Name ?? parent.Name;
                parent.Age = parent.Age;
                parent.Birthday = parent.Birthday;

                return await _context.SaveChangesAsync() > 0
                    ? Unit.Value : throw new Exception("Problem saving changes");
            }
        }
    }
}