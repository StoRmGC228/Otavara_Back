namespace API.BackgroundServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class BookingExpirationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

    public BookingExpirationService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
                await bookingService.RemoveExpiredBookingsAsync();
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
}
