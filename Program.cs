
using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using NetDapperWebApi;
using NetDapperWebApi.Common.Interfaces;
using NetDapperWebApi.Services;
using WebApi.Context;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
          {
              options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                              }

                              );
          });
            builder.Services.AddWebServices(builder.Configuration).AddAuthentications(builder.Configuration);

            builder.Services.AddSwaggerExplorers(builder.Configuration);
            builder.Services.AddLogging(s=>{
                s.AddConsole();
                s.AddDebug();
               
            });
            //--------------------------------------------
            var app = builder.Build();
            app.UseExceptionHandler(config =>
            {
                //tối ưu hơn
                config.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    var message = exception.Message;
                    var statusCode = 500;
                    if (exception is SqlException)
                    {
                        statusCode = 400;
                    }
                    var response = new { message, statusCode };
                    var result = JsonSerializer.Serialize(response);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsync(result);
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentications();
            app.MapControllers();
            app.UseCors(MyAllowSpecificOrigins);
            app.Run();
        }
    }
}
