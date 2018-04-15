using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC.Models
{
    public class MedicComparer : IEqualityComparer<Medic>
    {
        public bool Equals(Medic x, Medic y)
        {
            return x.firstName.Equals(y.firstName) && x.lastName.Equals(y.lastName) && x.userName.Equals(y.userName);
        }

        public int GetHashCode(Medic obj)
        {
            return string.Format("{0}{1}{2}", obj.firstName, obj.lastName, obj.userName).GetHashCode();
        }
    }
}