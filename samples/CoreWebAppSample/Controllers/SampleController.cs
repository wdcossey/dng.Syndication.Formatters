using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using dng.Syndication;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAppSample.Controllers
{
    [Route("feed/[controller]")]
    public class SampleController : Controller
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

        [HttpGet("get.{format}"), FormatFilter]
        public IActionResult Get()
        {
            return Ok(GenerateFeed());
        }

        [HttpGet("atom")]
        public IActionResult GetAtom()
        {
            HttpContext.Response.ContentType = MediaTypes.AtomMediaType;
            return Ok(GenerateFeed());
        }

        [HttpGet("rss")]
        public IActionResult GetRss()
        {
            HttpContext.Response.ContentType = MediaTypes.RssMediaType;
            return Ok(GenerateFeed());
        }
    }
}
