using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;


namespace Usda.PlantBreeding.Site
{
    public static class SessionManager
    {
        private const string newSessionKey = "newSession";
        private const string redirectUrlKey = "redirectUrl";
        private const string sessionPageSizeKey = "sessionPageSize";

        public static UserPreference UserPreference
        {
            get
            {
                UserPreference userpreference = null;
                var user = HttpContext.Current.User.Identity.Name;
                using (IPlantBreedingRepo repo = new PlantBreedingRepo())
                {
                    userpreference = repo.GetUserPreference(user);
                }
                return userpreference;
            }
            set
            {
                using (IPlantBreedingRepo repo = new PlantBreedingRepo())
                {
                    if (value.GenusId > 0 && value.UserId != string.Empty)
                    {
                        repo.SaveUserPreference(value);
                    }
                }
            }
        }

        public static Genus Genus
        {
            get
            {
                Genus genus = null;
                var user =  HttpContext.Current.User.Identity.Name;
                using (IPlantBreedingRepo repo = new PlantBreedingRepo())
                {
                    var preference = repo.GetUserPreference(user);
                    if (preference != null && preference.GenusId > 0)
                    {
                        genus = repo.GetGenus(preference.GenusId);
                    }

                }
                return genus;
            }
            set
            {
                var user = HttpContext.Current.User.Identity.Name;
                using (IPlantBreedingRepo repo = new PlantBreedingRepo())
                {
                    if (value.Id > 0)
                    {
                        UserPreference prefence = new UserPreference()
                                                    {
                                                        UserId = user,
                                                        GenusId = value.Id
                                                    };
                        repo.SaveUserPreference(prefence);
                    }
                 
                }
            }
        }

        public static int? GetGenusId()
        {
            Genus genus = Genus;
            int? val = null;
            if(genus != null)
            {
                val = genus.Id;
            }

            return val;
        }

        public static string RedirectUrl
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Session[redirectUrlKey] != null)
                {
                    result = HttpContext.Current.Session[redirectUrlKey] as string;
                }
                return result;
            }
            set
            {
                HttpContext.Current.Session[redirectUrlKey] = value;
            }
        }
        public static bool? NewSession
        {
            get
            {
                bool? result = null;
                if (HttpContext.Current.Session[newSessionKey] != null)
                {
                    result = HttpContext.Current.Session[newSessionKey] as bool?;
                }

                return result;
            }
            set
            {
                HttpContext.Current.Session[newSessionKey] = value;
            }
        }



        public static int? SessionPageSize
        {
            get
            {
                int? result = null;
                if (HttpContext.Current.Session[sessionPageSizeKey] != null)
                {
                    result = (int)HttpContext.Current.Session[sessionPageSizeKey];
                }

                return result;
            }
            set
            {
                HttpContext.Current.Session[sessionPageSizeKey] = value;
            }
        }

      

    }
}