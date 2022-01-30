using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Repository;
using WebApi.Repository.IRepository;
using AutoMapper;
using WebApi.Mapper;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    /// <summary>
    /// The StartUp Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The startup constructor
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The configuration interface declaration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(VendorMapping));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("VendorMachineOpenApiSpecProduct",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Vendor API Product",
                        Version = "1",
                        Description="Vending Machine API Product",
                        Contact= new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email="anijageorge@gmail.com",
                            Name="Anija George",
                            Url=new Uri("https://github.com/tvm-anija")
                        },
                        License= new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name="Api License",
                            Url=new Uri("https://github.com/tvm-anija")
                        }
                    });
                options.SwaggerDoc("VendorMachineOpenApiSpecUser",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Vendor API User",
                        Version = "1",
                        Description = "Vending Machine API User",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "anijageorge@gmail.com",
                            Name = "Anija George",
                            Url = new Uri("https://github.com/tvm-anija")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "Api License",
                            Url = new Uri("https://github.com/tvm-anija")
                        }
                    });
                options.SwaggerDoc("VendorMachineOpenApiSpecReset",
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Vendor API Reset",
                    Version = "1",
                    Description = "Vending Machine API Reset",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "anijageorge@gmail.com",
                        Name = "Anija George",
                        Url = new Uri("https://github.com/tvm-anija")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "Api License",
                        Url = new Uri("https://github.com/tvm-anija")
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n "+
                    "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n"+
                    "Example:\"Bearer 123456abcdefg\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme = "oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddControllers();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder object</param>
        /// <param name="env">The environment variable</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/Swagger/VendorMachineOpenApiSpecProduct/swagger.json", "Vendor API Product");
                options.SwaggerEndpoint("/Swagger/VendorMachineOpenApiSpecUser/swagger.json", "Vendor API User");
                options.SwaggerEndpoint("/Swagger/VendorMachineOpenApiSpecReset/swagger.json", "Vendor API Reset");
                options.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
