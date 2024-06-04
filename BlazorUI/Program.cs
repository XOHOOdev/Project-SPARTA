using Helium.BlazorUI.Areas.Identity;
using Helium.BlazorUI.Authorization;
using Helium.BlazorUI.Data;
using Helium.BlazorUI.Data.ConfigurationData;
using Helium.BlazorUI.Data.DiscordData;
using Helium.BlazorUI.Data.UserManagementData;
using Helium.BlazorUI.Entities;
using Helium.BlazorUI.Permissions;
using Helium.BlazorUI.Services;
using Helium.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI
{
    class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddConfiguration(ConfigLoader.Load());

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext<IdentityUser, ApplicationRole, string>>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddRoles<ApplicationRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            builder.Services.AddLocalization();

            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            builder.Services.AddSingleton<IPermissionService, PermissionService>();
            builder.Services.AddScoped<ConfigurationService>();
            builder.Services.AddScoped<UserManagementService>();
            builder.Services.AddScoped<DiscordDataService>();
            builder.Services.AddHttpClient();

            builder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = builder.Build();
            using (var serviceScope = app.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                ApplyMigrations(serviceProvider);
                CreatePermissions(serviceProvider).Wait();
                CreateRoles(serviceProvider).Wait();
                CreateDefaultUser(serviceProvider).Wait();
                LoadDefaultConfig();

                UserExtensions.Configure(serviceScope.ServiceProvider.GetService<IAuthorizationHandler>());
            }

            var supportedCultures = new[] { "en-US", "en-GB", "de-DE" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();

        }

        private static async Task CreatePermissions(IServiceProvider serviceProvider)
        {
            ApplicationDbContext<IdentityUser, ApplicationRole, string> dbContext = serviceProvider.GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();

            var permissions = GetPermissions(typeof(Permissions.Permissions).GetNestedTypes());

            foreach (Permission permission in permissions)
            {
                if (!await dbContext.US_Permissions.AnyAsync(x => x.Name == permission.Name))
                {
                    permission.Id = Guid.NewGuid().ToString();
                    await dbContext.US_Permissions.AddAsync(permission);
                }
            }
            await dbContext.SaveChangesAsync();
        }

        private static List<Permission> GetPermissions(IEnumerable<Type> classPermissions)
        {
            var permissions = new List<Permission>();
            foreach (var classPermission in classPermissions)
            {
                permissions.AddRange(classPermission.GetFields().Select(x => new Permission { Name = (x.GetValue(null) as string) ?? "", NormalizedName = ((x.GetValue(null) as string) ?? "").ToUpper() }));
                permissions.AddRange(GetPermissions(classPermission.GetNestedTypes()));
            }
            return permissions;
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            if (roleManager == null) return;

            foreach (var role in Enum.GetValues(typeof(DefaultRoles)))
            {
                var roleName = role.ToString() ?? "";

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            var adminRole = roleManager.FindByNameAsync("Admin").Result;
            if (adminRole == null) return;

            ApplicationDbContext<IdentityUser, ApplicationRole, string> dbContext = serviceProvider.GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();
            foreach (var permission in dbContext.US_Permissions)
            {
                adminRole.Permissions.Add(permission);
            }
            dbContext.SaveChanges();
        }

        private static async Task CreateDefaultUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager == null) return;


            if (await userManager.FindByNameAsync("admin@helium.de") == null)
            {
                var user = new IdentityUser()
                {
                    Email = "admin@helium.de",
                    UserName = "admin@helium.de",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Test1.");
            }

            var adminUser = await userManager.FindByNameAsync("admin@helium.de");
            if (adminUser == null) return;

            foreach (var role in Enum.GetValues(typeof(DefaultRoles)))
            {
                await userManager.AddToRoleAsync(adminUser, role.ToString() ?? "");
            }
        }

        private static void ApplyMigrations(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();
            context.Database.Migrate();
        }

        private static void LoadDefaultConfig()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "default-config.json");
            using StreamReader r = new(path);
            string json = r.ReadToEnd();
            ConfigHelper.SetConfig(json);
        }
    }
}