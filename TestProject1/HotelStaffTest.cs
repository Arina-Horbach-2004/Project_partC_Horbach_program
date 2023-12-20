using Project_partC_Horbach_program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class HotellStaffTest
    {

        //Перевіряє, чи коректно встановлені значення полів Name, ContactNumber, BirthDate і StaffPosition при створенні об'єкта гостя.
        [TestMethod]
        public void GuestInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var firstname = "Admin";
            var lastname = "Kim";
            var contactNumber = "+48538353802";
            var birthDate = new DateTime(1990, 1, 1);
            var staffpossition = StaffPosition.Administrator;

            // Act
            var staff = new HotelStaff(firstname, lastname, contactNumber, birthDate, staffpossition);

            // Assert
            Assert.AreEqual(firstname, staff.FirstName);
            Assert.AreEqual(lastname, staff.LastName);
            Assert.AreEqual(contactNumber, staff.ContactNumber);
            Assert.AreEqual(birthDate, staff.BirthDate);
            Assert.AreEqual(staffpossition, staff.StaffPosition);
        }

        //Перевіряє, чи властивість StaffPosition встановлюється правильно при створенні об'єкта класу HotelStaff.
        [TestMethod]
        public void StaffPosition_SetPosition_ShouldSetStaffPosition()
        {
            // Arrange
            var staff = new HotelStaff("Admin", "kim", "+48536354802", new DateTime(1990, 1, 1), StaffPosition.Administrator);

            // Act
            var position = staff.StaffPosition;

            // Assert
            Assert.AreEqual(StaffPosition.Administrator, position);
        }

        // Перевіряє, чи контактний номер встановлюється правильно для об'єкта класу HotelStaff.
        [TestMethod]
        public void ContactNumber_ValidFormat_ShouldSetContactNumber()
        {
            // Arrange
            var staff = new HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Administrator);

            // Act
            var contactNumber = staff.GetContactNumber();

            // Assert
            Assert.AreEqual("+48538353802", contactNumber);
        }

        //Перевіряє, чи метод GetFullName класу HotelStaff повертає коректне повне ім'я персоналу
        [TestMethod]
        public void GetFullName_ShouldReturnCorrectFullName()
        {
            // Arrange
            var staff = new HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Administrator);

            // Act
            var fullName = staff.Get_Full_Name();

            // Assert
            Assert.AreEqual(" Admin Kim", fullName);
        }

        //Перевіряє, чи метод ToString персоналу повертає коректну інформацію
        [TestMethod]
        public void ToString_ShouldReturnCorrectInformation()
        {
            // Arrange
            var staff = new HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Administrator);


            // Act
            var staffInfo = staff.ToStringAcctiveStaff();

            // Assert
            Assert.IsNotNull(staffInfo);
            Assert.IsTrue(staffInfo.Contains("Admin Kim"));
            Assert.IsTrue(staffInfo.Contains("+48538353802"));
            Assert.IsTrue(staffInfo.Contains("01/01/1990"));
            Assert.IsTrue(staffInfo.Contains("Administrator"));
        }
    }
}
