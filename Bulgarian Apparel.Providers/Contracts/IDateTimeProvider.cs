using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulgarian_Apparel.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentTime();

        DateTime GetTimeFromCurrentTime(int hours, int minutes, int seconds);
    }
}
