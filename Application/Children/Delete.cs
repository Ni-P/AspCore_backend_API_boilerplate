using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Children
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var child = await _context.Children.FindAsync(request.Id);

                if (child == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { child = "Not found" });
                }

                _context.Remove(child);

                return await _context.SaveChangesAsync() > 0
                    ? Unit.Value : throw new Exception("Problem deleting child");
            }
        }
    }
}