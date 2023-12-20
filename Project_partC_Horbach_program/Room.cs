using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsCheckedIn { get; set; }
        public double Rate { get; set; }
        public Guest CurrentGuest { get; set; }
        public int MaxGuests { get; set; }

        public List<Guest> Guests { get; set; } = new List<Guest>();

        public Room(int roomNumber, double rate, RoomType type)
        {
            RoomNumber = roomNumber;
            Rate = rate;
            Type = type;
            IsOccupied = false;
            CurrentGuest = null;
            MaxGuests = GetMaxGuestsForRoomType(type);
        }

        public void CheckIn(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest), "Гість не може бути порожнім.");
            }

            // Проверка, что количество гостей в комнате не превышает максимальное значение
            if (Guests.Count + 1 > MaxGuests)
            {
                throw new InvalidOperationException($"Неможливо заселити більше гостей в кімнату {RoomNumber} типу {Type}.");
            }

            Guests.Add(guest);
            IsOccupied = true;
        }

        public void CheckOut()
        {
            if (!IsOccupied)
            {
                throw new InvalidOperationException($"Кімната {RoomNumber} не зайнята, і гостей не можна виселити.");
            }

            // Видалення гостя зі списку гостей
            Guests.Remove(CurrentGuest);

            // Оновлення статусу та видалення посилання на гостя
            IsOccupied = false;
            CurrentGuest = null;
        }

        public void UpdateGuestList()
        {
            Guests.RemoveAll(guest => guest == null || guest.CheckOutTime != null);
        }

        public override string ToString()
        {
            return $"- Номер {RoomNumber}, Ціна: {Rate}, Тип: {Type}";
        }

        private int GetMaxGuestsForRoomType(RoomType Type)
        {
            switch (Type)
            {
                case RoomType.Single:
                    return 1;
                case RoomType.Double:
                    return 2;
                case RoomType.Familyroom:
                    return 4;
                case RoomType.BusinessClass:
                    return 5;
                case RoomType.Withseparatebeds:
                    return 2;
                case RoomType.Villa:
                    return 7;
                case RoomType.Oceanview:
                    return 8;
                case RoomType.Standard:
                    return 4;
                case RoomType.Luxury:
                    return 2;
                case RoomType.Longstay:
                    return 3;
                default:
                    return 1;
            }
        }
    }
}
