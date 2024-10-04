using CRAVENEST.Model.Bookings;
using CRAVENEST.Service.Interface;
using CRAVENEST.Service.Interfce;
using CRAVENEST.Utilities.Eums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRAVENEST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {   
        /// <summary>
        /// 
        /// </summary>
        private readonly IBookingsService _bookingsService;
        private readonly ILogger<BookingsController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingsService"></param>
        /// <param name="logger"></param>
        public BookingsController(IBookingsService bookingsService, ILogger<BookingsController> logger)
        {
            _bookingsService = bookingsService;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingsService.GetAll();
            return Ok(bookings);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bookings bookings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookingsService.Create(bookings);

            return result switch
            {
                ResultStatus.Success => Ok(new { message = "Booking created successfully." }),
                ResultStatus.DuplicateEntry => Conflict("Booking already exists."),
                _ => BadRequest("Unable to create booking."),
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Bookings bookings)
        {
            if (!ModelState.IsValid || bookings.BookingId == null)
                return BadRequest("Invalid data or missing BookingId.");

            var result = await _bookingsService.Update(bookings);

            return result == ResultStatus.Success
                ? Ok(new { message = "Booking updated successfully." })
                : BadRequest("Unable to update booking.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingsId"></param>
        /// <returns></returns>
        [HttpGet("{bookingsId}")]
        public async Task<IActionResult> GetById(int bookingsId)
        {
            var result = await _bookingsService.GetById(bookingsId);

            return result != null && result.BookingId != 0
                ? Ok(result)
                : NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var result = await _bookingsService.SoftDelete(id);
            return result == ResultStatus.Success
                ? Ok("Booking soft deleted successfully.")
                : BadRequest("Failed to soft delete the booking.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _bookingsService.Restore(id);
            return result == ResultStatus.Success
                ? Ok("Booking restored successfully.")
                : BadRequest("Failed to restore the booking.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{bookingId}/status")]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, [FromBody] UpdateStatusRequest request)
        {
            if (string.IsNullOrEmpty(request.Status))
            {
                return BadRequest("Status cannot be null or empty.");
            }

            try
            {
                var result = await _bookingsService.UpdateStatus(bookingId, request.Status);

                if (result == ResultStatus.Success)
                {
                    return Ok(new { message = "Booking status updated successfully." });
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update booking status.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating booking status for BookingId {bookingId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the booking status.");
            }
        }
        [HttpGet("signup/{signUpId}")]
        public async Task<IActionResult> GetBookingsBySignUpId(int signUpId)
        {
            if (signUpId <= 0)
            {
                return BadRequest("Invalid SignUpId");
            }

            var bookings = await _bookingsService.GetBookingsBySignUpId(signUpId);

            if (bookings == null || bookings.Count == 0)
            {
                return NotFound("No bookings found for the given SignUpId");
            }

            return Ok(bookings);
        }


    }
    /// <summary>
    /// 
    /// </summary>
    public class UpdateStatusRequest
    {
        public string Status { get; set; }
    }
}

    

