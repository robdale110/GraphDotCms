using AutoMapper;
using GraphDotCms.Application.Interfaces;
using GraphDotCms.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GraphDotCms.Application.Values
{
    public class GetValues
    {
        public class Query : IRequest<List<ValueDto>> { }

        public class Handler : IRequestHandler<Query, List<ValueDto>>
        {
            private readonly IMongoDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IMongoDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ValueDto>> Handle(Query request, CancellationToken cancellationToken) =>
                _mapper.Map<List<ValueDto>>(await _context.GetCollection<Value>("values")
                    .Find(p => true)
                    .ToListAsync(cancellationToken));
        }
    }
}