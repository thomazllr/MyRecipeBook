using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Identity;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Identity;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;
using MyRecipeBook.Infrastructure.Security.Tokens.Access;
using System.Configuration;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        {

            services.AddRepositories();

            services.AddTokenHandlers(configuration);

            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

            services.AddScoped<ILoggedUser, LoggedUser>();

            services.AddDbContext(configuration);

            services.AddFluentMigrator(configuration);

        }

        private void AddRepositories()
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();

        }

        private void AddTokenHandlers(IConfiguration configuration)
        {
            services.AddScoped<IAccessTokenGenerator>(_ =>
            {
                var expirationTimeInMinutes = configuration.GetValue<uint>("Jwt:ExpirationTimeMinutes");
                var singinKey = configuration.GetValue<string>("Jwt:SinginKey");

                return new JwtTokenHandler(expirationTimeInMinutes, singinKey!);
            });

        }

        private void AddDbContext(IConfiguration configuration)
        {
            services.AddDbContext<MyRecipeBookDbContext>(config =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection")!;
                config.UseMySQL(connectionString);
            });
        }

        private void AddFluentMigrator(IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(config =>
            {
                config
                    .AddMySql5()
                    .WithGlobalConnectionString(_ =>
                    {
                        return configuration.GetConnectionString("DbConnection")!;
                    })
                    .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure"))
                    .For.All();
            });
        }


    }

}
