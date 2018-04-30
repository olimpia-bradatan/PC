using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC.Models
{
    public partial class Archive
    {
        public int appointmentId
        {
            get;
            set;
        }
        public string Diagnostic { get; set; }
        public string Medication { get; set; }
        public bool Free { get; set; }
    }
}