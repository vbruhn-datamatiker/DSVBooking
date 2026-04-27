using DSVBooking.Models;

namespace DSVBooking.Repositories
{
    //Oprettelse af static class, for at sikre at der kun er 1 liste i applikationen som alle sider kan læse
    public static class MeetingRoomRepository
    {
        //Database "substitut"
        //Laver en liste over mødelokaler når applikationen starter
        //Private, så kun metoderne i denne klasse har adgang - static for at være 1 delt liste for hele applikationen
        private static List<MeetingRoom> _rooms = new List<MeetingRoom>
        {
            new MeetingRoom(1, "Mødelokale A", 6,  new List<string> { "Projektor", "Whiteboard" }),
            new MeetingRoom(2, "Mødelokale B", 12, new List<string> { "Projektor", "TV-skærm" }),
            new MeetingRoom(3, "Mødelokale C", 4,  new List<string> { "Whiteboard" }),
            new MeetingRoom(4, "Mødelokale D", 20, new List<string> { "Projektor", "Whiteboard", "Videokonference" }),
            new MeetingRoom(5, "Mødelokale E", 8,  new List<string> { "TV-skærm", "Whiteboard" }),
        };

        //Returnerer hele listen af Meeting Rooms - kaldes af index når der ikke er nogen filtre aktive.
        public static List<MeetingRoom> GetAll()
        {
            return _rooms;
        }


        //Lister som kan collect og return, benyttes til at filtrere rum efter kriterierne som f.eks navn, kapacicet, faciliteter

        //Returnerer rum ved ID
        public static MeetingRoom GetById(int id)
        {
            foreach (MeetingRoom room in _rooms)
            {
                if (room.Id == id)
                {
                    return room;
                }
            }
            return null;
        }

        //Filtrerer kapacitet 
        public static List<MeetingRoom> GetByMinCapacity(int minCapacity)
        {
            List<MeetingRoom> result = new List<MeetingRoom>();

            foreach (MeetingRoom room in _rooms)
            {
                if (room.Capacity >= minCapacity)
                {
                    result.Add(room);
                }
            }

            return result;
        }

        //Filtrerer efter faciliteter (udstyr i lokalet)
        //Nested loop, fordi "equipment" også er en liste inde i hvert rum. (Et rum kan indeholde flere faciliteter)
        public static List<MeetingRoom> GetByEquipment(string equipment)
        {
            List<MeetingRoom> result = new List<MeetingRoom>();

            foreach (MeetingRoom room in _rooms)
            {
                foreach (string item in room.Equipment)
                {
                    if (item.ToLower() == equipment.ToLower())
                    {
                        result.Add(room);
                        break; //Afslutter loop når der er fundet et match så der ikke sker nogen duplikeringer
                    }
                }
            }

            return result;
        }
    }
}
