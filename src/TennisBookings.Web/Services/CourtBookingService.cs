using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Services
{
    public class CourtBookingService : ICourtBookingService
    {
        private readonly TennisBookingDbContext _dbContext;
        private readonly IDateTime _dateTime;

        public CourtBookingService(TennisBookingDbContext dbContext, IDateTime dateTime)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
        }

        public async Task CreateCourtBooking(CourtBooking courtBooking)
        {
            _dbContext.CourtBookings.Add(courtBooking);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CourtBooking> LoadBooking(int bookingId)
        {
            var booking = await _dbContext.CourtBookings
                .AsNoTracking()
                .Include(x => x.Court)
                .Include(x => x.Member)
                .SingleOrDefaultAsync(x => x.Id == bookingId);

            return booking;
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _dbContext.CourtBookings.FindAsync(bookingId);

            if (booking == null)
                return false;

            _dbContext.CourtBookings.Remove(booking);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CourtBooking>> BookingsUntilDateAsync(DateTime date)
        {
            var bookings = await _dbContext.CourtBookings
                .AsNoTracking()
                .Include(x => x.Court)
                .Include(x => x.Member)
                .Where(x => x.StartDateTime >= _dateTime.DateTimeUtc && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<CourtBooking>> BookingsForDayAsync(DateTime date)
        {
            var bookings = await _dbContext.CourtBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<CourtBooking>> CourtBookingsForDayAsync(DateTime date, int courtId)
        {
            var bookings = await _dbContext.CourtBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.CourtId == courtId)
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<CourtBooking>> MemberBookingsForDayAsync(DateTime date, Member member)
        {
            var bookings = await _dbContext.CourtBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.Member.Id == member.Id)
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<CourtBooking>> GetFutureBookingsForMemberAsync(int memberId)
        {
            return await _dbContext.CourtBookings
                .AsNoTracking()
                .Where(c => c.Member.Id == memberId && c.StartDateTime >= _dateTime.DateTimeUtc)
                .OrderBy(x => x.StartDateTime)
                .ToListAsync();
        }

        public async Task<int> GetBookedHoursForMemberAsync(int memberId, DateTime date)
        {
            var member = await _dbContext.Members.FindAsync(memberId);

            if (member == null)
                throw new Exception("Member not found"); // should have better error handling here

            return await GetBookedHoursForMemberAsync(member, date);
        }

        public async Task<int> GetBookedHoursForMemberAsync(Member member, DateTime date)
        {
            var bookings = await _dbContext.CourtBookings
                .AsNoTracking()
                .Where(c => c.Member.Id == member.Id && c.StartDateTime >= date.Date && c.EndDateTime <= date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            var hoursBooked = 0;

            foreach (var booking in bookings)
            {
                var length = (booking.EndDateTime - booking.StartDateTime).Hours;
                hoursBooked = hoursBooked + length;
            }

            return hoursBooked;
        }
    }
}
