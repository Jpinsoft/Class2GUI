using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jpinsoft.Class2GUITest.SampleTypes
{
    public class RootComplexType
    {
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public ComplexCar Car { get; set; }

        public ComplexUser LastEditedBy { get; set; }

        public bool IsArchived { get; set; }
    }
}
