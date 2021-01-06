using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Children
{
    public class List
    {
        public class Query : IRequest<List<ChildDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ChildDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ChildDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var children = await _context.Children
                    .ToListAsync();

                return _mapper.Map<List<Child>, List<ChildDTO>>(children);
            }
        }
    }
}