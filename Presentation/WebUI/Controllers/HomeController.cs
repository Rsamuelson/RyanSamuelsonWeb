using Application.IpAddresses.Commands.AddIpAddress;
using Application.IpAddresses.Queries.DoesIpAddressExists;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WebUI.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContentAccessor;

        public HomeController(IMediator mediator, IHttpContextAccessor httpContentAccessor)
        {
            _mediator = mediator;
            _httpContentAccessor = httpContentAccessor;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default) 
        {
            return View();
        }
    }

    public class HomeControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new HomeController(_mockMediator.Object, null);
        }

        [Fact]
        public void Index_GET_ReturnsViewResult() =>
            _controller.Index().Should().BeOfType<ViewResult>();

        [Fact]
        public void Index_GET_DoesIpAddressExistsQueryIsCalledOnce()
        {
            _controller.Index();

            _mockMediator.Verify(m => m.Send(new DoesIpAddressExistsQuery(It.IsAny<string>()), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void Index_GET_WhenDoesIpAddressExistsQueryReturnsFalse_AddIpCommandIsCalledNever()
        {

            _mockMediator.Setup(m => m.Send(new DoesIpAddressExistsQuery(It.IsAny<string>()), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            _controller.Index();

            _mockMediator.Verify(m => m.Send(new AddIpCommand(new AddIpViewModel() { IpAddress = ""}), It.IsAny<CancellationToken>()), Times.Never);
        } 
    }
}
