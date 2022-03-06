using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhoneDirectory.Application.Interfaces;
using PhoneDirectory.Application.MappingProfiles;
using PhoneDirectory.Application.Services;
using PhoneDirectory.Infrastructure.Database;

namespace PhoneDirectory.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PhoneDirectory"));
            });

            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
	            c.SwaggerDoc("v1", new OpenApiInfo {Title = "PhoneDirectory.Api", Version = "v1"});
	            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "PhoneDirectory.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}