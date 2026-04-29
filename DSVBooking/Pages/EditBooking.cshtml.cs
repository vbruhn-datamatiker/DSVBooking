using DSVBooking.Models;
using DSVBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVBooking.Pages
{
    public class EditBookingModel : PageModel
    {
        //Room details som vises, så brugeren kan se hvilket lokale der ændres på
        public MeetingRoom Room { get; set; }
        public string ErrorMessage { get; set; } = "";

        //Ikke vist på formen, men er nødvendige for at OnPost ved hvilken booking der skal opdateres
        [BindProperty]
        public int BookingId { get; set; }
        [BindProperty]
        public int RoomId { get; set; }

        //Viste form felter - bliver automatisk udfyldt ved submission
        [BindProperty]
        public string EmployeeName { get; set; } = "";
        [BindProperty]
        public string Date { get; set; } = "";
        [BindProperty]
        public string StartTime { get; set; } = "";
        [BindProperty]
        public string EndTime { get; set; } = "";
        [BindProperty]
        public string Comment { get; set; } = "";

        //Kører når siden loades, finder den eksisterende booking og fylder den med de nuværende værdier
        public void OnGet(int bookingId)
        {
            Booking existing = BookingRepository.GetById(bookingId);

            if (existing == null)
            {
                ErrorMessage = "Bookingen blev ikke fundet.";
                return;
            }

            // Udfylder felterne med eksisterende værdier
            BookingId = existing.Id;
            RoomId = existing.MeetingRoomId;
            EmployeeName = existing.EmployeeName;
            Date = existing.Date.ToString("yyyy-MM-dd");
            StartTime = existing.StartTime.ToString("HH:mm");
            EndTime = existing.EndTime.ToString("HH:mm");
            Comment = existing.Comment;

            Room = MeetingRoomRepository.GetById(existing.MeetingRoomId);
        }

        // Kører når brugeren submitter formen, validerer input og gemmer den opdaterede booking
        public IActionResult OnPost()
        {
            Room = MeetingRoomRepository.GetById(RoomId);

            // Validerer at alle felter er udfyldt
            if (EmployeeName == "" || Date == "" || StartTime == "" || EndTime == "")
            {
                ErrorMessage = "Udfyld venligst alle påkrævede felter.";
                return Page();
            }

            // Konverterer strings til DateOnly og TimeOnly
            DateOnly bookingDate = DateOnly.Parse(Date);
            TimeOnly bookingStart = TimeOnly.Parse(StartTime);
            TimeOnly bookingEnd = TimeOnly.Parse(EndTime);

            // Tjek at end time er efter start time
            if (bookingEnd <= bookingStart)
            {
                ErrorMessage = "Sluttidspunktet skal være efter starttidspunktet.";
                return Page();
            }

            // Tjekker tilgængelighed - Ekskluderer den nuværende booking, så der ikke opstår konflikter
            if (!BookingRepository.IsRoomAvailable(RoomId, bookingDate, bookingStart, bookingEnd, BookingId))
            {
                ErrorMessage = "Mødelokalet er desværre optaget i det valgte tidsrum. Vælg et andet tidspunkt.";
                return Page();
            }

            // Opdaterer booking med de nye værdier
            Booking updatedBooking = new Booking();
            updatedBooking.Id = BookingId;
            updatedBooking.MeetingRoomId = RoomId;
            updatedBooking.EmployeeName = EmployeeName;
            updatedBooking.Date = bookingDate;
            updatedBooking.StartTime = bookingStart;
            updatedBooking.EndTime = bookingEnd;
            updatedBooking.Comment = Comment;

            BookingRepository.Update(updatedBooking);

            TempData["Confirmation"] = "Din booking af " + Room.Name + " den " + bookingDate.ToString("dd/MM/yyyy") + " kl. " + bookingStart.ToString("HH:mm") + " - " + bookingEnd.ToString("HH:mm") + " er opdateret!";

            return RedirectToPage("/MyBookings");
        }
    }
}
