using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web
{
    public static class SessionHelper
    {
        public static object HttpSession { get; private set; }

        public static void UpdateSession(string sessionKey, object sessionObj)
        {
            //HttpSession[sessionKey] = sessionObj;
        }
    }
}