using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

namespace EmployeeWebAPI.Controllers
{
    public class CustomXmlFormatter : XmlSerializerInputFormatter
    {
        public CustomXmlFormatter(MvcOptions options) : base(options)
        {
        }

        protected override XmlReader CreateXmlReader(Stream readStream, Encoding encoding)
        {
            var reader = new StreamReader(readStream, encoding);
            var writer = new StreamWriter(new MemoryStream());
            // This pattern will ignore already encoded charecters
            var re = new Regex("&(?!.{3};)");
            var startPosition = readStream.Position;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var encoded = re.Replace(line, "&amp;");
                writer.WriteLine(encoded);
            }

            writer.Flush();
            writer.BaseStream.Position = startPosition;
            var xmlReader = XmlReader.Create(writer.BaseStream);

            return xmlReader;
        }
    }
}
