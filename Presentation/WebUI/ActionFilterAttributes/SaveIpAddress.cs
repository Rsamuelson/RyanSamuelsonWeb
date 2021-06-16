using Application.IpAddresses.Commands.AddIpAddress;
using Application.IpAddresses.Queries.DoesIpAddressExists;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebUI.ActionFilterAttributes
{
    public class SaveIpAddressFilter : ActionFilterAttribute
    {
        private readonly IMediator _mediator;

        public SaveIpAddressFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) 
        {
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

            if (!(await _mediator.Send(new DoesIpAddressExistsQuery(ipAddress))))
                await _mediator.Send(new AddIpCommand(new AddIpViewModel() { IpAddress = ipAddress }));

            await next();
        }
    }
}
