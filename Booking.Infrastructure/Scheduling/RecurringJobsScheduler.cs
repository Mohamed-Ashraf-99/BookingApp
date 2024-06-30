using Booking.Domain.Repositories;
using Booking.Infrastructure.Repositories;
using Hangfire;


namespace Booking.Infrastructure.Scheduling;

public class JobsScheduler
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IReservationRepository _reservationRepository;
    private readonly IOfferRepository _offerRepository;


    public JobsScheduler(IRecurringJobManager recurringJobManager, IReservationRepository reservationService,
        IOfferRepository offerRepository)
    {
        _recurringJobManager = recurringJobManager;
        _reservationRepository = reservationService;
        _offerRepository = offerRepository;
    }

    public void ScheduleRecurringJobs()
    {
        _recurringJobManager.AddOrUpdate(
        "DeleteExpiredReservations",
                () => _reservationRepository.DeleteExpiredReservationsAsync(),
                "*/5 * * * *"); // Every 5 minutes

        _recurringJobManager.AddOrUpdate(
     "DeleteExpiredReservations",
             () => _offerRepository.DeleteExpiredOffer(),
             "*/5 * * * *"); // Every 5 minutes
    }
}
