using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Order.Api.Controllers
{
    public class BaseController : Controller
    {
        private ICommandDispatcher? _commandDispatcher;
        private IQueryDispatcher? _queryDispatcher;

        protected ICommandDispatcher CommandDispatcher
            => _commandDispatcher ??= HttpContext.RequestServices.GetRequiredService<ICommandDispatcher>();

        protected IQueryDispatcher QueryDispatcher
            => _queryDispatcher ??= HttpContext.RequestServices.GetRequiredService<IQueryDispatcher>();

    }
}
