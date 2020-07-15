using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace TweetBook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Tweetbook API", Version = "v1" });
            });

            services.AddApiVersioning();

            //services.AddApiVersioning(cfg=> {
            //    cfg.DefaultApiVersion = new ApiVersion(1,1);
            //    cfg.AssumeDefaultVersionWhenUnspecified = true;
            //    cfg.ReportApiVersions = true;
            //    cfg.ApiVersionReader =new QueryStringApiVersionReader("1.0");
            //});
        }
    }
}
