using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MissionApp.Data;
using MissionApp.Service;
using MissionApp.Repository;
using AutoMapper;
using MissionApp.Service.Models;

namespace MissionApp.API
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

            services.AddDbContext<MissionContext>(option => option.UseSqlServer(Configuration.GetConnectionString("MissionConnectionString")));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddTransient<IMissionService, MissionService>();
            services.AddTransient<IMissionRepository, MissionRepository>();
            services.AddTransient<IAppSettingRepository, AppSettingRepository>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MissionProfile());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mission API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
