using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoListCQRS.Application.Mapping;
using ToDoListCQRS.Application.Mapping.PagedResponce;
using ToDoListCQRS.Application.Queries;
using ToDoListCQRS.Domain.Interfaces.Repositories.IToDoRepository;
using ToDoListCQRS.Persistence;
using ToDoListCQRS.Persistence.Mapping;
using ToDoListCQRS.Persistence.Repositories;

namespace ToDoListCQRS.WebApi.Extensions;

public static class DependencyInjection
{
    public  static IServiceCollection ConfigureSevices(this IServiceCollection services)
    {
        var assemblies = typeof(GetToDoListQueryHandler).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        services.AddAutoMapper(cfg => { }, 
            typeof(PagedResponceProfile).Assembly,  
            typeof(ToDoItemProfile).Assembly);

        services.AddScoped<IToDoRepository, ToDoRepository>();

        return services;
    }

    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DbConnectionString"));
        });
    }
}
