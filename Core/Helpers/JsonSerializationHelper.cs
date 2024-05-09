using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Helpers
{
    public class JsonSerializationHelper
    {
        /// <summary>
        /// Deserializes the specified JSON string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object of the specified type.</returns>
        public static T DeserializeObject<T>(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Objects,

            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Serializes the specified object into a JSON string.
        /// </summary>
        /// <param name="objectModel">The object to serialize.</param>
        /// <returns>The JSON string representing the serialized object.</returns>
        public static string SerializeObject(object objectModel)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.SerializeObject(objectModel, Formatting.Indented, settings);
        }
    }
}
