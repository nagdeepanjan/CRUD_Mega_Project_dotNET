var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
