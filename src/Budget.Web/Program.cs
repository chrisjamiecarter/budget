using Budget.Application.Installers;
using Budget.Infrastructure.Installers;
using Budget.Web.Installers;

namespace Budget.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddWeb();

        var config = builder.Configuration;

        var app = builder.Build();
        app.AddMiddleware();
        app.Run();
    }
}
