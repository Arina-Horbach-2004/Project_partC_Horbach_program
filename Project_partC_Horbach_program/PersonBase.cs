using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public abstract class PersonBase : IPerson
    {
        private string firstName;
        private string lastName;
        private string contactNumber;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                do
                {
                    if (!string.IsNullOrEmpty(value) && value.Length >= 3 && value.All(char.IsLetter))
                    {
                        firstName = value;
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне ім'я . Ім'я повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть ім'я ще раз:");
                        value = Console.ReadLine();
                    }
                } while (true);

            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                do
                {
                    if (!string.IsNullOrEmpty(value) && value.Length >= 3 && value.All(char.IsLetter))
                    {
                        lastName = value;
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Помилка: Некоректне прізвище .Прізвище повинно містити не менше трьох літер латинського алфавіту.");
                        Console.WriteLine("Введіть  прізвище ще раз:");
                        value = Console.ReadLine();
                    }
                } while (true);

            }
        }

        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                do
                {
                    if (!string.IsNullOrEmpty(value) && value.Length == 12 && value.StartsWith("+") && value.Substring(1).All(char.IsDigit))
                    {
                        contactNumber = value;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Некоректний номер телефону. Повторіть спробу.");
                        Console.WriteLine("Введіть  Номер телефону" +
                            " ще раз:");
                        value = Console.ReadLine();

                    }
                } while (true);
            }
        }

        public virtual string Get_Full_Name()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
