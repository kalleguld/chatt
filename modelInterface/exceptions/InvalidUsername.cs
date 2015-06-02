using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace modelInterface.exceptions
{
    public class InvalidUsername : Exception
    {
        public string Username { get; private set; }
        public Regex Regex { get; private set; }

        public InvalidUsername(string username, Regex regex)
        {
            Username = username;
            Regex = regex;
        }
    }
}
