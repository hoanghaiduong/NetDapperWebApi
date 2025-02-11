
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
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

            // builder.Services.AddScoped<DapperDbContext>();
            builder.Services.AddScoped<IDbConnection>(cnn => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
