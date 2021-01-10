using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Billing.WebApi.Repositories;
using Billing.WebApi.Repositories.Models;
using Billing.WebApi.Models.Converter;

namespace Billing.WebApi
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
            services.AddDbContext<BillingContext>(opt =>
                                   opt.UseNpgsql(Configuration.GetConnectionString("BillingContextConnection")));
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IOrderConverter, OrderConverter>();
            services.AddScoped<IGoodsRepository, GoodsRepository>();
            services.AddScoped<IGoodConverter, GoodConverter>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddControllers();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
