using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public class Guest : PersonBase
    {
        private static int _checkedInIdCounter = 0;
        private static int _checkedOutIdCounter = 0;

        public int Id { get; set; }
        public virtual string ContactNumber
        {
            get => base.ContactNumber;
            set => base.ContactNumber = value;
        }
        public DateTime BirthDate { get; set; }

        public virtual string FirstName
        {
            get => base.FirstName;
            set => base.FirstName = value;
        }

        public virtual string LastName
        {
            get => base.LastName;
            set => base.LastName = value;
        }

        public int StayDuration { get; set; }
        public HotelStaff CheckedInBy { get; set; }
        public HotelStaff CheckedOutBy { get; set; }
        public Room CurrentRoom { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        public Guest(string firstname, string lastname, string contactNumber, DateTime birthdate)
        {
            if (CheckedOutBy == null)
            {
                IncrementCheckedInId();
                Id = _checkedInIdCounter;
            }
            else
            {
                IncrementCheckedOutId();
                Id = _checkedOutIdCounter;
            }

            FirstName = firstname;
            LastName = lastname;
            ContactNumber = contactNumber;
            BirthDate = birthdate;
        }

        private static void IncrementCheckedInId()
        {
            _checkedInIdCounter++;
        }

        private static void IncrementCheckedOutId()
        {
            _checkedOutIdCounter++;
        }

        //Метод ToString для виведення інформації про проживаючих гостей
        public string ToStringLivingGuests()
        {
            string roomInfo = CurrentRoom != null
                ? $", Room Number: {CurrentRoom.RoomNumber}"
                : "";

            string checkInInfo = CheckInTime != null && StayDuration > 0
                ? $", Check-In Time: {CheckInTime}, Stay Duration: {StayDuration} days"
                : "";

            string checkedInByInfo = CheckedInBy != null
                ? $", Checked In By: {CheckedInBy.Get_Full_Name()}, Staff Position: {CheckedInBy.StaffPosition}"
                : "";

            return $"Guest Id: {Id}, Full Name: {Get_Full_Name()}, Birthdate: {BirthDate.ToShortDateString()}, Contact Number: {ContactNumber}{roomInfo}{checkInInfo}{checkedInByInfo}";
        }

        // Метод ToString для виведення інформації про виселених гостей
        public string ToStringCheckedOutGuests()
        {
            string checkOutInfo = CheckOutTime != null
                ? $", Check-Out Time: {CheckOutTime}"
                : "";

            string checkedOutByInfo = CheckedOutBy != null
                ? $", Checked Out By: {CheckedOutBy.Get_Full_Name()}, Staff Position: {CheckedOutBy.StaffPosition}"
                : "";

            return $"Guest Id: {Id}, Full Name: {Get_Full_Name()}, Birthdate: {BirthDate.ToShortDateString()}, Contact Number: {ContactNumber}{checkOutInfo}{checkedOutByInfo}";
        }

        // Перевизначення методу GetFullName
        public override string Get_Full_Name()
        {
            return base.Get_Full_Name();
        }

        public string GetContactNumber()
        {
            return $"{ContactNumber}";
        }
    }
}
