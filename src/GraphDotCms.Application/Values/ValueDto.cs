using GraphDotCms.Application.Interfaces;
using GraphDotCms.Domain.Entities;

namespace GraphDotCms.Application.Values
{
    public class ValueDto : IMapFrom<Value>
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string TheValue { get; set; }
    }
}
