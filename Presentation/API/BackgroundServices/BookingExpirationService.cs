//namespace API.BackgroundServices;

//using Application.Interfaces;

//public class BookingExpirationService : BackgroundService
//{
//    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);
//    private readonly IServiceProvider _serviceProvider;

//    public BookingExpirationService(
//        IServiceProvider serviceProvider)
//    {
//        _serviceProvider = serviceProvider;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            using (var scope = _serviceProvider.CreateScope())
//            {
//                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
//                await bookingService.RemoveExpiredBookingsAsync();
//            }

//            await Task.Delay(_checkInterval, stoppingToken);
//        }
//    }
//}