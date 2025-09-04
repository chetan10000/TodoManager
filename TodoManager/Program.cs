using Microsoft.EntityFrameworkCore;
using TodoManager.Data;
using FluentValidation;
using TodoManager.Models;
using TodoManager.Endpoints;
namespace TodoManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>
            (
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
            );
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddValidatorsFromAssemblyContaining<TodoValidator>();
            // below code allow any origin so we don't get cors error while testing with frontend
           // builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            builder.Services.AddEndpointsApiExplorer();  // Enables API endpoint discovery
            builder.Services.AddSwaggerGen();  // Enables Swagger generation

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                /*
                 we will create table if it's not there 
                this command is not safe for production better 
                in production we can use db.Database.Migrate();
                */
                db.Database.EnsureCreated();
                seedData(db);

            }
           // app.UseCors("CorsPolicy");
            app.MapGroup("/api").MapTodoEndpoints();
            app.Run();
        }
        public static void seedData(ApplicationDbContext dbcontext)
        {
            if (!dbcontext.Todos.Any())
            {
                dbcontext.AddRange
                (
                    new Todo { Title="wash dish" ,Description="can you wash dish in 10 minutes it's urgent ",Deadline=DateTime.Now},
                    new Todo { Title = "buy chocolate", Description = "can you buy chocolate on behalf of me  ", Deadline = DateTime.Now }
                );
                dbcontext.SaveChanges();
            }
        }
    }
}
