using DSVBooking.Models;
using DSVBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVBooking.Pages
{
    public class BookModel : PageModel
    {
        //Properties
        public MeetingRoom Room { get; set; }
        public string ErrorMessage { get; set; } = "";

        //[BindProperty form fields]
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

        [BindProperty]
        public int RoomId { get; set; }

        //Kører når siden indlæses
        public void OnGet(int roomId)
        {
            RoomId = roomId;
            Room = MeetingRoomRepository.GetById(roomId);
        }

        //Kører når brugeren submitter en form. Funktionen er ikke void, IActionResult kan returnere den samme side igen eller sende til en anden siden
        public IActionResult OnPost()
        {
            Room = MeetingRoomRepository.GetById(RoomId);

            // Validering af alle nødvendige felter er udfyldt
            if (EmployeeName == "" || Date == "" || StartTime == "" || EndTime == "")
            {
                ErrorMessage = "Udfyld venligst alle påkrævede felter.";
                return Page();
            }

            // Konverterer strings til DateOnly og TimeOnly
            DateOnly bookingDate = DateOnly.Parse(Date);
            TimeOnly bookingStart = TimeOnly.Parse(StartTime);
            TimeOnly bookingEnd = TimeOnly.Parse(EndTime);

            // Tjekker at end time er efter start time
            if (bookingEnd <= bookingStart)
            {
                ErrorMessage = "Sluttidspunktet skal være efter starttidspunktet.";
                return Page();
            }

            // Tjek at lokalet er tilgængeligt inden for tidsperiode
            if (!BookingRepository.IsRoomAvailable(RoomId, bookingDate, bookingStart, bookingEnd))
            {
                ErrorMessage = "Lokalet er desværre optaget i det valgte tidsrum. Vælg et andet tidspunkt.";
                return Page();
            }

            // Opret og gem booking
            Booking newBooking = new Booking();
            newBooking.MeetingRoomId = RoomId;
            newBooking.EmployeeName = EmployeeName;
            newBooking.Date = bookingDate;
            newBooking.StartTime = bookingStart;
            newBooking.EndTime = bookingEnd;
            newBooking.Comment = Comment;

            BookingRepository.Add(newBooking);

            // Bekræftelses-besked på at booking er oprettet som vises på næste side
            TempData["Confirmation"] = "Din booking af " + Room.Name + " den " + bookingDate.ToString("dd/MM/yyyy") + " kl. " + bookingStart.ToString("HH:mm") + " - " + bookingEnd.ToString("HH:mm") + " er oprettet!";

            // Sender brugeren til MyBookings efter succesful booking
            return RedirectToPage("/MyBookings");
        }
    }
}
