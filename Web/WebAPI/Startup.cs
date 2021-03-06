﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BLL;
using BLL.ThirtyPart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.Filters;
using WebAPI.Middleware;

namespace WebAPI
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<StopWatch>();

            services.AddMvc(options =>
            {
                options.Filters.Add<ModelIsValidFilter>();
                options.Filters.Add<CustomExceptionFilterAttribute>();
            })
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
          

            services.AddCors(options =>
                 options.AddPolicy("AllowAnyOrigin",
                 builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials())
             );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "web 接口文档",
                    Version = "v1",
                    Description = "web 接口文档",
                    Contact = new Contact()
                    {
                        Name = "eleven yi",
                    },
                });

                //设置xml文件路径
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //var xmlPath = Path.Combine(basePath, "WebAPI.xml");
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "web api v1");
                //c.RoutePrefix = string.Empty;
            });
            app.UseMiddleware<TimeMiddleware>();
            // app.UseMiddleware<TimeMiddleware>(new StopWatch()); //也可以不把StopWatch添加到依赖注入容器中，而是在UserMiddleware方法中直接给出参数。
            app.UseMvc();
#if !DEBUG
            InitBLL.Start();
#endif
            InitBLL.Start();
            Task.Factory.StartNew(()=> {
                TaoBaoKeHelper.ImportAllTaobaoke();
                TaoBaoKeHelper.ImportTaobaoke();
            });
        }
    }
}
