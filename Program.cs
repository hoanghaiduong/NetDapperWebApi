
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


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
            app.MapGet("/", async (IConfiguration configuration) =>
            {

                var cnnStr = configuration.GetConnectionString("DefaultConnection");
                using (var cnn = new SqlConnection(cnnStr))
                {
                    var sql = "Select id, name , description From Roles where Id=@id";
                    var paramerters = new DynamicParameters();
                    paramerters.Add("@id", 1, DbType.Int32);
                    var result =await cnn.QueryAsync(sql,paramerters,null,null,CommandType.Text);
                    return Results.Ok(new
                      {
                        data=result
                      });
                }
              
            });
            app.Run();
        }
    }
}
