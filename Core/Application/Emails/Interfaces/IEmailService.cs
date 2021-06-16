using System.Net;
using System.Threading.Tasks;

namespace Application.Emails.Interfaces
{
    public interface IEmailService
    {
        Task<HttpStatusCode> SendEmail(string to, string from, string message);
    }
}
