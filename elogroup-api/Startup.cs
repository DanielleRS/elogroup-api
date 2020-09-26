using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repositories;
using Services.LeadServices;
using Services.UserServices;
using Utils.Utils.ClientsUrls;

namespace elogroup_api 
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
            services.AddControllers();
            services.AddTransient<IRegisterUserService, RegisterUserService>();
            services.AddTransient<IRegisterLeadService, RegisterLeadService>();
            services.AddTransient<IListLeadByCustomerService, ListLeadByCustomerService>();
            services.AddTransient<IListAllLeadsService, ListAllLeadsService>();

            services.AddSingleton<IUserRepository>(s => new UserRepository("Data Source=DESKTOP-P7UELKU;Initial Catalog=elogroup;Integrated Security=True"));
            services.AddSingleton<ILeadRepository>(s => new LeadRepository("Data Source=DESKTOP-P7UELKU;Initial Catalog=elogroup;Integrated Security=True"));
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => 
                {
                    builder.WithOrigins("*").AllowAnyHeader().WithMethods("POST", "PUT", "DELETE", "GET", "PATCH");
                });
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

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
