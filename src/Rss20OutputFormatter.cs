#if NETSTANDARD2_0
using System.Threading.Tasks;
using dng.Syndication.Generators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace dng.Syndication.Formatters
{
    public class Rss20OutputFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return (context.ObjectType == typeof(Feed) || context.ObjectType.IsSubclassOf(typeof(Feed))) && context.ContentType.Equals(MediaTypes.RssMediaType);
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            using (var atomGenerator = new Rss20Generator(context.Object as Feed))
            {
                context.HttpContext.Response.ContentType = MediaTypes.RssMediaType;
                await context.HttpContext.Response.WriteAsync(atomGenerator.Process());
            }
        }

    }
}
#endif