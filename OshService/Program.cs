namespace OshService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/api/test", () => "Hello World!");

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}
