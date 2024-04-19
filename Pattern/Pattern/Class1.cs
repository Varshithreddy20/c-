using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ParentClass
    {
        public int x {  get; set; }
    }

    public class ChildClass: ParentClass
    {
        public int y { get; set; }
    }
}
