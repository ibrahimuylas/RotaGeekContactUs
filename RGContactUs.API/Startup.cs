using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using RGContactUs.Data.EF;
using RGContactUs.Core.Service;
using RGContactUs.Service;
using RGContactUs.Core.Repository;
using RGContactUs.Repository;
using AutoMapper;

namespace RGContactUs.API
{
    public class Startup
    {

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Rota Geek Contact Us API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "https://localhost:44389")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowAnyOrigin()
                                        .AllowCredentials();
                });
            });

            services
                 .AddEntityFrameworkInMemoryDatabase()
                 .AddDbContext<ContactUsContext>((serviceProvider, options) =>
                     options.UseInMemoryDatabase("RGContactUs")
                            .UseInternalServiceProvider(serviceProvider));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContactUsProfile());
                mc.ValidateInlineMaps = false;
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            //services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
            //services.AddSingleton<IAPISettings, APISettings>();
            //services.AddSingleton<IPBSClient, PBSClient>();

            services.AddTransient<IContactUsService, ContactUsService>();
            services.AddTransient<IContactUsRepository, ContactUsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rota Geek Contact Us API V1");
            });

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
