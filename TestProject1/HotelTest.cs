using Project_partC_Horbach_program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class HotelTests
    {
        [TestMethod]
        public void AddRoom_NewRoom_ShouldAddRoomToList()
        {
            // Arrange
            var hotel = new Hotel("Country", "City");
            var room = new Room(101, 100, RoomType.Single);

            // Act
            hotel.AddRoom(room);

            // Assert
            CollectionAssert.Contains(hotel.Rooms, room);
        }

        [TestMethod]
        public void AddRoom_DuplicateRoom_ShouldThrowException()
        {
            // Arrange
            var hotel = new Hotel("Country", "City");
            var room1 = new Room(101, 100, RoomType.Single);
            var room2 = new Room(101, 200, RoomType.Double);

            // Act
            hotel.AddRoom(room1);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.AddRoom(room2));
        }
        // Перевірка, чи коректно виписується гість при виклику методу CheckOut
        [TestMethod]
        public void RemoveGuest_ValidGuest_ShouldCheckOutGuest()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;

            hotel.AddRoom(room);
            hotel.RegisterGuest(guest, staff, room, day); // Реєстрація гостя в готелі

            // Act
            hotel.RemoveGuest(guest, staff, room);

            // Assert
            Assert.IsTrue(hotel.GetCheckedOutGuests().Contains(guest)); // Перевірка, що гостя виписано
        }

        // Перевірка, чи виникає виняток для невірного гостя при виклику методу CheckOut
        [TestMethod]
        public void CheckOut_InvalidGuest_ShouldThrowException()
        {
            // Arrange
            var hotel = new Hotel("Country", "City");
            var invalidGuest = new Guest("Invalid", "Guest", "+48538353801", new DateTime(1995, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);


            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RemoveGuest(invalidGuest, staff, room));
        }

        // Перевірка, що метод GetGuestList() повертає порожній список, якщо немає зареєстрованих гостей
        [TestMethod]
        public void GetGuestList_NoGuestsRegistered_ShouldReturnEmptyList()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");

            // Act
            var guestList = hotel.GetGuestList();

            // Assert
            Assert.IsNotNull(guestList);
            Assert.AreEqual(0, guestList.Count);

        }

        // Перевірка, що метод GetGuestList() повертає список всіх зареєстрованих гостей
        [TestMethod]
        public void GetGuestList_ReturnsListOfRegisteredGuests()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest1 = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353802", new DateTime(1990, 1, 1));
            var guest2 = new Project_partC_Horbach_program.Guest("Seyran", "Korhan", "+48538353802", new DateTime(1995, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var room1 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;
            hotel.AddRoom(room);
            hotel.AddRoom(room1);
            hotel.RegisterGuest(guest1, staff, room, day);
            hotel.RegisterGuest(guest2, staff, room1, day);

            // Act
            var guestList = hotel.GetCheckedInGuests();

            // Assert
            Assert.IsNotNull(guestList);
            Assert.AreEqual(2, guestList.Count);
            CollectionAssert.Contains(guestList, guest1);
            CollectionAssert.Contains(guestList, guest2);
        }

        // Перевірка, що метод GetGuestList() повертає порожній список після виселення всіх гостей
        [TestMethod]
        public void GetGuestList_ReturnsEmptyListAfterCheckingOutAllGuests()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room1 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;

            hotel.AddRoom(room1);
            hotel.RegisterGuest(guest, staff, room1, day);
            hotel.RemoveGuest(guest, staff, room1);

            // Act
            var guestList = hotel.GetGuestList();

            // Assert
            Assert.IsNotNull(guestList);
            Assert.AreEqual(1, guestList.Count);
        }

        // Перевірка, що метод GetGuestList() правильно повертає список гостей після декількох операцій
        [TestMethod]
        public void GetGuestList_ReturnsListOfRegisteredGuestsAfterSeveralOperations()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest1 = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353802", new DateTime(1990, 1, 1));
            var guest2 = new Project_partC_Horbach_program.Guest("Seyran", "Korhan", "+48536353802", new DateTime(1995, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var room1 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;
            hotel.AddRoom(room);
            hotel.AddRoom(room1);
            hotel.RegisterGuest(guest1, staff, room, day);
            hotel.RegisterGuest(guest2, staff, room1, day);
            hotel.RemoveGuest(guest1, staff, room);

            // Act
            var guestList = hotel.GetGuestList();

            // Assert
            Assert.IsNotNull(guestList);
            Assert.AreEqual(2, guestList.Count);
            CollectionAssert.Contains(guestList, guest2);
            CollectionAssert.Contains(guestList, guest1);
        }

        // Перевірка, що метод GetRoomList() повертає порожній список, якщо немає доступних номерів
        [TestMethod]
        public void GetRoomList_NoRoomsAvailable_ShouldReturnEmptyList()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");

            // Act
            var roomList = hotel.GetRoomList();

            // Assert
            Assert.IsNotNull(roomList);
            Assert.AreEqual(0, roomList.Count);
        }

        // Перевірка, що метод GetRoomList() повертає список всіх доступних номерів
        [TestMethod]
        public void GetRoomList_ReturnsListOfAvailableRooms()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var room1 = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var room2 = new Project_partC_Horbach_program.Room(102, 150, RoomType.Double);
            hotel.Rooms.Add(room1);
            hotel.Rooms.Add(room2);

            // Act
            var roomList = hotel.GetRoomList();

            // Assert
            Assert.IsNotNull(roomList);
            Assert.AreEqual(2, roomList.Count);
            CollectionAssert.Contains(roomList, room1);
            CollectionAssert.Contains(roomList, room2);

        }

        // Перевірка, що метод GetRoomList() повертає список доступних номерів після виселення гостя
        [TestMethod]
        public void GetRoomList_ReturnsListOfAvailableRoomsAfterCheckOut()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room1 = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var room2 = new Project_partC_Horbach_program.Room(102, 150, RoomType.Double);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;

            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            hotel.RegisterGuest(guest, staff, room1, day);
            hotel.RegisterGuest(guest, staff, room2, day);
            hotel.RemoveGuest(guest, staff, room1);

            // Act
            var roomList = hotel.GetRoomList();

            // Assert
            Assert.IsNotNull(roomList);
            Assert.AreEqual(2, roomList.Count);
            CollectionAssert.Contains(roomList, room1);
            CollectionAssert.Contains(roomList, room2);
        }

        // Перевірка, що метод GetStaffList() повертає порожній список, якщо немає доступного персоналу
        [TestMethod]
        public void GetStaffList_NoStaffAvailable_ShouldReturnEmptyList()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");

            // Act
            var staffList = hotel.GetStaffList();

            // Assert
            Assert.IsNotNull(staffList);
            Assert.AreEqual(0, staffList.Count);
        }

        // Перевірка, що метод GetStaffList() повертає список всього доступного персоналу
        [TestMethod]
        public void GetStaffList_ReturnsListOfAvailableStaff()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var staff1 = new Project_partC_Horbach_program.HotelStaff("Mikhael", "Dolan", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Staff);
            var staff2 = new Project_partC_Horbach_program.HotelStaff("Lara", "Doe", "+48538353802", new DateTime(1995, 1, 1), StaffPosition.Manager);
            hotel.RegisterStaff(staff1);
            hotel.RegisterStaff(staff2);

            // Act
            var staffList = hotel.GetStaffList();

            // Assert
            Assert.IsNotNull(staffList);
            Assert.AreEqual(2, staffList.Count);
            CollectionAssert.Contains(staffList, staff1);
            CollectionAssert.Contains(staffList, staff2);
        }

        // Перевірка, що метод GetStaffList() повертає порожній список після видалення персоналу
        [TestMethod]
        public void GetStaffList_ReturnsEmptyListAfterRemovingStaff()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var staff = new Project_partC_Horbach_program.HotelStaff("Mikhael", "Dolan", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Staff);
            hotel.RegisterStaff(staff);
            hotel.RemoveStaff(staff);

            // Act
            var staffList = hotel.GetStaffList();

            // Assert
            Assert.IsNotNull(staffList);
            Assert.AreEqual(1, staffList.Count);
        }

        [TestMethod]
        public void GetStaffList_ReturnsListOfAvailableStaffAfterSeveralOperations()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var staff1 = new Project_partC_Horbach_program.HotelStaff("Mikhael", "Dolan", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Staff);
            var staff2 = new Project_partC_Horbach_program.HotelStaff("Lara", "Doe", "+48538353802", new DateTime(1995, 1, 1), StaffPosition.Manager);
            hotel.RegisterStaff(staff1);
            hotel.RegisterStaff(staff2);
            hotel.RemoveStaff(staff1);

            // Act
            var staffList = hotel.GetStaffList();

            // Assert
            Assert.IsNotNull(staffList);
            Assert.AreEqual(2, staffList.Count);
            CollectionAssert.Contains(staffList, staff2);
            CollectionAssert.Contains(staffList, staff1);
        }

        // Перевірка, що гість був успішно зареєстрований
        [TestMethod]
        public void RegisterGuest_AddValidGuest_ShouldRegisterGuest()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room1 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;

            // Act
            hotel.AddRoom(room1);
            hotel.RegisterGuest(guest, staff, room1, day);

            // Assert
            Assert.IsTrue(hotel.GetCheckedInGuests().Contains(guest));
        }

        // Перевірка, що спроба зареєструвати того ж самого гостя двічі призводить до винятку
        [TestMethod]
        public void RegisterGuest_AddDuplicateGuest_ShouldThrowException()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room1 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;
            hotel.AddRoom(room1);
            hotel.RegisterGuest(guest, staff, room1, day);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RegisterGuest(guest, staff, room1, day));
        }

        // Перевірка, що гість успішно заселений в номер
        [TestMethod]
        public void RegisterGuest_ValidCheckIn_ShouldCheckInGuest()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var day = 3;


            // Act
            hotel.AddRoom(room);
            hotel.RegisterGuest(guest, staff, room, day);

            // Assert
            Assert.IsTrue(room.IsOccupied);
        }

        // Перевірка, що спроба заселити гостя в незареєстрований номер призводить до винятку
        [TestMethod]
        public void CheckIn_CheckInInvalidRoom_ShouldThrowException()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var invalidRoom = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single); // Номер не зареєстрований в готелі
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RemoveGuest(guest, staff, invalidRoom));
        }

        // Перевірка, що спроба заселити гостя неправильним персоналом призводить до винятку
        [TestMethod]
        public void CheckIn_InvalidStaffPosition_ShouldThrowException()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Project_partC_Horbach_program.Room(101, 100, RoomType.Single);
            var invalidStaff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Cleaner);
            var day = 3;
            hotel.AddRoom(room);
            // Персонал з неправильною посадою для рецепції

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RegisterGuest(guest, invalidStaff, room, day));
        }


        // Перевірка, що спроба видалити неправильний персонал призводить до винятку
        [TestMethod]
        public void RemoveStaff_InvalidRemoval_ShouldThrowException()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Administrator);
            // Персонал з неправильною посадою для видалення

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RemoveStaff(staff));
        }

        // Перевірка, що спроба видалити персонал з порожнього списку призводить до винятку
        [TestMethod]
        public void RemoveStaff_EmptyStaffList_ShouldThrowException()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var staff = new Project_partC_Horbach_program.HotelStaff("Staff", "Member", "+48538353801", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            // Персонал, якого немає в готелі

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => hotel.RemoveStaff(staff));
        }

        // Перевірка, що метод ToString() повертає правильний рядок
        [TestMethod]
        public void ToString_ReturnsCorrectString()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");
            var guest1 = new Project_partC_Horbach_program.Guest("Ferit", "Korhan", "+48538353802", new DateTime(1990, 1, 1));
            var guest2 = new Project_partC_Horbach_program.Guest("Seyran", "Korhan", "+48538353802", new DateTime(1995, 1, 1));
            var staff = new Project_partC_Horbach_program.HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Receptionist);
            var room1 = new Project_partC_Horbach_program.Room(101, 100, RoomType.Double);
            var room2 = new Project_partC_Horbach_program.Room(102, 100, RoomType.Double);
            var stayDuration1 = 4;
            var stayDuration2 = 3;
            guest1.Id = 1;
            guest2.Id = 2;
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);

            hotel.RegisterStaff(staff);
            hotel.RegisterGuest(guest1, staff, room1, stayDuration1);
            hotel.RegisterGuest(guest2, staff, room2, stayDuration2);

            // Act
            // Act
            var actualResult = hotel.ToString();
            var expectedRoomCount = hotel.GetRoomList().Count;
            var expectedStaffCount = hotel.GetStaffList().Count;
            var expectedGuestCount = hotel.GetGuestList().Count;
            var expectedResult = $"Country, City - {expectedRoomCount} кімнат, {expectedStaffCount} персонал, {expectedGuestCount} гостей в готелі з яких : {hotel.GetCheckedInGuests().Count} гостей (Checked In), {hotel.GetCheckedOutGuests().Count} гостей (Checked Out)";

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedRoomCount, 2);
            Assert.AreEqual(expectedStaffCount, 1);
            Assert.AreEqual(hotel.GetCheckedInGuests().Count, 2);
            Assert.AreEqual(hotel.GetCheckedOutGuests().Count, 0);
        }


        //Перевіряє, чи обробляє метод ToString() вірно випадок порожнього готелю, повертаючи коректну інформацію.
        [TestMethod]
        public void ToString_EmptyHotel_ShouldReturnCorrectString()
        {
            // Arrange
            var hotel = new Project_partC_Horbach_program.Hotel("Country", "City");

            // Act
            var hotelInfo = hotel.ToString();

            // Assert
            Assert.IsNotNull(hotelInfo);
            Assert.IsTrue(hotelInfo.Contains("Country"));
            Assert.IsTrue(hotelInfo.Contains("City"));
            Assert.IsFalse(hotelInfo.Contains("Guests:"));
            Assert.IsFalse(hotelInfo.Contains("Rooms:"));
            Assert.IsFalse(hotelInfo.Contains("Staff:"));
        }
    }
}
