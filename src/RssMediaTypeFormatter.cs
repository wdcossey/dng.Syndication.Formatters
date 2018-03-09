#if NET452
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using dng.Syndication.Generators;

namespace dng.Syndication.Formatters
{
    public class RssMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        public RssMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypes.RssMediaType));
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
                using (var rss20Generator = new Rss20Generator(feed))
                {
                    streamWriter.WriteLine(rss20Generator.Process());
                }
            }
        }
    }
}
#endif