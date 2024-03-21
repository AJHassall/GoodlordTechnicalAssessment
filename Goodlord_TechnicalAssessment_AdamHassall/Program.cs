
using Goodlord_TechnicalAssessment_AdamHassall.Services;
using Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors;

namespace Goodlord_TechnicalAssessment_AdamHassall
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<AffordabilityCheckService>();
            builder.Services.AddTransient<ICSVProcessorFactory, CSVProcessorFactory>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.MapControllers();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
