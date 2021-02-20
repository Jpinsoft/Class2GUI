using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUITest.SampleTypes
{
    public class ComplexCar
    {
        public ComplexUser Owner { get; set; }

        public string VIN { get; set; }

        public CarTypeEnum CarType { get; set; }

        public List<ComplexUser> Drivers { get; set; }
    }
}
