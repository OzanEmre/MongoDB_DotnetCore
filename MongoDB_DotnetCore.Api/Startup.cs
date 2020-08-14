using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB_DotnetCore.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace MongoDB_DotnetCore.Api
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
            string mongoConnStr = this.Configuration.GetConnectionString("MongoDBConnectionString");
            services.AddTransient(s => new MongoRepository(mongoConnStr, "testdb","Tasks"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "MongoDB API",
                    Version = "v1",
                    Description = "MongoDB API tutorial using MongoDB",
                });
            });
            //services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5001").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My MongoDB Api V1");
            });

            // app.UseCors(
            //     options => options.WithOrigins("http://localhost:5001/").AllowAnyMethod()
            // );
            app.UseCors("ApiCorsPolicy");
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
