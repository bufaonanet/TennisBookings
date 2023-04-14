using System.Threading.Tasks;

namespace TennisBookings.Web.Core
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string sender, string recipient, string title, string content)
        {
            // Demoware - not implemented!

            return Task.CompletedTask;
        }
    }
}
