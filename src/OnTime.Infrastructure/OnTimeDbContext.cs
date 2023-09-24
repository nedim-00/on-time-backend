using System.Reflection;

namespace OnTime.Infrastructure;

public class OnTimeDbContext : DbContext
{
    public OnTimeDbContext(DbContextOptions<OnTimeDbContext> context)
        : base(context)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<Table> Tables { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        ConfigureEntityTypes(modelbuilder);

        base.OnModelCreating(modelbuilder);
    }

    private static void ConfigureEntityTypes(ModelBuilder modelbuilder)
    {
        var typesToConfigure = Assembly.GetAssembly(typeof(OnTimeDbContext))?.GetTypes()
            .Where(type => type.GetInterfaces().Any(iface => iface.IsGenericType &&
                iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        if (typesToConfigure is not null)
        {
            foreach (var type in typesToConfigure)
            {
                dynamic configurationInstance = Activator.CreateInstance(type)!;
                modelbuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
