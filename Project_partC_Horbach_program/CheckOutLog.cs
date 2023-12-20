using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public class CheckOutLog
    {
        // Час виселення
        public DateTime CheckOutTime { get; set; }

        // Інформація про гостя
        public Guest Guest { get; }

        // Інформація про номер
        public Room Room { get; }

        // Інформація про працівника готелю, який виселив  гостя
        public HotelStaff CheckedOutBy { get; }

        // Конструктор класу
        public CheckOutLog(DateTime checkoutTime, Guest guest, Room room, HotelStaff checkedOutBy)
        {
            CheckOutTime = checkoutTime;
            Guest = guest;
            Room = room;
            CheckedOutBy = checkedOutBy;
        }
    }
}
