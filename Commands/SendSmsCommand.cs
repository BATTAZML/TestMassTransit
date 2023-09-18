using MassTransit;

namespace TestMassTransit
{
    public record SendSmsCommand
    {
        public string? Message { get; set; }
    }
    public class SendSmsCommandHandler : IConsumer<SendSmsCommand>
    {
        public SendSmsCommandHandler() { }

        public Task Consume(ConsumeContext<SendSmsCommand> context)
        {
            return Task.CompletedTask;
        }
    }
}