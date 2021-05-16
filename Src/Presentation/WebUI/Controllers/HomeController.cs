using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();
        }

        [Fact]
        public void Index_GET_ReturnsViewResult() =>
            _controller.Index().Should().BeOfType<ViewResult>();

    }
}
