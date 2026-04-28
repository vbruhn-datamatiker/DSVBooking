using DSVBooking.Models;
using DSVBooking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSVBooking.Pages
{
    public class IndexModel : PageModel
    {
        //Tom liste der fyldes i OnGet(). = new List<>() sørger for at der altid er en tom liste, hvis fx. onGet() ikke fylder listen ud
        public List<MeetingRoom> Rooms { get; set; } = new List<MeetingRoom>();

        //BindProperty = Indstruks til ASP.NET, når en bruger submitter en form, find den form field der matcher og put værdierne derind
        //Da vores filter form bruger method="get" fortæller vi også ASP.NET til at bind når vi har GET requests med SupportsGet = true
        [BindProperty(SupportsGet = true)]
        public int MinCapacity { get; set; } = 0;

        [BindProperty(SupportsGet = true)]

        public string SelectedEquipment { get; set; } = "";

        //Kører når siden indlæses. Checker hvilke filtre er aktive og finder matchende lokaler fra repository
        public void OnGet()
        {
            if (SelectedEquipment != "" && SelectedEquipment != null)
            {
                Rooms = MeetingRoomRepository.GetByEquipment(SelectedEquipment);
            }
            else if (MinCapacity > 0)
            {
                Rooms = MeetingRoomRepository.GetByMinCapacity(MinCapacity);
            }
            else
            {
                Rooms = MeetingRoomRepository.GetAll();
            }
        }
    }
}