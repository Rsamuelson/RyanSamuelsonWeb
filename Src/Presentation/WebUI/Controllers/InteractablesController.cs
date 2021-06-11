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
    public class InteractablesController : BaseController
    {
        private readonly IMediator _mediator;

        public InteractablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddButtonClick(CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new AddButtonClickCommand(), cancellationToken);

            return Json(await _mediator.Send(new GetButtonClickQuery(), cancellationToken));
        }
    }

    public class InteractablesControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly InteractablesController _controller;

        public InteractablesControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new InteractablesController(_mockMediator.Object);
        }

        [Fact]
        public void Index_GET_ReturnsViewResult() =>
            _controller.Index().Should().BeOfType<ViewResult>();

        [Fact]
        public async Task AddButtonClicks_POST_AddButtonClickCommandIsCalledOnce()
        {
            await _controller.AddButtonClick();

            _mockMediator.Verify(m => m.Send(new AddButtonClickCommand(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task AddButtonClicks_POST_ReturnsjsonWithGetButtonClickQueryResult(int testValue)
        {
            _mockMediator.Setup(m => m.Send(new GetButtonClickQuery(), It.IsAny<CancellationToken>())).ReturnsAsync(testValue);

            var expectedJsonResult = new JsonResult(testValue);

            (await _controller.AddButtonClick()).Should().BeEquivalentTo(expectedJsonResult);
        }
    }
}
