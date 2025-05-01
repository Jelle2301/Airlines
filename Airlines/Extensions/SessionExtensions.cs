using Newtonsoft.Json;

namespace Airlines.Extensions
{
    public static class SessionExtensions //static toevoegen!!!!
    {
        // Extension methods, as the name suggests, are additional methods.
        // Extension methods allow you to inject additional methods
        // without modifying, deriving or 
        // recompiling the original class, struct or interface.
        //!!!!

        //eerste parameter this en dan de klasse of interface waarop je uitbreiding wilt schrijven
        //je noemt die methode dieje toevoegt SetObject en je geeft een object mee
        public static void SetObject(this ISession session, string key, object? value)
        {

            session.SetString(key, JsonConvert.SerializeObject(value));
        }


        //tweede methode dieje toevoegt
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key); // Json-Object
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);//omdat je generiek hebt gemaakt kan je die DeserializeObject
                                                                                        // doen omdat anders zouje niet weten welk type hetgene is daje binnekrijgt
                                                                                        //}
        }
    }
}
