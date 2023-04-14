using System.Threading.Tasks;

namespace TennisBookings.Web.Core
{
    public interface IEmailService
    {
        Task SendAsync(string sender, string recipient, string title, string content);
    }
}
