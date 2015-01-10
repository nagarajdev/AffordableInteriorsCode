using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoySol.Logger
{
    public interface ILogger
    {
        void Log(string message, Enum category);
        void Log(string message, Enum category, Exception ex);
    }
}
