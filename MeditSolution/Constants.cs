using System;

namespace MeditSolution
{
    public class Constants
    {


#if UAT

        public static string RestUrl = "http://apiuat.matsiya.net/medit_solution/v1/";

        public static string ContactRedirectUrl = "http://medit-solutions.com/contact";

        public static string CGURedirectUrl = "http://medit-solutions.com/cgu";


#else  

        public static string RestUrl = "https://api.medit-solutions.com/v1/";

        public static string ContactRedirectUrl = "http://medit-solutions.com/contact";

        public static string CGURedirectUrl = "http://medit-solutions.com/cgu";

#endif


    }
}
