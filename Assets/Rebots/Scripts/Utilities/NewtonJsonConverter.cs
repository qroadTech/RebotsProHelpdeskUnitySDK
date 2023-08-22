using HelpDesk.Sdk.Library.Converter;
using Newtonsoft.Json;

namespace Rebots.HelpDesk
{
    public class NewtonJsonConverter : IJsonConverter
    {
        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialize<T>(T val)
        {
            return JsonConvert.SerializeObject(val);
        }
    }
}
