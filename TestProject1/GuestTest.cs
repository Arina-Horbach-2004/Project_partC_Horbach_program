using System;
using Project_partC_Horbach_program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProject1
{
    [TestClass]
    public class GuestTest
    {
        [TestMethod]
        public void GuestInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var firstname = "Ferit";
            var lastname = "Korhan";
            var contactNumber = "+48538353801"; // Provide a valid contact number
            var birthDate = new DateTime(1990, 1, 1);

            // Act
            var guest = new Guest(firstname, lastname, contactNumber, birthDate);

            // Assert
            Assert.AreEqual(firstname, guest.FirstName);
            Assert.AreEqual(lastname, guest.LastName);
            Assert.AreEqual(contactNumber, guest.ContactNumber);
            Assert.AreEqual(birthDate, guest.BirthDate);
        }

        //Перевіряє, чи метод GetFullName повертає коректне повне ім'я гостя.
        [TestMethod]
        public void GetFullName_ShouldReturnCorrectFullName()
        {
            // Arrange
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));

            // Act
            var fullName = guest.Get_Full_Name();

            // Assert
            Assert.AreEqual("Ferit Korhan", fullName);
        }

        //Перевіряє, чи метод GetContactNumber повертає очікуваний контактний номер для гостя з коректним форматом.
        [TestMethod]
        public void GetContactNumber_ValidFormat_ShouldReturnContactNumber()
        {
            // Arrange
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));

            // Act
            var contactNumber = guest.GetContactNumber();

            // Assert
            Assert.AreEqual("+48538353801", contactNumber);
        }

        [TestMethod]
        public void ToStringLivingGuests_ShouldReturnCorrectInformation()
        {
            // Arrange
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Room(101, 150.0, RoomType.Single);
            var staff = new HotelStaff("John", "Doe", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Receptionist);

            // Set guest properties
            guest.CheckInTime = DateTime.Now.AddHours(-1); // Assuming the guest checked in 1 hour ago
            guest.StayDuration = 3; // Assuming the guest stays for 3 days
            guest.CurrentRoom = room;
            guest.CheckedInBy = staff;

            // Act
            var guestInfo = guest.ToStringLivingGuests();

            // Assert
            Assert.IsNotNull(guestInfo);
            Assert.IsTrue(guestInfo.Contains($"Guest Id: {guest.Id}"));
            Assert.IsTrue(guestInfo.Contains($"Full Name: {guest.Get_Full_Name()}"));
            Assert.IsTrue(guestInfo.Contains($"Birthdate: {guest.BirthDate.ToShortDateString()}"));
            Assert.IsTrue(guestInfo.Contains($"Contact Number: {guest.ContactNumber}"));
            Assert.IsTrue(guestInfo.Contains($"Room Number: {guest.CurrentRoom.RoomNumber}"));
            Assert.IsTrue(guestInfo.Contains($"Check-In Time: {guest.CheckInTime}, Stay Duration: {guest.StayDuration} days"));
            Assert.IsTrue(guestInfo.Contains($"Checked In By: {guest.CheckedInBy.Get_Full_Name()}, Staff Position: {guest.CheckedInBy.StaffPosition}"));
        }

        [TestMethod]
        public void ToStringCheckedOutGuests_ShouldReturnCorrectInformation()
        {
            // Arrange
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var staff = new HotelStaff("John", "Doe", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Receptionist);

            // Set guest properties
            guest.CheckOutTime = DateTime.Now; // Assuming the guest checked out now
            guest.CheckedOutBy = staff;

            // Act
            var guestInfo = guest.ToStringCheckedOutGuests();

            // Assert
            Assert.IsNotNull(guestInfo);
            Assert.IsTrue(guestInfo.Contains($"Guest Id: {guest.Id}"));
            Assert.IsTrue(guestInfo.Contains($"Full Name: {guest.Get_Full_Name()}"));
            Assert.IsTrue(guestInfo.Contains($"Birthdate: {guest.BirthDate.ToShortDateString()}"));
            Assert.IsTrue(guestInfo.Contains($"Contact Number: {guest.ContactNumber}"));
            Assert.IsTrue(guestInfo.Contains($"Check-Out Time: {guest.CheckOutTime}"));
            Assert.IsTrue(guestInfo.Contains($"Checked Out By: {guest.CheckedOutBy.Get_Full_Name()}, Staff Position: {guest.CheckedOutBy.StaffPosition}"));
        }
    }
}
