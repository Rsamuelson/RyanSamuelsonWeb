using Microsoft.AspNetCore.Mvc;
using WebUI.ActionFilterAttributes;

namespace WebUI.Controllers
{
    [ServiceFilter(typeof(SaveIpAddressFilter))]
    public class BaseController : Controller
    {
    }
}
