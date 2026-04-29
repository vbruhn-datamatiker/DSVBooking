using DSVBooking.Models;
using DSVBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVBooking.Pages
{
    public class MyBookingsModel : PageModel
    {
        //Liste over bookings, starter tom og fyldes ved bruger input
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        [BindProperty(SupportsGet = true)]
        public string EmployeeName { get; set; } = "";

        //ErrorMessage hvis brugeren skriver et navn der ikke har booket noget
        public string ErrorMessage { get; set; } = "";

        //Kører når siden indlæses. 
        //Hvis brugeren skriver et navn der har en booking, fyldes den i listen
        //Hvis brugeren srkiver et navn der ikke har noget, kommer errorMessage
        public void OnGet()
        {
            if (EmployeeName != "" && EmployeeName != null)
            {
                Bookings = BookingRepository.GetByEmployee(EmployeeName);

                if (Bookings.Count == 0)
                {
                    ErrorMessage = "Ingen booking fundet i det navn.";
                }
            }
        }

        //Finder RoomName med ID
        public string GetRoomName(int roomId)
        {
            MeetingRoom room = MeetingRoomRepository.GetById(roomId);

            if (room != null)
            {
                return room.Name;
            }

            return "Ukendt lokale";
        }
    }
}
