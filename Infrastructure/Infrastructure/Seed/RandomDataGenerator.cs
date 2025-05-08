namespace Infrastructure.Seed;

public static class RandomDataGenerator
{
    private static readonly Random _random = new();

    public static string GetRandomItem(string[] items)
    {
        return items[_random.Next(0, items.Length)];
    }

    public static int GetRandomInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static double GetRandomDouble(double min, double max)
    {
        return min + _random.NextDouble() * (max - min);
    }

    public static DateTime GetRandomDate(DateTime from, DateTime to)
    {
        var range = (to - from).Days;
        return from.AddDays(_random.Next(range));
    }

    public static DateOnly GetRandomDateOnly(DateOnly from, DateOnly to)
    {
        var range = to.DayNumber - from.DayNumber;
        return DateOnly.FromDayNumber(from.DayNumber + _random.Next(range));
    }

    public static string GenerateFirstName()
    {
        string[] names = new[]
        {
            "Петро", "Олександр", "Марія", "Олена", "Петро", "Андрій", "Катерина",
            "Михайло", "Наталія", "Сергій", "Валентина", "Юрій", "Анна", "Василь",
            "Ірина", "Тетяна", "Дмитро", "Ярослав", "Оксана", "Максим", "Вікторія",
            "Володимир", "Людмила", "Микола", "Світлана", "Олег", "Євген", "Галина",
            "Ніна", "Павло"
        };

        return GetRandomItem(names);
    }

    public static string GenerateLastName()
    {
        string[] lastNames = new[]
        {
            "Порошенко", "Іваненко", "Коваленко", "Бондаренко", "Шевченко", "Мельник",
            "Ткаченко", "Савченко", "Кравченко", "Поліщук", "Василенко", "Гончаренко",
            "Павленко", "Федоренко", "Пономаренко", "Кириленко", "Лисенко", "Козак",
            "Сидоренко", "Марченко", "Кравчук", "Бойко", "Дмитренко", "Гнатюк"
        };

        return GetRandomItem(lastNames);
    }

    public static string GenerateUsername(string firstName, string lastName)
    {
        return $"{firstName.ToLower()}_{lastName.ToLower()}";
    }

    public static string GeneratePhotoUrl()
    {
        var imageNumber = _random.Next(1, 10);
        return $"https://example.com/photos/avatar{imageNumber}.jpg";
    }

    public static long GenerateTelegramId()
    {
        return _random.Next(100000000, 999999999);
    }

    public static string GenerateEventName()
    {
        string[] eventTypes = new[]
        {
            "Турнір", "Зустріч", "Марафон", "Фестиваль", "Челендж", "Розіграш",
            "Групова кампанія", "Майстер-клас", "Воркшоп", "Відкритий урок"
        };

        string[] games = new[]
        {
            "Magic: The Gathering", "Dungeons & Dragons", "Warhammer 40K", "Yu-Gi-Oh!",
            "Hearthstone", "Legends of Runeterra", "Pathfinder", "Pokemon TCG", "Gwent",
            "Настільні ігри", "Карткові ігри", "Рольові ігри"
        };

        return $"{GetRandomItem(eventTypes)} з {GetRandomItem(games)}";
    }

    public static string GenerateEventDescription()
    {
        string[] descriptions = new[]
        {
            "Щомісячний турнір для новачків та просунутих гравців",
            "Щотижнева кампанія для досвідчених гравців",
            "Щомісячні змагання з популярних настільних ігор",
            "Приходьте на розіграш рідкісних карт!",
            "Марафон по популярним картковим іграм",
            "Щомісячний челендж для справжніх геймерів",
            "Групова кампанія для новачків у світі настільних ігор",
            "Зустріч для гравців та фанатів карткових ігор",
            "Турнір для всіх фанатів популярних ігор",
            "Фестиваль з різноманітних настільних ігор для всієї родини"
        };

        return GetRandomItem(descriptions);
    }

    public static string GenerateEventFormat()
    {
        string[] formats = new[]
        {
            "Standard", "Campaign", "Tournament", "Lottery", "Marathon",
            "Challenge", "Meetup", "Festival", "Workshop", "Draft"
        };

        return GetRandomItem(formats);
    }

    public static string GenerateGameName()
    {
        string[] games = new[]
        {
            "Magic: The Gathering", "Dungeons & Dragons", "Warhammer 40K", "Yu-Gi-Oh!",
            "Hearthstone", "Legends of Runeterra", "Pathfinder", "Pokemon TCG", "Gwent",
            "Настільні ігри", "Карткові ігри", "Рольові ігри"
        };

        return GetRandomItem(games);
    }

    public static string GenerateGoodName()
    {
        string[] prefixes = new[]
        {
            "Бустер", "Колода", "Набір", "Книга", "Фігурка", "Дошка",
            "Колекційний набір", "Декорації", "Гральні кості", "Колекційна карта"
        };

        string[] games = new[]
        {
            "Magic: The Gathering", "Dungeons & Dragons", "Warhammer 40K", "Yu-Gi-Oh!",
            "Hearthstone", "Pokémon", "Pathfinder", "Monopoly", "настільних ігор"
        };

        return $"{GetRandomItem(prefixes)} {GetRandomItem(games)}";
    }

    public static string GenerateGoodDescription()
    {
        string[] descriptions = new[]
        {
            "Бустер останнього випуску популярної гри",
            "Готова колода для популярного формату",
            "Набір для настільних ігор",
            "Базова книга правил для популярної гри",
            "Колекційна фігурка для настільних ігор",
            "Класична настільна гра для всієї родини",
            "Колекційний набір карт для популярної гри",
            "Декорації для настільних ігор",
            "Набір кольорових гральних костей для настільних ігор",
            "Рідкісна карта для колекціонерів"
        };

        return GetRandomItem(descriptions);
    }

    public static string GenerateCardLink()
    {
        string[] sets = new[] { "dom", "war", "mh1", "thb", "eld", "mh2" };
        var number = _random.Next(1, 100);
        var set = GetRandomItem(sets);

        return $"https://scryfall.com/card/{set}/{number}/card-name";
    }

    public static string GenerateCardCode()
    {
        string[] codes = new[] { "DOM", "WAR", "MH1", "THB", "ELD", "MH2" };
        return GetRandomItem(codes);
    }

    public static string GenerateEventImage()
    {
        string[] imagePaths = new[]
        {
            "src/assets/images/Modern.png",
            "src/assets/images/Pioner.png",
            "src/assets/images/Standart.png",
            "src/assets/images/Commander.png",
            "src/assets/images/Pauper.png",
            "src/assets/images/Lorcana.png",
            "src/assets/images/Pocemon.png",
            "src/assets/images/Draft.png",
            "src/assets/images/Sealed.png"
        };
        return GetRandomItem(imagePaths);
    }
}