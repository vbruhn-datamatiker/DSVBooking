using DSVBooking.Models;

namespace DSVBooking.Repositories
{
    public static class BookingRepository
    {
        //Opretter en tom liste til bookings
        private static List<Booking> _bookings = new List<Booking>();
        
        //Tæller, som giver hver booking et unikt ID
        private static int _nextId = 1;

        //Kan returnere liste af alle bookinger når de er oprettet
        public static List<Booking> GetAll()
        {
            return _bookings;
        }

        public static Booking GetById(int id)
        {
            foreach (Booking booking in _bookings)
            {
                if (booking.Id == id)
                {
                    return booking;
                }
            }
            return null;
        }

        //Metode til at finde bookings ud fra medarbejdernavn 
        public static List<Booking> GetByEmployee(string employeeName)
        {
            List<Booking> result = new List<Booking>();

            foreach (Booking booking in _bookings)
            {
                if (booking.EmployeeName.ToLower() == employeeName.ToLower())
                {
                    result.Add(booking);
                }
            }

            return result;
        }

        //Tilføjer bookings til liste
        public static void Add(Booking booking)
        {
            booking.Id = _nextId++;
            _bookings.Add(booking);
        }

        //Metode til at opdatere en booking
        //Finder en eksisterende booking og tillader at overwrite med nye værdier (hvis man vil ændre en booking)
        public static void Update(Booking booking)
        {
            Booking existing = GetById(booking.Id);

            if (existing == null)
            {
                return;
            }
            //Kopierer hver property fra den nye booking ind i den eksisterende
            existing.MeetingRoomId = booking.MeetingRoomId;
            existing.EmployeeName = booking.EmployeeName;
            existing.Date = booking.Date;
            existing.StartTime = booking.StartTime;
            existing.EndTime = booking.EndTime;
            existing.Comment = booking.Comment;
        }

        //Metode til at finde booking objekt i listen og fjerne den
        public static void Delete(int id)
        {
            Booking booking = GetById(id);

            if (booking != null) //Sørger for programmet ikke crasher hvis booking ikke eksisterer
            {
                _bookings.Remove(booking);
            }
        }

        //Metode til at forhindre at der kan ske double bookings
        public static bool IsRoomAvailable(int roomId, DateOnly date, TimeOnly startTime, TimeOnly endTime, int excludeBookingId = 0)
        {
            foreach (Booking booking in _bookings)
            {
                if (booking.MeetingRoomId == roomId &&
                    booking.Date == date &&
                    booking.Id != excludeBookingId)
                {
                    if (booking.StartTime < endTime && booking.EndTime > startTime)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
