using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Parents;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Parents
{
    public class Details
    {
        public class Query : IRequest<ParentDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ParentDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ParentDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                Console.WriteLine($"id was : {request.Id}");
                var parent = await _context.Parents
                    .FindAsync(request.Id);

                if (parent == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new {parent = "Not found"});
                }

                var parentToReturn = _mapper.Map<Parent, ParentDTO>(parent);

                return parentToReturn;
            }
        }
    }
}