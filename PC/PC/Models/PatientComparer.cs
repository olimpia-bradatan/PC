using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC.Models
{
    public class PatientComparer
    {
        public bool Equals(Patient x, Patient y)
        {
            return x.cardNumber.Equals(y.cardNumber);
        }
    }
}