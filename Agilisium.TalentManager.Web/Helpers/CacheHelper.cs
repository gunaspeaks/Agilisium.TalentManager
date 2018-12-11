using System.Web;

namespace Agilisium.TalentManager.Web.Helpers
{
    public class CacheHelper
    {
        public static void AddOrUpdateItem(string key, object item, HttpContextBase context)
        {
            context.Cache.Remove(key);
            context.Cache.Insert(key, item);
        }

        public static  object GetItem(string key, HttpContextBase context)
        {
            return context.Cache[key];
        }

        public static bool IsCached(string key, HttpContextBase context)
        {
            return !(context.Cache[key] == null);
        }
    }
}