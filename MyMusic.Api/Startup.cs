using System;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyMusic.Core;
using MyMusic.Core.Services;
using MyMusic.Data;
using MyMusic.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMusic.Api
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
      services.AddDbContext<MyMusicDbContext>(options =>
      {
        options.UseNpgsql(Configuration.GetConnectionString("Default"), x =>
        {
          x.MigrationsAssembly("MyMusic.Data");
        });
      });

      services.AddControllers().AddFluentValidation(config =>
      {
        config.RegisterValidatorsFromAssemblyContaining<Startup>();
      });
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddTransient<IMusicService, MusicService>();
      services.AddTransient<IArtistService, ArtistService>();

      // auto mapper injection
      services.AddAutoMapper(typeof(Startup));

      // swagger options
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Music", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      // swagger config
      app.UseSwagger();
      app.UseSwaggerUI(config =>
      {
        config.RoutePrefix = "docs";
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
      });
    }
  }
}
