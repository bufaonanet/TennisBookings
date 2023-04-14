using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisBookings.Web.Core;
using TennisBookings.Web.Data;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly ICourtBookingService _courtBookingService;
        private readonly IGreetingService _loginGreetingService;

        public BookingsModel(ICourtBookingService courtBookingService, IGreetingService loginGreetingService)
        {
            _courtBookingService = courtBookingService;
            _loginGreetingService = loginGreetingService;
        }

        public IEnumerable<IGrouping<DateTime, CourtBooking>> CourtBookings { get; set; }

        public string Greeting { get; private set; }

        [TempData]
        public bool BookingSuccess { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Greeting = _loginGreetingService.GetRandomLoginGreeting($"{User.GetMemberForename()} {User.GetMemberSurname()}");

            var bookings = await _courtBookingService.GetFutureBookingsForMemberAsync(User.GetMemberId());

            CourtBookings = bookings.GroupBy(x => x.StartDateTime.Date);

            return Page();
        }
    }
}
