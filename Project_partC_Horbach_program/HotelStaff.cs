using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public class HotelStaff : PersonBase
    {
        private static int idCounter = 0;

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

        public virtual string ContactNumber
        {
            get => base.ContactNumber;
            set => base.ContactNumber = value;
        }

        public StaffPosition StaffPosition { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsDismissed { get; set; }

        public int Id { get; set; }

        public HotelStaff(string firstName, string lastname, string contactNumber, DateTime birthDate, StaffPosition position)
        {
            idCounter++;
            Id = idCounter;
            FirstName = firstName;
            LastName = lastname;
            ContactNumber = contactNumber;
            BirthDate = birthDate;
            StaffPosition = position;
        }

        public override string Get_Full_Name()
        {
            return base.Get_Full_Name();
        }

        public string GetContactNumber()
        {
            return ContactNumber;
        }

        public string ToStringAcctiveStaff()
        {
            return $" Staff Id: {Id}, Name: {Get_Full_Name()}, Contact Number: {ContactNumber}, Birthdate: {BirthDate.ToShortDateString()}, Position: {StaffPosition}";
        }

        public string ToStringDismissed()
        {
            return $" Dismissed Staff Id: {Id}, Name: {Get_Full_Name()}, Contact Number: {ContactNumber}, Birthdate: {BirthDate.ToShortDateString()}, Position: {StaffPosition}";
        }
    }
}
