using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD
{
    public static class Extensions
    {
        public static EventData AsJson(this object value)
        {
            if (value == null) throw new ArgumentNullException("value");

            var json = JsonConvert.SerializeObject(value);
            var data = Encoding.UTF8.GetBytes(json);
            var eventName = value.GetType().Name;

            return new EventData(Guid.NewGuid(), eventName, true, data, new byte[] { });
        }

        // This method Convert Json to C# object 
        public static T ParseJson<T>(this RecordedEvent data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var value = Encoding.UTF8.GetString(data.Data);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
