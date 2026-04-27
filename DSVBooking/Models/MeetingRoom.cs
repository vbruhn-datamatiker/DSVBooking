namespace DSVBooking.Models
{
    public class MeetingRoom
    {
        //Meeting Room Class, definerer et meeting room og hvad det indeholder
        //Id = INT Identifier
        //Name = Navn til lokale, f.eks "Mødelokale A"
        //Capacity = Hvor mange pladser er der i lokalet?
        //Equiment = Hvilket udstyr findes i lokalet? F.eks projektor, whiteboard m.m
        //IsAvailable = Bool for lokalets tilgængelighed

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<string> Equipment { get; set; } = new();
        public bool IsAvailable { get; set; } = true;

        public MeetingRoom() { }

        public MeetingRoom(int id, string name, int capacity, List<string> equipment)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Equipment = equipment;
            IsAvailable = true;
        }
    }
}
