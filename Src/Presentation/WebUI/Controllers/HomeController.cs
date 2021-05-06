using Application.Counters.Commands.AddButtonClick;
using Application.Counters.Queries.GetButtonClick;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Counter()
        {
            return View();
        }

        public async Task<IActionResult> GetButtonClicks(CancellationToken cancellationToken = default)
        {
            return Json(new { clicks = await _mediator.Send(new GetButtonClickQuery(), cancellationToken) });
        }

        [HttpPost]
        public async Task<IActionResult> AddButtonClick(CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new AddButtonClickCommand(), cancellationToken);
            return Json(true);
        }
    }

    public class HomeControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new HomeController(_mockMediator.Object);
        }

        [Fact]
        public void Index_GET_ReturnsViewResult() =>
            _controller.Index().Should().BeOfType<ViewResult>();

        [Fact]
        public void Counter_GET_ReturnsViewResult() =>
            _controller.Counter().Should().BeOfType<ViewResult>();

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task GetButtonClicks_GET_ReturnsJsonResult_withClickPropertyValueEqualingGetButtonClickQueryResult(int testValue)
        {
            _mockMediator.Setup(m => m.Send(new GetButtonClickQuery(), It.IsAny<CancellationToken>())).ReturnsAsync(testValue).Verifiable();

            var expectedJsonResult = new JsonResult(new { clicks = testValue });

            (await _controller.GetButtonClicks()).Should().BeEquivalentTo(expectedJsonResult);
            _mockMediator.Verify();
        }

        [Fact]
        public async Task AddButtonClicks_POST_AddButtonClickCommandIsCalledOnce()
        {
            await _controller.AddButtonClick();

            _mockMediator.Verify(m => m.Send(new AddButtonClickCommand(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddButtonClicks_POST_ReturnsJsonResultWithPropertyValueEqualingTrue()
        {
            var expectedJsonResult = new JsonResult(true);

            (await _controller.AddButtonClick()).Should().BeEquivalentTo(expectedJsonResult);
        }
    }
}
