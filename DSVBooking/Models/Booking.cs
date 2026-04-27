namespace DSVBooking.Models
{
    public class Booking
    {
        //Booking class, beskriver hvad en booking er
        //Id = identifyer, ligesom i Meeting Room
        //MeetingRoomId = Hvilket lokale der er booket, linker sammen emd MeetingRoom
        //EmployeeName = Hvilken medarbejder har oprettet booking
        //Date = Hvilken dag
        //Start/EndTime = Fra hvilken tid og til hvornår er lokalet booket
        //Comment = En kommentar som brugeren kan tilføje, f.eks hvad mødet handler om el. lign. 

        public int Id { get; set; }
        public int MeetingRoomId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Booking() { }

        public Booking(int id, int meetingRoomId, string employeeName, DateOnly date, TimeOnly startTime, TimeOnly endTime, string comment = "")
        {
            Id = id;
            MeetingRoomId = meetingRoomId;
            EmployeeName = employeeName;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Comment = comment;
        }
    }
}
