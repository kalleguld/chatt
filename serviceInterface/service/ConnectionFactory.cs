using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serviceInterface.service
{
    public class ConnectionFactory
    {
        public Connection GetConnection()
        {
            return new Connection();
        }
    }
}
