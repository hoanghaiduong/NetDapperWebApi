
using NetDapperWebApi;


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
            builder.Services.AddLogging(s =>
            {
                s.AddConsole();
                s.AddDebug();

            });
            //--------------------------------------------
            var app = builder.Build();
            app.UseCustomExtensions();
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
