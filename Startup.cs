using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using System;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Proxies;
using System.IO;
using System.Threading.Tasks;

namespace ltl_cloudstorage
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
            services.AddCors(options => {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSigninKey")))
                };
            });

            services.AddControllers();

			services.Configure<IISServerOptions>(options =>
			{
				options.MaxRequestBodySize = long.MaxValue;
			});


			services.Configure<KestrelServerOptions>(options =>
			{
				options.Limits.MaxRequestBodySize = long.MaxValue; 
			});

			services.Configure<FormOptions>(options =>
			{
				options.MultipartBodyLengthLimit = long.MaxValue;
			});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ltl_pf", Version = "v1" });
            });

            services.AddDbContext<CSDbContext>(options => {
                options
				.UseLazyLoadingProxies()
				.UseMySQL(Configuration.GetConnectionString("Default"));
            });

            services.AddSingleton(new JwtService(Configuration.GetValue<string>("JwtSigninKey")));

            services.AddScoped<AuthService>();

            services.AddScoped<UserService>();

            services.AddScoped<StorageService>();
           
			CreateStorageDirectory();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CSDbContext context)
        {
			context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ltl_webdev v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowCors");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

		private void CreateStorageDirectory()
		{
			
			string contextStoragePath = Directory.GetCurrentDirectory() + "/Storage";

			if(!Directory.Exists(contextStoragePath))
				Directory.CreateDirectory(contextStoragePath);
		}

    }
}
