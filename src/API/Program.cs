using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<IQuizRepository, QuizRepository>(
    _ => new QuizRepository(connectionString)
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/quizzes", (IQuizRepository repository) =>
{
    try
    {
        return Results.Ok(repository.GetAllQuizzes());
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
    }
});

app.Run();

