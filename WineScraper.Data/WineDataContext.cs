using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace WineScraper.Data
{
    public partial class WineDataContext
    {
        public static readonly string WINESCRAPER_CONNECTION_STRING_NAME = "WineScraper.Properties.Settings.WineConnectionString";

        public static WineDataContext GetNewContextFromConfigFile()
        {
            var cs = ConfigurationManager.ConnectionStrings[WINESCRAPER_CONNECTION_STRING_NAME];
            if (cs == null && ConfigurationManager.ConnectionStrings.Count > 0)
            {
                cs = ConfigurationManager.ConnectionStrings[0];
            }
            if (cs == null)
            {
                return null;
            }
            try
            {
                return new WineDataContext(cs.ConnectionString);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
