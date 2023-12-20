using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    // Клас, що представляє запис про реєстрацію гостя в готелі
    public class CheckInLog
    {
        // Час реєстрації
        public DateTime CheckInTime { get; set; }
        // Інформація про гостя
        public Guest Guest { get; }

        // Інформація про номер
        public Room Room { get; }

        // Інформація про працівника готелю, який зареєстрував гостя
        public HotelStaff CheckedInBy { get; }

        // Конструктор класу
        public CheckInLog(DateTime checkInTime, Guest guest, Room room, HotelStaff checkedInBy)
        {
            CheckInTime = checkInTime;
            Guest = guest;
            Room = room;
            CheckedInBy = checkedInBy;
        }
    }
}
