var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueDev", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// VARIABLE
var app = builder.Build();

app.UseRouting();

app.UseCors("AllowVueDev");


app.MapControllers();
app.Run();
