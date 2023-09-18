using MassTransit;

namespace TestMassTransit
{
	public record BookAppointmentCommand
    {
    }
    public class BookAppointmentCommandHandler : IConsumer<BookAppointmentCommand>
    {
        private readonly ILogger<BookAppointmentCommandHandler> _logger;
        public BookAppointmentCommandHandler(ILogger<BookAppointmentCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BookAppointmentCommand> context)
        {

            try
            {
                //1 - Booking an appointment....
                //...
                //..
                //.

                //2 -SMS reminder sent 24 hours before appointment...

                //DEMO
				var when = DateTime.UtcNow.AddMinutes(2);

                _logger.LogInformation("scheduling TC URL to be sent {scheduledSend}", when.ToString());

                var scheduledMessage = await context.SchedulePublish<SendSmsCommand>(when, new() { Message = "Hello Worlds!" });

                _logger.LogInformation("Scheduled message {guid}", scheduledMessage.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send SMS test");
            }
        }
    }
}