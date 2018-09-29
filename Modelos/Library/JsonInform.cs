using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Modelos.Library
{
    public class JsonInform
    {
        public static String PageCommentsToJson(Modelos.PageComments obj)
        {
            DataContractJsonSerializer ser;
            MemoryStream ms = new MemoryStream();
            ser = new DataContractJsonSerializer(typeof(Modelos.PageComments));
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}
