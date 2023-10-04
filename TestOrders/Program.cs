using Microsoft.EntityFrameworkCore;
using TestOrders.Middleware;
using TestOrders.ObjectModel.IRepository;
using TestOrders.ObjectModel.IService;
using TestOrders.Persistance;
using TestOrders.Persistance.Repository;
using TestOrders.Service;
using TestOrders.Service.Mapper;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();

InitializeDatabase(app);

ConfigureMiddleware(app);
ConfigureHttpPipeline(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
               .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information));

    services.AddAutoMapper(typeof(MappingProfile));
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IProductRepository, ProductRepository>();
}

static void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

static void ConfigureMiddleware(WebApplication app)
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

static void ConfigureHttpPipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}
