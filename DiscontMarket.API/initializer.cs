using DiscontMarket.DAL.Repository.Implementations;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.DAL.SqlServer.Context;
using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Services.Services.Implementations;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DiscontMarket.API
{
    public static class initializer
    {
        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(UserManager<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IAttributeRepository), typeof(AttributeRepository));
            services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));



            return services;
        }

        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserStore<User>, UserStore<User, IdentityRole<uint>, AppDbContext, uint>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(IAttributeService), typeof(AttributeService));
            services.AddScoped(typeof(IFilterService), typeof(FilterService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IBrandService), typeof(BrandService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            return services;
        }


        public static IServiceCollection InitializeIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthManager<User>>(provider =>
            {
                var userManager = provider.GetRequiredService<UserManager<User>>();
                return new AuthManager<User>(userManager, configuration);
            });

            services.AddScoped(typeof(ITokenManager<User>), typeof(AuthManager<User>));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole<uint>>()
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<User>, UserStore<User, IdentityRole<uint>, AppDbContext, uint>>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }


        public static async Task InitializeRoles(this IServiceCollection services)
        {
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>();

            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRoles(roleManager);
        }


        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(RoleType)).Cast<RoleType>())
            {
                var roleName = role.ToString();

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public async static Task SeedAdmins(this IServiceCollection services)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
            const uint adminId = 0;
            const string adminName = "Admin";

            var user = await userManager.FindByNameAsync(adminName);
            if (user != null) return;

            var admin = new User()
            {
                Id = adminId,
                FirstName = "Admin",
                LastName = "Admin",
                MiddleName = "Admin",
                Email = "admin@admin.com",
                UserName = adminName,
                NormalizedUserName = "Admin".Normalize(),
                NormalizedEmail = "admin@admin.com".Normalize(),
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "P@ssw0rd!");


            await userManager.CreateAsync(admin);
            await userManager.AddToRoleAsync(admin, RoleType.Admin.ToString());
        }
    }
}
