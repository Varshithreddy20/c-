using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectclass
{
    public class Person
    {
        public string PersonNAME { get; set; }
        public string email { get; set; }

        public override bool Equals(object obj)
        {
            Person p = (Person)obj;
            if (this.PersonNAME == p.PersonNAME && this.email == p.email)
            {
                return true;
            }
            else
            {
                return false;
            }
    
        }


        public override string ToString()
        {
            return "Person name:"+this.PersonNAME;
        }
    }





    
}
