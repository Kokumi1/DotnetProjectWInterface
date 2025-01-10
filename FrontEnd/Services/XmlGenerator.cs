using System.Xml.Serialization;
using FrontEnd.Datas;
using FrontEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Services
{
    class XmlGenerator
    {
        public static void SerializeToXml<T>(List<T> operationList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string filePath = "../../../../Operation.xml";

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, operationList);
            }
        }
    }
}
