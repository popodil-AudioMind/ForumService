using Audiomind.RabbitMQ.Moddels;
using ForumService.Data;
using MassTransit;

namespace Audiomind.RabbitMQ
{
    public class DeleteConsumer : IConsumer<DeleteMessage>
    {
        private ISqlForum _sqlForum;
        public DeleteConsumer(ISqlForum sqlForum) 
        {
            _sqlForum = sqlForum;
        }
        public Task Consume(ConsumeContext<DeleteMessage> context)
        {
            _sqlForum.GDPRDeleteForum(context.Message.id);
            return Task.CompletedTask;
        }
    }
}
