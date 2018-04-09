using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC.Models
{
    public class AppointmentComparer: IEqualityComparer<Appointment>
    {
        public bool Equals(Appointment x, Appointment y)
        {
            return x.cardNumber.Equals(y.cardNumber) && x.Date.Equals(y.Date) && x.Time.Equals(y.Time);
        }

        public int GetHashCode(Appointment obj)
        {
            return string.Format("{0}{1}{2}", obj.cardNumber, obj.Date, obj.Time).GetHashCode();
        }
    }
}