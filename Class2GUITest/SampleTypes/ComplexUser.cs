using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUITest.SampleTypes
{
    public class ComplexUser
    {
        public SimpleAddress Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte Age { get; set; }

        public string Email { get; set; }

        public SexEnum Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsAdult { get { return (DateTime.Now - BirthDate).TotalDays > (18 * 365); } }
    }
}
