using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Boutique.Models
{
    public static class SessionExtentions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public static T GetJSon<T>(this ISession session, string key)
        {
            var data = session.GetString(key);

            return data == null ?
                default(T) : JsonConvert.DeserializeObject<T>(data);
        }
    }
}
