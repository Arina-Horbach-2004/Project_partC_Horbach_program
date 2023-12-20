using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public class Hotel : IHotel, IMyEnumerable
    {
        public string Country { get; set; } // Властивість для зберігання країни, до якої належить готель.

        public string City { get; set; } // Властивість для зберігання міста, де розташований готель.

        public List<Room> Rooms { get; set; } // Список кімнат у готелі.

        public List<HotelStaff> staffList;// Список персоналу готелю.

        public List<Guest> Guests { get; set; } // Список гостей готелю.

        public List<Guest> CheckedInGuests { get; set; } // Список зареєстрованих гостей, які перебувають у готелі.

        public List<Guest> CheckedOutGuests { get; set; } // Список гостей, які вже виселилися з готелю.

        public List<CheckInLog> CheckInLogs { get; set; }

        public List<CheckOutLog> CheckOutLogs { get; set; }

        public List<HotelStaff> staff; // Список активного персоналу готелю.

        private List<HotelStaff> dismissedStaff = new List<HotelStaff>();

        public Hotel(string country, string city)
        {
            Country = country; // Ініціалізація властивості "Country" значенням країни, до якої належить готель.
            City = city; // Ініціалізація властивості "City" значенням міста, де розташований готель.

            Rooms = new List<Room>(); // Створення порожнього списку для кімнат готелю.
            staffList = new List<HotelStaff>(); // Створення порожнього списку для персоналу готелю.
            Guests = new List<Guest>(); // Створення порожнього списку для гостей готелю.
            CheckedInGuests = new List<Guest>(); // Створення порожнього списку для зареєстрованих гостей, які перебувають у готелі.
            CheckedOutGuests = new List<Guest>(); // Створення порожнього списку для гостей, які вже виселилися з готелю.
            staff = new List<HotelStaff>(); // Створення порожнього списку для активного персоналу готелю.
            dismissedStaff = new List<HotelStaff>(); // Створення порожнього списку для звільненого персоналу готелю.
            CheckInLogs = new List<CheckInLog>();
            CheckOutLogs = new List<CheckOutLog>();
        }

        // Метод для додавання нової кімнати в готель
        public void AddRoom(Room room)
        {
            // Перевіряємо унікальність номера кімнати
            if (Rooms.Any(existingRoom => existingRoom.RoomNumber == room.RoomNumber))
            {
                throw new InvalidOperationException($"Кімната з номером {room.RoomNumber} вже існує в готелі.");
            }

            // Додаємо кімнату в список кімнат
            Rooms.Add(room);
        }

        // Метод для отримання кімнати за номером
        public Room GetRoomByNumber(int roomNumber)
        {
            foreach (var room in Rooms)
            {
                if (room.RoomNumber == roomNumber)
                {
                    return room;
                }
            }
            return null;
        }

        public void RegisterStaff(HotelStaff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Персонал не може бути порожнім.");
            }

            if (staffList.Any(existingStaff => existingStaff.Id == staff.Id))
            {
                throw new Exception($"Персонал з ідентифікаційним номером {staff.Id} вже зареєстрований в готелі.");
            }

            staffList.Add(staff);
            Console.WriteLine($"{staff.Get_Full_Name()} зареєстрований як персонал готелю.");
        }

        // Метод для видалення персоналу готелю
        public void RemoveStaff(HotelStaff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Персонал не може бути порожнім.");
            }

            if (staffList.Count == 0)
            {
                throw new InvalidOperationException("Неприпустиме видалення. Список персоналу порожній.");
            }

            if (!staffList.Contains(staff))
            {
                throw new InvalidOperationException($"Неприпустиме видалення. {staff.Get_Full_Name()} не зареєстрований в готелі.");
            }

            staffList.Remove(staff);
            staff.IsDismissed = true;
            staff.Id = dismissedStaff.Count + 1;

            dismissedStaff.Add(staff);
            Console.WriteLine($"Співробітника {staff.Get_Full_Name()} уволено. Новий ID: {staff.Id}");
        }


        public Dictionary<RoomType, int> maxGuestsPerRoomType = new Dictionary<RoomType, int>
    {
        { RoomType.Single, 1 },
        { RoomType.Double, 2 },
        { RoomType.Familyroom, 4 },
        { RoomType.BusinessClass, 5 },
        { RoomType.Withseparatebeds, 2 },
        { RoomType.Villa, 7 },
        { RoomType.Oceanview, 8 },
        { RoomType.Standard, 4 },
        { RoomType.Luxury, 2 },
        { RoomType.Longstay, 3 }
        };

        public void RegisterGuest(Guest guest, HotelStaff checkedInBy, Room room, int stayDuration)
        {
            // Проверка, зарегистрирована ли комната в гостинице
            if (!IsRoomRegistered(room))
            {
                throw new InvalidOperationException($"Кімната {room.RoomNumber} не зареєстрована в готелі.");
            }

            // Проверка, что гость не является null
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest), "Гість не може бути порожнім.");
            }

            // Проверка, что персонал для регистрации является допустимым
            if (!IsValidCheckInStaff(checkedInBy, room))
            {
                throw new InvalidOperationException($"Працівник {checkedInBy.Get_Full_Name()} не має права реєстрації гостя.");
            }

            // Проверка, что количество гостей в комнате не превышает максимальное значение
            if (room.Guests.Count + 1 > room.MaxGuests)
            {
                throw new InvalidOperationException($"Неможливо заселити більше гостей в кімнату {room.RoomNumber} типу {room.Type}.");
            }

            // Добавление гостя в комнату и в список заселенных гостей
            room.CheckIn(guest);
            CheckedInGuests.Add(guest);

            guest.CurrentRoom = room;

            // Установка времени заселения на текущее время
            guest.CheckInTime = DateTime.Now;

            // Установка продолжительности пребывания
            guest.StayDuration = stayDuration;

            // Установка информации о том, кто зарегистрировал гостя
            guest.CheckedInBy = checkedInBy;

            // Создание записи о заселении в журнале
            var checkInLog = new CheckInLog(guest.CheckInTime, guest, room, checkedInBy);
            CheckInLogs.Add(checkInLog);

            Console.WriteLine($"{guest.Get_Full_Name()} був успішно зареєстрований {checkedInBy.Get_Full_Name()}.");
        }

        public void RemoveGuest(Guest guest, HotelStaff checkedOutBy, Room room)
        {
            var checkedInRoom = room;

            // Перевірка, чи гість проживає в номері, і виклик методу CheckOut для відмітки виселення
            if (checkedInRoom != null)
            {
                checkedInRoom.CheckOut();

                // Після виселення, оновіть інформацію про гостей у кімнаті
                checkedInRoom.UpdateGuestList();
            }

            // Перевірка, чи гість зареєстрований у списку заселених гостей
            if (CheckedInGuests.Contains(guest))
            {
                CheckedInGuests.Remove(guest);
                CheckedOutGuests.Add(guest);

                // Встановлення часу виселення за допомогою DateTime.Now для однорідного порівняння часу
                guest.CheckOutTime = DateTime.Now;

                // Встановлення інформації про працівника, який здійснив виселення
                guest.CheckedOutBy = checkedOutBy;

                // Додавання інформації про виселення у журнал виписки
                var checkOutLog = new CheckOutLog(guest.CheckInTime, guest, room, checkedOutBy);
                CheckOutLogs.Add(checkOutLog);
            }
            else
            {
                // Якщо гостя немає в списку заселених або виселених гостей, розгляньте викидання винятку
                throw new InvalidOperationException($"Гостя {guest.Get_Full_Name()} не можна виселити, оскільки він не проживає в готелі.");
            }
        }

        private bool IsRoomRegistered(Room room)
        {
            return Rooms.Contains(room);
        }

        // Приватний метод для перевірки, чи правильний персонал для реєстрації
        public bool IsValidCheckInStaff(HotelStaff staff, Room room)
        {
            // Список допустимих посад для реєстрації гостя
            List<StaffPosition> allowedPositions = new List<StaffPosition>
            {
                StaffPosition.Receptionist,
                StaffPosition.Administrator,
                StaffPosition.Staff,
                StaffPosition.RegistrationManager,
                StaffPosition.Manager,
                StaffPosition.FrontDeskClerk
            };
            // Перевірка, чи посада персоналу є в списку допустимих
            return allowedPositions.Contains(staff.StaffPosition);
        }

        // Метод для отримання списку персоналу готелю
        public List<HotelStaff> GetStaffList()
        {
            // Об'єднання списку активного та звільненого персоналу та повернення його
            var allStaff = staffList.Concat(dismissedStaff).ToList();
            return allStaff;
        }

        // Реализация интерфейса IEnumerable
        public IEnumerator GetEnumerator()
        {
            return Guests.GetEnumerator();
        }

        public List<Guest> GetGuestList()
        {
            List<Guest> allGuests = new List<Guest>();

            // Add checked-in guests to the list
            allGuests.AddRange(CheckedInGuests);

            // Add checked-out guests to the list
            allGuests.AddRange(CheckedOutGuests);

            return allGuests;
        }

        // Метод для отримання списку всіх кімнат готелю
        public List<Room> GetRoomList()
        {
            return Rooms.ToList();
        }

        // Метод для отримання списку заселених гостей готелю
        public List<Guest> GetCheckedInGuests()
        {
            return CheckedInGuests.ToList();
        }

        // Метод для отримання списку виписаних гостей готелю
        public List<Guest> GetCheckedOutGuests()
        {
            return CheckedOutGuests.ToList();
        }

        public List<HotelStaff> GetDismissedStaff()
        {
            return dismissedStaff.ToList();
        }

        // Перевизначений метод для представлення готелю у вигляді рядка
        public override string ToString()
        {
            int checkedInGuestsCount = GetCheckedInGuests().Count;
            int checkedOutGuestsCount = GetCheckedOutGuests().Count;
            return $"{Country}, {City} - {GetRoomList().Count} кімнат, {GetStaffList().Count} персонал в готелі з яких: {GetDismissedStaff().Count} звільненний персонал, {GetGuestList().Count} гостей в готелі з яких : {checkedInGuestsCount} гостей (Checked In), {checkedOutGuestsCount} гостей (Checked Out)";
        }
    }
}
