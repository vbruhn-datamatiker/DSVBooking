using DSVBooking.Models;
using DSVBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVBooking.Pages
{
    public class DeleteBookingModel : PageModel
    {
        // Booking der skal slettes og dets rum
        public Booking Booking { get; set; }
        public MeetingRoom Room { get; set; }
        
        // Error message hvis bookingen ikke kan findes
        public string ErrorMessage { get; set; } = "";

        //Booking ID så onPost ved hvilken booking der skal slettes
        [BindProperty]
        public int BookingId { get; set; }

        // Kører når siden loader - finder booking og viser info så brugeren kan bekræfte sletning af booking
        // Finder og viser blot booking
        public void OnGet(int bookingId)
        {
            Booking = BookingRepository.GetById(bookingId);

            if (Booking == null)
            {
                ErrorMessage = "Bookingen blev ikke fundet.";
                return;
            }

            //Gemmer ID i gemt felt
            BookingId = Booking.Id;

            // Finder room så navnet vises på siden
            Room = MeetingRoomRepository.GetById(Booking.MeetingRoomId);
        }

        // Kører når brugeren bekræfter sletning
        // Sletter booking og sender brugeren tilbage til /MyBookings
        public IActionResult OnPost()
        {
            Booking = BookingRepository.GetById(BookingId);

            if (Booking == null)
            {
                ErrorMessage = "Bookingen blev ikke fundet.";
                return Page();
            }

            //Finder room før delete, så navnet kan bruges i confirmation message
            Room = MeetingRoomRepository.GetById(Booking.MeetingRoomId);

            // Sender bekræftigelse ud om sletning af booking
            TempData["Confirmation"] = "Din booking af " + Room.Name + " den " + Booking.Date.ToString("dd/MM/yyyy") + " er blevet slettet.";

            // Sletter booking
            BookingRepository.Delete(BookingId);

            return RedirectToPage("/MyBookings");
        }
    }
}
