using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Project_partC_Horbach_program;


namespace Project_partC_Horbach_program
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Hotel hotel = new Hotel("Ukraine", "Kiev");

            // Підписка на подію заселення гостя
            hotel.GuestCheckedIn += (guest, room) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Гість {guest.Get_Full_Name()} заселений в номер {room.RoomNumber}.");
                Console.ResetColor();
            };

            // Підписка на подію виселення гостя
            hotel.GuestCheckedOut += guest =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Гість {guest.Get_Full_Name()} виселений.");
                Console.ResetColor();
            };

            // Підписка на подію видалення сотрудника
            hotel.StaffRemoved += staff =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Співробітник {staff.Get_Full_Name()} видалений.");
                Console.ResetColor();
            };

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Додати номери до готелю");
                Console.WriteLine("2. Додати персонал");
                Console.WriteLine("3. Видалення персоналу");
                Console.WriteLine("4. Зареєструвати гостя");
                Console.WriteLine("5. Виселити гостя");
                Console.WriteLine("6. Вивести інформацію про зареєстрованих гостей");
                Console.WriteLine("7. Вивести інформацію про виселених гостей");
                Console.WriteLine("8. Вивести інформацію про персонал");
                Console.WriteLine("9. Вивести інформацію про звільненний персонал");
                Console.WriteLine("10. Вивести інформацію про готель");
                Console.WriteLine("11. Вивести інформацію про номери");
                Console.WriteLine("0. Вийти");

                Console.Write("Введіть номер опції: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddRoomsToHotel(hotel);
                        break;
                    case "2":
                        AddStaffToHotel(hotel);
                        break;
                    case "3":
                        RemoveStaff(hotel);
                        break;
                    case "4":
                        RegisterGuest(hotel);
                        break;
                    case "5":
                        RemoveGuest(hotel);
                        break;
                    case "6":
                        DisplayRegisteredGuests(hotel);
                        break;
                    case "7":
                        DisplayRemoveGuests(hotel);
                        break;
                    case "8":
                        PrintHotelStaff(hotel);
                        break;
                    case "9":
                        DisplayDismissedStaff(hotel);
                        break;
                    case "10":
                        PrintHotelInfo(hotel);
                        break;
                    case "11":
                        PrintRoomInfo(hotel);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір опції. Спробуйте ще раз.");
                        break;
                }
            }
        }

        static void AddRoomsToHotel(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            Console.Write("Введіть кількість номерів для додавання: ");
            int roomCount;

            // Обробка помилок при введенні кількості кімнат
            while (!int.TryParse(Console.ReadLine(), out roomCount) || roomCount <= 0)
            {
                Console.WriteLine("Будь ласка, введіть коректну кількість номерів (ціле число більше 0).");
            }

            for (int i = 0; i < roomCount; i++)
            {
                Console.Write($"Введіть номер {i + 1}: ");
                int roomNumber;

                // Обробка помилок при введенні номера кімнати
                while (!int.TryParse(Console.ReadLine(), out roomNumber) || roomNumber <= 0)
                {
                    Console.WriteLine("Будь ласка, введіть коректний номер кімнати (ціле число більше 0).");
                }

                Console.Write($"Введіть ціну для номера {i + 1}: ");
                double price;

                // Обробка помилок при введенні ціни
                while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
                {
                    Console.WriteLine("Будь ласка, введіть коректну ціну (додатне число).");
                }

                Console.Write($"Введіть тип для номера {i + 1} (Standard, Suite, Deluxe, Luxury, Familyroom, Adjoiningroom, Oceanview, HoneymoonSuite, BusinessClass, Villa, Garden_view, Longstay, Single, Double, Withseparatebeds): ");
                RoomType roomType;

                // Обробка помилок при введенні типу кімнати
                while (!Enum.TryParse(Console.ReadLine(), out roomType))
                {
                    Console.WriteLine("Будь ласка, введіть коректний тип кімнати.");
                }

                try
                {
                    // Додавання кімнати до готелю
                    hotel.AddRoom(new Room(roomNumber, price, roomType));
                    Console.WriteLine($"Номер {roomNumber} був успішно доданий до готелю.");
                }
                catch (InvalidOperationException ex)
                {
                    // Виведення повідомлення про помилку на екран
                    Console.WriteLine($"Помилка: {ex.Message}");
                    i--; // Зменшення лічильника для повторення введення даних для поточної кімнати
                }
            }

            Console.WriteLine("Номери були успішно додані до готелю.");
        }

        static void AddStaffToHotel(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            Console.Write("Введіть кількість персоналу для додавання: ");
            int staffCount;
            while (!int.TryParse(Console.ReadLine(), out staffCount) || staffCount <= 0)
            {
                Console.WriteLine("Будь ласка, введіть дійсне число більше нуля.");
                Console.Write("Введіть кількість персоналу для додавання: ");
            }

            for (int i = 0; i < staffCount; i++)
            {
                Console.Write($"Введіть ім'я для персоналу {i + 1}: ");
                string firstName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(firstName) && firstName.Length >= 3 && firstName.All(char.IsLetter) && firstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть ім'я ще раз:");
                        firstName = Console.ReadLine();
                    }
                } while (true);

                Console.Write($"Введіть прізвище для персоналу {i + 1}: ");
                string lastName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(lastName) && lastName.Length >= 3 && lastName.All(char.IsLetter) && lastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть прізвище ще раз:");
                        lastName = Console.ReadLine();
                    }
                } while (true);

                Console.Write("Введіть дату народження (у форматі ДД/ММ/РРРР): ");
                string inputDate;
                DateTime birthDate;
                do
                {
                    inputDate = Console.ReadLine();
                    if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
                    {
                        Console.WriteLine($"Введена дата народження: {birthDate.ToShortDateString()}");
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат дати. Спробуйте ще раз.");
                        Console.Write("Введіть дату народження (у форматі ДД/ММ/РРРР): ");
                    }
                } while (birthDate == DateTime.MinValue);

                Console.Write($"Введіть номер телефону для персоналу {i + 1}: ");
                string contactNumber;
                do
                {
                    contactNumber = Console.ReadLine();

                    if (!string.IsNullOrEmpty(contactNumber) && contactNumber.Length == 12 && contactNumber.StartsWith("+") && contactNumber.Substring(1).All(char.IsDigit))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректний номер телефону. Введіть номер у форматі 48538353801.");
                        Console.Write("Введіть номер телефону ще раз: ");
                    }
                } while (true);

                Console.Write($"Введіть посаду для персоналу {i + 1} (Receptionist/Administrator/Cleaner/Staff/Manager/Concierge/RegistrationManager/FrontDeskClerk): ");
                StaffPosition staffPosition;
                while (!Enum.TryParse(Console.ReadLine(), out staffPosition))
                {
                    Console.WriteLine("Некоректна посада. Спробуйте ще раз.");
                    Console.Write($"Введіть посаду для персоналу {i + 1}: ");
                }

                hotel.RegisterStaff(new HotelStaff(firstName, lastName, contactNumber, birthDate, staffPosition));
                Console.WriteLine("Персонал був успішно доданий до готелю.");
            }
        }

        static void RegisterGuest(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }
            try
            {
                Console.Write("Введіть ім'я гостя:");
                string firstName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(firstName) && firstName.Length >= 3 && firstName.All(char.IsLetter) && firstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть ім'я ще раз:");
                        firstName = Console.ReadLine();
                    }
                } while (true);

                Console.Write("Введіть прізвище гостя: ");
                string lastName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(lastName) && lastName.Length >= 3 && lastName.All(char.IsLetter) && lastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть прізвище ще раз:");
                        lastName = Console.ReadLine();
                    }
                } while (true);

                Console.Write("Введіть дату народження (у форматі ДД/ММ/РРРР): ");
                string inputDate;
                DateTime birthDate;
                do
                {
                    inputDate = Console.ReadLine();
                    if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
                    {
                        Console.WriteLine($"Введена дата народження: {birthDate.ToShortDateString()}");
                    }
                    else
                    {
                        Console.WriteLine("Некоректний формат дати. Спробуйте ще раз.");
                        Console.Write("Введіть дату народження (у форматі ДД/ММ/РРРР): ");
                    }
                } while (birthDate == DateTime.MinValue);
                Console.Write("Введіть номер телефону: ");
                string contactNumber;
                do
                {
                    contactNumber = Console.ReadLine();

                    if (!string.IsNullOrEmpty(contactNumber) && contactNumber.Length == 12 && contactNumber.StartsWith("+") && contactNumber.Substring(1).All(char.IsDigit))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректний номер телефону. Введіть номер у форматі +48538353801.");
                        Console.Write("Введіть номер телефону ще раз: ");
                    }
                } while (true);

                Console.Write("Введіть номер кімнати, в яку заселяється гість: ");
                if (!int.TryParse(Console.ReadLine(), out int roomNumber))
                {
                    Console.WriteLine("Неуореткне введення номеру телефону.");
                    return;
                }

                Room room = hotel.GetRoomByNumber(roomNumber);

                if (room == null)
                {
                    Console.WriteLine($"Кімната з номером {roomNumber} не знайдена.");
                    return;
                }

                Console.Write("Введіть ім'я співробітника, який заселяє гостя: ");
                string staffFirstName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(staffFirstName) && staffFirstName.Length >= 3 && staffFirstName.All(char.IsLetter) && staffFirstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть ім'я ще раз:");
                        staffFirstName = Console.ReadLine();
                    }
                } while (true);

                Console.Write("Введіть прізвище співробітника, який заселяє гостя: ");
                string staffLastName = Console.ReadLine();
                do
                {
                    if (!string.IsNullOrEmpty(staffLastName) && staffLastName.Length >= 3 && staffLastName.All(char.IsLetter) && staffLastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть ім'я ще раз:");
                        staffLastName = Console.ReadLine();
                    }
                } while (true);

                HotelStaff staff = hotel.GetStaffList().FirstOrDefault(s => s.FirstName == staffFirstName && s.LastName == staffLastName);

                if (staff == null)
                {
                    Console.WriteLine($"Співробітник {staffFirstName} {staffLastName} не знайдений.");
                    return;
                }

                Console.Write("Введіть кількість днів проживання: ");
                if (!int.TryParse(Console.ReadLine(), out int stayDuration) || stayDuration <= 0)
                {
                    Console.WriteLine("Некоректний ввід кількості днів проживання.");
                    return;
                }
                hotel.RegisterGuest(new Guest(firstName, lastName, contactNumber, birthDate), staff, room, stayDuration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка при реєстрації гостя: {ex.Message}");
            }
        }

        static void DisplayRegisteredGuests(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            var registeredGuests = hotel.GetCheckedInGuests();

            if (registeredGuests.Count == 0)
            {
                Console.WriteLine("Немає зареєстрованих гостей.");
            }
            else
            {
                Console.WriteLine("Зареєстровані гості:");
                foreach (var guest in registeredGuests)
                {
                    Console.WriteLine(guest.ToStringLivingGuests());
                }
            }
        }

        static void DisplayRemoveGuests(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            var checkedOutGuests = hotel.GetCheckedOutGuests();

            if (checkedOutGuests.Count == 0)
            {
                Console.WriteLine("Немає виселених гостей.");
            }
            else
            {
                Console.WriteLine("Виселені гості:");

                foreach (var guest in checkedOutGuests)
                {
                    Console.WriteLine(guest.ToStringCheckedOutGuests());
                }
            }
        }

        static void RemoveGuest(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            Console.Write("Введіть ім'я гостя для виїзду: ");
            string guestFirstName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(guestFirstName) && guestFirstName.Length >= 3 && guestFirstName.All(char.IsLetter) && guestFirstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть ім'я ще раз:");
                    guestFirstName = Console.ReadLine();
                }
            } while (true);

            Console.Write("Введіть прізвище гостя для виїзду: ");
            string guestLastName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(guestLastName) && guestLastName.Length >= 3 && guestLastName.All(char.IsLetter) && guestLastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть прізвище ще раз:");
                    guestLastName = Console.ReadLine();
                }
            } while (true);

            Guest guest = hotel.GetCheckedInGuests().FirstOrDefault(g => g.FirstName == guestFirstName && g.LastName == guestLastName);

            if (guest == null)
            {
                Console.WriteLine($"Гостя {guestFirstName} {guestLastName} не знайдено.");
                return;
            }

            Console.Write("Введіть номер кімнати гостя для виїзду: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Неправильне введення номера кімнати.");
                return;
            }

            Room room = hotel.GetRoomByNumber(roomNumber);

            if (room == null)
            {
                Console.WriteLine($"Номер кімнати {roomNumber} не знайдено.");
                return;
            }

            Console.Write("Введіть ім'я співробітника, який виселяє гостя: ");
            string staffFirstName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(staffFirstName) && staffFirstName.Length >= 3 && staffFirstName.All(char.IsLetter) && staffFirstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть ім'я ще раз:");
                    staffFirstName = Console.ReadLine();
                }
            } while (true);

            Console.Write("Введіть прізвище співробітника, який виселяє гостя");
            string staffLastName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(guestLastName) && guestLastName.Length >= 3 && guestLastName.All(char.IsLetter) && guestLastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть ім'я ще раз:");
                    guestLastName = Console.ReadLine();
                }
            } while (true);

            HotelStaff staff = hotel.GetStaffList().FirstOrDefault(s => s.FirstName == staffFirstName && s.LastName == staffLastName);

            if (staff == null)
            {
                Console.WriteLine($"Співробітник {staffFirstName} {staffLastName} не знайдено.");
                return;
            }

            try
            {
                hotel.RemoveGuest(guest, staff, room);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підчас виселення гостя: {ex.Message}");
            }

        }

        static void DisplayDismissedStaff(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            var dismissedStaff = hotel.GetDismissedStaff();

            if (dismissedStaff.Count == 0)
            {
                Console.WriteLine("Звільнених працівників немає");
            }
            else
            {
                Console.WriteLine("Звільнені працівники:");

                foreach (var staffMember in dismissedStaff)
                {
                    Console.WriteLine(staffMember.ToStringDismissed());
                }
            }
        }
        static void PrintHotelStaff(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            var hotelStaff = hotel.staffList;

            if (hotelStaff.Count == 0)
            {
                Console.WriteLine("Немає персоналу в готелі.");
            }
            else
            {
                Console.WriteLine("Персонал готелю:");

                foreach (var staffMember in hotelStaff)
                {
                    Console.WriteLine(staffMember.ToStringAcctiveStaff());
                }
            }
        }

        static void PrintHotelInfo(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }
            Console.WriteLine(hotel.ToString());
        }

        static void PrintRoomInfo(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            var roomList = hotel.GetRoomList();

            if (roomList.Count == 0)
            {
                Console.WriteLine("Немає доступних номерів в готелі.");
            }
            else
            {
                Console.WriteLine("Доступні номери:");
                foreach (var room in roomList)
                {
                    Console.WriteLine(room.ToString());
                }
            }
        }

        static void RemoveStaff(Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("Спочатку створіть готель.");
                return;
            }

            Console.Write("Введіть ім'я працівника для звільнення: ");
            string staffFirstName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(staffFirstName) && staffFirstName.Length >= 3 && staffFirstName.All(char.IsLetter) && staffFirstName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть ім'я ще раз:");
                    staffFirstName = Console.ReadLine();
                }
            } while (true);

            Console.Write("Введіть прізвище працівника для звільнення:  ");
            string staffLastName = Console.ReadLine();
            do
            {
                if (!string.IsNullOrEmpty(staffLastName) && staffLastName.Length >= 3 && staffLastName.All(char.IsLetter) && staffLastName.All(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z'))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Помилка: Некоректне прізвище . Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                    Console.WriteLine("Введіть прізвище ще раз:");
                    staffLastName = Console.ReadLine();
                }
            } while (true);

            HotelStaff staffm = hotel.GetStaffList().FirstOrDefault(s => s.FirstName == staffFirstName && s.LastName == staffLastName);

            if (staffm == null)
            {
                Console.WriteLine($"Працівника {staffFirstName} {staffLastName} не знайдено.");
                return;
            }

            try
            {
                hotel.RemoveStaff(staffm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка під час звільнення працівника: {ex.Message}");
            }
        }
    }
}