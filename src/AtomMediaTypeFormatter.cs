#if NET452
using dng.Syndication.Generators;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace dng.Syndication.Formatters
{
    public class AtomMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public AtomMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypes.AtomMediaType));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Feed) || type.IsSubclassOf(typeof(Feed));
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            if (!(value is Feed feed))
            {
                return;
            }

            using (var streamWriter = new StreamWriter(writeStream))
            {
                using (var atomGenerator = new AtomGenerator(feed))
                {
                    streamWriter.WriteLine(atomGenerator.Process());
                }
            }
        }
    }
}
#endif