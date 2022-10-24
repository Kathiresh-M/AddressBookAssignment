using Contract;
using Contract.IHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using Services.Helper;
using System.Text;

namespace AddressBook
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
            services.AddControllers();

            //Add Database
            services.AddDbContext<BookRepository>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection"));
            });

            //Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Injection
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IEmailRepo, EmailRepo>();
            services.AddScoped<IPhoneRepo, PhoneRepo>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IAddressBookRepo, AddressBookRepo>();
            services.AddScoped<IAssetRepo, AssetRepo>();
            services.AddScoped<IRefSetRepo, RefSetRepo>();
            services.AddScoped<IRefTermRepo, RefTermRepo>();
            services.AddScoped<IRefSetTermRepo, RefSetTermRepo>();

            services.AddSingleton<IPassword, Password>();
            services.AddSingleton<IJWTService, JWTService>();

            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IRefSetService, RefSetService>();
            services.AddScoped<IRefTermService, RefTermService>();
            services.AddScoped<IAddressBookService, AddressBookService>();

            //JWT configuration
            var appSettingsSection = Configuration.GetSection("JwtConfig");
            services.Configure<AppSecret>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSecret>();
            var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt => {
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddSwaggerGen();
            //Add Swagger 
            /*services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AddressBookAPI",
                    Description = "An address book API",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });*/
        }

        /*public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
        }*/
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //exception handler
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async c =>
                    {
                        c.Response.StatusCode = 500;
                        await c.Response.WriteAsync("Something went wrong, please try again later!");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI();
            /*app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });*/
        }
    }
}
