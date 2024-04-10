using Core.Entities;
using Infrastructure.Constants;

namespace Infrastructure.Mock;

public static class MockData
{
    public static List<Role> Roles { get; } = new List<Role>
    {
        new Role{ Id = 1, Name = RoleType.Admin },
        new Role{ Id = 2, Name = RoleType.Customer },
        new Role{ Id = 3, Name = RoleType.Cashier },
        new Role{ Id = 4, Name = RoleType.Seller }
    };

    public static List<User> Users { get; } = new List<User>
    {
        new User { Id = 1, Name = "Administrador", Lastname = "", Email = "admin@random.com", Password = "123", Username = "admin", Roles = new List<Role> { Roles[0] } },
        new User { Id = 2, Name = "Mike", Lastname = "Tyson", Email = "mike.tyson@random.com", Password = "1234", Username = "mike.tyson", Roles = new List<Role> { Roles[1], Roles[2] } },
        new User { Id = 3, Name = "Roberto", Lastname = "Durán", Email = "roberto.duran@random.com", Password = "12345", Username = "roberto.duran", Roles = new List<Role> { Roles[2], Roles[3] } }
    };
}
