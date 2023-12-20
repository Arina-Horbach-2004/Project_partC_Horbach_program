using Project_partC_Horbach_program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class CheckOutLogTest
    {
        [TestMethod]
        public void CheckOutLogInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var checkOutTime = DateTime.Now;
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Room(101, 150.0, RoomType.Single);
            var staff = new HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Administrator);

            // Act
            var checkOutLog = new CheckOutLog(checkOutTime, guest, room, staff);

            // Assert
            Assert.AreEqual(checkOutTime, checkOutLog.CheckOutTime);
            Assert.AreEqual(guest, checkOutLog.Guest);
            Assert.AreEqual(room, checkOutLog.Room);
            Assert.AreEqual(staff, checkOutLog.CheckedOutBy);
        }
    }
}


