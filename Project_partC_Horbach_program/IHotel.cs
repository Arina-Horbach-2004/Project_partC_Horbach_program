using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public interface IHotel
    {
        // Реєстрація персоналу
        public void RegisterStaff(HotelStaff staff);
        // Виселення посетителя
        public void RemoveStaff(HotelStaff staffm);

        // Метод для реєстрації гостя в готелі
        public void RegisterGuest(Guest guest, HotelStaff checkedInBy, Room room, int stayDuration);

        // Метод для виписування гостя з готелю
        public void RemoveGuest(Guest guest, HotelStaff checkedOutBy, Room room);

        // Метод для отримання списку персоналу готелю
        public List<HotelStaff> GetStaffList();

        // Метод для отримання списку гостей готелю
        public List<Guest> GetGuestList();

        // Метод для отримання списку всіх кімнат готелю
        public List<Room> GetRoomList();

        // Метод для отримання списку заселених гостей готелю
        public List<Guest> GetCheckedInGuests();

        // Метод для отримання списку виписаних гостей готелю
        public List<Guest> GetCheckedOutGuests();

        public string ToString();
    }
}
