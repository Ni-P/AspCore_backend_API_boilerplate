using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Children
{
    public class Details
    {
        public class Query : IRequest<ChildDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ChildDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ChildDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine($"id was : {request.Id}");
                var child = await _context.Children
                    .FindAsync(request.Id);

                if (child == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new {child = "Not found"});
                }

                var childToReturn = _mapper.Map<Child, ChildDTO>(child);

                return childToReturn;
            }
        }
    }
}