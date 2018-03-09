#if NETSTANDARD2_0
using dng.Syndication.Generators;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace dng.Syndication.Formatters
{
    public class AtomOutputFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return (context.ObjectType == typeof(Feed) || context.ObjectType.IsSubclassOf(typeof(Feed))) && context.ContentType.Equals(MediaTypes.AtomMediaType);
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            using (var atomGenerator = new AtomGenerator(context.Object as Feed))
            {
                context.HttpContext.Response.ContentType = MediaTypes.AtomMediaType;
                await context.HttpContext.Response.WriteAsync(atomGenerator.Process());
            }
        }
    }
}
#endif