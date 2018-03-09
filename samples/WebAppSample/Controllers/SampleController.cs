using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using dng.Syndication;

namespace WebAppSample.Controllers
{
    [RoutePrefix("feed/sample")]
    public class SampleController : ApiController
    {
        private Feed GenerateFeed()
        {
            return new Feed
            {
                Title = FeedContent.Text("dotnetgeek feed"),
                Author = new Author
                {
                    Name = "Daniel",
                    Email = "email@email.em"
                },
                Copyright = "2016 @ www.dotnetgeek.com",
                Description = FeedContent.Text("Dotnet relevant thinks"),
                Generator = "dng.Syndication",
                Language = CultureInfo.GetCultureInfo("de"),
                UpdatedDate = new DateTime(2016, 08, 16),
                Link = new Uri("http://www.dotnetgeek.de/rss"),
                FeedEntries = new List<IFeedEntry>
                {
                    new FeedEntry
                    {
                        Title =  FeedContent.Plain("First Entry"),
                        Content =  FeedContent.Plain("Content"),
                        Link = new Uri("http://www.dotnetgeek.com/first-entry"),
                        Summary =  FeedContent.Plain("summary"),
                        PublishDate = new DateTime(2016, 08, 16),
                        Updated = new DateTime(2016, 08, 16),
                    }
                }
            };
        }

        [HttpGet, ActionName("atom")]
        public HttpResponseMessage GetAtom()
        {
            return Request.CreateResponse(HttpStatusCode.OK, GenerateFeed(), MediaTypeHeaderValue.Parse(MediaTypes.AtomMediaType));
        }

        [HttpGet, ActionName("rss")]
        public HttpResponseMessage GetRss()
        {
            return Request.CreateResponse(HttpStatusCode.OK, GenerateFeed(), MediaTypeHeaderValue.Parse(MediaTypes.RssMediaType));
        }
    }
}
