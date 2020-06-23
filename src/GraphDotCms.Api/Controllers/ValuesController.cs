using GraphDotCms.Application.Values;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphDotCms.Api.Controllers
{
    public class ValuesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValueDto>>> GetValuesAsync() =>
            await Mediator.Send(new GetValues.Query());
    }
}
