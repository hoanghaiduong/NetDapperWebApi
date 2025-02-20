
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetDapperWebApi.Common.Filters;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Models;
using NetDapperWebApi.Services;

namespace NetDapperWebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddScoped<IDbConnection>(cnn => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService,UserService>();
            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddSwaggerExplorers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen((configure) =>
              {
                  configure.SchemaFilter<EnumSchemaFilter>();
                  configure.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                  {
                      Name = "JWT Authentication",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.Http,
                      Scheme = "Bearer",
                      Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer ey...')",
                  });
                  configure.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                {
                     new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                  });
              });

            return services;
        }
        public static IServiceCollection AddAuthentications(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidAudience = configuration["AppSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:AccessTokenSecret"]!))
                };
            }).AddJwtBearer("RefreshToken", y =>
            {
                y.SaveToken = false;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidAudience = configuration["AppSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:RefreshTokenSecret"]!))
                };
            });
            services.AddAuthorization();
            return services;
        }
        public static WebApplication UseAuthentications(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}