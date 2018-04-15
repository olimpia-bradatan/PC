using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC.Models
{
    public class AssistantComparer
    {

        public bool Equals(Assistant x, Assistant y)
        {
            return  x.userName.Equals(y.userName);
        }

    }
}