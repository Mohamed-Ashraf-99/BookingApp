using Booking.Domain.Repositories;
using Hangfire;


namespace Booking.Infrastructure.Scheduling;

public class JobsScheduler
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IReservationRepository reservationRepository;

    public JobsScheduler(IRecurringJobManager recurringJobManager, IReservationRepository reservationService)
    {
        _recurringJobManager = recurringJobManager;
        reservationRepository = reservationService;
    }

    public void ScheduleRecurringJobs()
    {
        _recurringJobManager.AddOrUpdate(
            "DeleteExpiredReservations",
            () => reservationRepository.DeleteExpiredReservationsAsync(),
            "0 0 * * *");
    }
}
