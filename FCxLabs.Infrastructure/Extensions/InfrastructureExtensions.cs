using FCxLabs.Core.Contracts.Repositories;
using FCxLabs.Core.Contracts.Services;
using FCxLabs.Core.Entities;
using FCxLabs.Infrastructure.DbContext;
using FCxLabs.Infrastructure.Repositories;
using FCxLabs.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCxLabs.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        //Config
		
        //var emailConfig = config.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
        //services.AddSingleton(emailConfig);
		
        services.AddLogging();
		
        //DB
		
        services.AddDbContext<UserDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
		
        //Repositories
		
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
		
        //Services
		
        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        services.AddScoped<IUserService, UserService>();
        
        //Identity
        services.AddIdentity<User, IdentityRole>(options => 
            {
                options.User.RequireUniqueEmail = true;  
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
		
        services.Configure<IdentityOptions>(
            options => options.SignIn.RequireConfirmedEmail = true
        );
		
        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });
		
        return services;
    }
}