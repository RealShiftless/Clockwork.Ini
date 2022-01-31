using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini
{
    class IniParserException : Exception
    {
        public IniParserException(int line, int chr, string message) : base("IniParser error at [" + line + ":" + chr + "]\n  " + message) { }
    }
}
