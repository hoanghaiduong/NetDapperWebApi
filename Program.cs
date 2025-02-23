
using NetDapperWebApi;


namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            
            builder.Services.AddWebServices(builder.Configuration).AddAuthentications(builder.Configuration);

            builder.Services.AddSwaggerExplorers(builder.Configuration);
    
            //--------------------------------------------
            var app = builder.Build();
            app.UseCustomExtensions();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentications();
            app.MapControllers();
        
            app.Run();
        }
    }
}
