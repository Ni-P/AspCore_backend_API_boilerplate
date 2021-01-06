using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Parents
{
    public class List
    {
        public class Query : IRequest<List<ParentDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ParentDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ParentDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var parents = await _context.Parents
                    .ToListAsync();

                return _mapper.Map<List<Parent>, List<ParentDTO>>(parents);
            }
        }
    }
}