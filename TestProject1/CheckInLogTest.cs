using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace TestProject1
{
    [TestClass]
    public class CheckInLogTest
    {
        [TestMethod]
        public void CheckInLogInitialization_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var checkInTime = DateTime.Now;
            var guest = new Guest("Ferit", "Korhan", "+48538353801", new DateTime(1990, 1, 1));
            var room = new Room(101, 150.0, RoomType.Single);
            var staff = new HotelStaff("Admin", "Kim", "+48538353802", new DateTime(1990, 1, 1), StaffPosition.Administrator);

            // Act
            var checkInLog = new CheckInLog(checkInTime, guest, room, staff);

            // Assert
            Assert.AreEqual(checkInTime, checkInLog.CheckInTime);
            Assert.AreEqual(guest, checkInLog.Guest);
            Assert.AreEqual(room, checkInLog.Room);
            Assert.AreEqual(staff, checkInLog.CheckedInBy);
        }
    }
}