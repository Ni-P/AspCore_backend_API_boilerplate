using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Parents
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
                var parent = await _context.Parents.FindAsync(request.Id);

                if (parent == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { parent = "Not found" });
                }

                _context.Remove(parent);

                return await _context.SaveChangesAsync() > 0
                    ? Unit.Value : throw new Exception("Problem deleting parent");
            }
        }
    }
}