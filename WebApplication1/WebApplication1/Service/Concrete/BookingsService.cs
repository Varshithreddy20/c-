using CRAVENEST.Model.Bookings;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Service.Interfce;
using CRAVENEST.Utilities.Eums;

namespace CRAVENEST.Service.Concrete
{
    public class BookingsService : IBookingsService
    {
        private readonly IBookingsRepository _bookingsRepository;

        public BookingsService(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }

        public async Task<List<Bookings>> GetAll()
        {
            return await _bookingsRepository.GetAll();
        }

        public async Task<ResultStatus> Create(Bookings bookings)
        {
            return await _bookingsRepository.Create(bookings);
        }

        public async Task<ResultStatus> Update(Bookings bookings)
        {
            return await _bookingsRepository.Update(bookings);
        }

        public async Task<Bookings?> GetById(int bookingsId)
        {
            return await _bookingsRepository.GetById(bookingsId);
        }

        public async Task<ResultStatus> SoftDelete(int bookingsId)
        {
            return await _bookingsRepository.SoftDelete(bookingsId);
        }

        public async Task<ResultStatus> Restore(int bookingsId)
        {
            return await _bookingsRepository.Restore(bookingsId);
        }
        public async Task<ResultStatus> UpdateStatus(int bookingId, string status)
        {
            return await _bookingsRepository.UpdateStatus(bookingId, status);
        }
        public async Task<List<Bookings>> GetBookingsBySignUpId(int signUpId)
        {
            return await _bookingsRepository.GetBookingsBySignUpId(signUpId);
        }
    }
}
