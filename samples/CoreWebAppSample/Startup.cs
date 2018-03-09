using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dng.Syndication;
using dng.Syndication.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CoreWebAppSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new AtomOutputFormatter());
                options.OutputFormatters.Add(new Rss20OutputFormatter());

                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("atom", MediaTypeHeaderValue.Parse(MediaTypes.AtomMediaType));

                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("rss", MediaTypeHeaderValue.Parse(MediaTypes.RssMediaType));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
