using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed.Seeders
{
    public class UserSeeder : IDataSeeder
    {
        public int Priority => 1;

        public async Task<bool> HasDataAsync(OtavaraDbContext dbContext)
        {
            return await dbContext.Users.AnyAsync();
        }

        public async Task SeedAsync(OtavaraDbContext dbContext)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.Parse("8d2cd223-6b6e-4c7a-8ba0-2a18d0b6849b"),
                    TelegramId = 123456789,
                    FirstName = "Петро",
                    LastName = "Порошенко",
                    Username = "petro_poroshenko",
                    PhotoUrl = "https://example.com/photos/avatar1.jpg"
                },
                new User
                {
                    Id = Guid.Parse("c5be0f28-92f3-4f9c-b2d3-3f27de01da71"),
                    TelegramId = 987654321,
                    FirstName = "Марія",
                    LastName = "Коваленко",
                    Username = "maria_kovalenko",
                    PhotoUrl = "https://example.com/photos/avatar2.jpg"
                },
                new User
                {
                    Id = Guid.Parse("e1f0ccb2-4c92-48d8-9b6c-a7d0c9417429"),
                    TelegramId = 555555555,
                    FirstName = "Олександр",
                    LastName = "Сидоренко",
                    Username = "alex_sydorenko",
                    PhotoUrl = "https://example.com/photos/avatar3.jpg"
                },
                new User
                {
                    Id = Guid.Parse("d3c8a794-55d7-4e40-a1c3-8f4b41f697f0"),
                    TelegramId = 111222333,
                    FirstName = "Admin",
                    LastName = "User",
                    Username = "admin_user",
                    PhotoUrl = "https://example.com/photos/admin.jpg"
                },
                new User
                {
                    Id = Guid.Parse("f6b2350b-bc67-4025-bcf6-e5b060fa3cbe"),
                    TelegramId = 222333444,
                    FirstName = "Катерина",
                    LastName = "Дмитренко",
                    Username = "kateryna_dmytrenko",
                    PhotoUrl = "https://example.com/photos/avatar4.jpg"
                },
                new User
                {
                    Id = Guid.Parse("2b922b07-01c0-47a0-8ba5-88c76d2f0b69"),
                    TelegramId = 333444555,
                    FirstName = "Сергій",
                    LastName = "Бойко",
                    Username = "serhiy_boyko",
                    PhotoUrl = "https://example.com/photos/avatar5.jpg"
                },
                new User
                {
                    Id = Guid.Parse("4d8c64c4-4f61-4563-bc24-1f84c6dbb5ae"),
                    TelegramId = 444555666,
                    FirstName = "Тетяна",
                    LastName = "Мельник",
                    Username = "tetyana_melnyk",
                    PhotoUrl = "https://example.com/photos/avatar6.jpg"
                },
                new User
                {
                    Id = Guid.Parse("e8d62f5e-e7b5-4862-ae38-72d413f5225c"),
                    TelegramId = 555666777,
                    FirstName = "Максим",
                    LastName = "Гнатюк",
                    Username = "maxym_hnatyuk",
                    PhotoUrl = "https://example.com/photos/avatar7.jpg"
                },
                new User
                {
                    Id = Guid.Parse("af39b8a1-5b39-43a7-a6d1-b31a07cae0b3"),
                    TelegramId = 666777888,
                    FirstName = "Олена",
                    LastName = "Козак",
                    Username = "olena_kozak",
                    PhotoUrl = "https://example.com/photos/avatar8.jpg"
                },
                new User
                {
                    Id = Guid.Parse("f4bcdd60-c0f2-4421-b060-c303ed26a70b"),
                    TelegramId = 777888999,
                    FirstName = "Ярослав",
                    LastName = "Тимчук",
                    Username = "yaroslav_tymchuk",
                    PhotoUrl = "https://example.com/photos/avatar9.jpg"
                }
            };

            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }
    }
}
