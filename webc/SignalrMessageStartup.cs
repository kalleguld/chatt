﻿using Microsoft.Owin;
using Owin;
using webc;

[assembly: OwinStartup(typeof(SignalrMessageStartup))]

namespace webc
{
    public class SignalrMessageStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
