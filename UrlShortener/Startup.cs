namespace UrlShortener
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using DataAccess;
    using DataAccess.DbContext;
    using Dto;
    using Entities;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().
                AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            var connectionString = @"Server=localhost\SQLEXPRESS;Database=UrlShortenerDB;Trusted_Connection=True;";
            services.AddDbContext<UrlShortenerContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UrlShortenerContext urlShortenerContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ShortenedUrlEntity, ShortenedUrlDto>();
                cfg.CreateMap<ShortenedUrlCreateDto, ShortenedUrlEntity>();
            });

            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
