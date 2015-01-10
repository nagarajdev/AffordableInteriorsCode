using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoySol.Logger
{
    public enum ERRORTYPE : int
    {
        WARNING = 'W',
        INFO = 'I',
        ERROR = 'E',
        FATALS = 'F',
        DEBUGS = 'D',
        UNKNOWN = 0,
    }
}
