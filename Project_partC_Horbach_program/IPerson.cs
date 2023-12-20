using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_partC_Horbach_program;

namespace Project_partC_Horbach_program
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string ContactNumber { get; set; }
    }
}
