using System;

namespace MeditSolution
{
    public class Constants
    {


#if UAT
        public static string RestUrl = "http://apiuat.matsiya.net/medit_solution/v1/";

        public static string FileUrl = "http://apiuat.matsiya.net/medit_solution/";

        public static string ContactRedirectUrl = "http://medit-solutions.com/contact";

        public static string CGURedirectUrl_FR = "http://medit-solutions.com/cgu";

        public static string CGURedirectUrl_EN = "http://medit-solutions.com/en/cgu/";

        public static string OneSignalID = "78268c76-54e1-443a-9e92-527ff34a32fb";

#else
        public static string RestUrl = "https://api.medit-solutions.com/v1/";

        public static string FileUrl = "https://api.medit-solutions.com/";

        public static string ContactRedirectUrl = "http://medit-solutions.com/contact";

        public static string CGURedirectUrl_FR = "http://medit-solutions.com/cgu";

        public static string CGURedirectUrl_EN = "http://medit-solutions.com/en/cgu/";

        public static string OneSignalID = "78268c76-54e1-443a-9e92-527ff34a32fb";
#endif

    }
}
