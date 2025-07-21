using Application.Interfaces;
using Hangfire;

namespace API.Configurations;

public static class ServiceProviderExtensions
{
    public static void AddRecurringJobs(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        var logger = provider.GetRequiredService<ILoggerFactory>()
            .CreateLogger("RecurringJobs");

        logger.LogInformation("Registering recurring Hangfire jobs...");

        RecurringJob.AddOrUpdate<IAnnouncementRepository>(
            "cleanup-overdue-announcements",
            repo => repo.DeleteOverdueAnnouncements(),
            Cron.Daily,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local, // або UTC, залежно від логіки
            });

        logger.LogInformation("Recurring job 'cleanup-overdue-announcements' registered.");
    }
}