using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Usda.PlantBreeding.Site
{
    public static class AppSettingsManager
    {

        public static bool IsSandbox
        {
            get
            {
                return Properties.Settings.Default.SanboxFlag;
            }
        }

        public static string SandboxName
        {
            get
            {
                return Properties.Settings.Default.SandboxName;
            }
        }

    }
}