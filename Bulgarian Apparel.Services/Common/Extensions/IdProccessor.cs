using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bulgarian_Apparel.Web.Infrastructure
{
    public static class IdProccessor
    {

        public static Guid GetGuidForStringId(string guid)
        {
            return (Guid.Parse(guid));
        }
    }
}