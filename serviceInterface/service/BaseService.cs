﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serviceInterface.service
{
    public class BaseService
    {
        protected Connection Connection { get; private set; }

        protected BaseService(Connection connection)
        {
            Connection = connection;
        }
    }
}
