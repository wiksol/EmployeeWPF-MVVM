using Second.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Second
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SecondDbContext database = new SecondDbContext("Server =.; Database = SecondDatabase; Trusted_Connection = True; ");

            DatabaseLocator.Database = database;
        }
    }
}
