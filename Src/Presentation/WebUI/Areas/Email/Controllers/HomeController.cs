using Application.Emails.Commands;
using Common.TestUtilities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WebUI.Areas.Email.Controllers
{
    public class HomeController : EmailBaseController
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

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailViewModel viewModel, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new SendEmailCommand(viewModel), cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }

    public class IndexTests
    {
        public readonly HomeController _homeController;

        public IndexTests()
        {
            _homeController = new HomeController(null);
        }

        [Fact]
        public void Index_POST_returnsViewResult() =>
            _homeController.Index().Should().BeOfType<ViewResult>();
    }

    public class SendEmailTests
    {
        public readonly Mock<IMediator> _mockMediator;
        public readonly HomeController _homeController;

        public SendEmailTests()
        {
            _mockMediator = new Mock<IMediator>();
            _homeController = new HomeController(_mockMediator.Object);
        }

        [Fact]
        public async Task SendEmail_POST_SendEmailCommandIsCalledOnce()
        {
            var testSendEmailViewModel = new SendEmailViewModel()
            {
                From = RandomStringUtilities.RandomAlphaNumericString(10),
                Message = RandomStringUtilities.RandomAlphaNumericString(10)
            };
            await _homeController.SendEmail(testSendEmailViewModel);

            _mockMediator.Verify(m => m.Send(new SendEmailCommand(testSendEmailViewModel), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SendEmail_POST_RedirectsToIndex() =>
            (await _homeController.SendEmail(It.IsAny<SendEmailViewModel>())).Should().BeAssignableTo<RedirectToActionResult>().Which.ActionName.Should().Be("Index");
    }
}
