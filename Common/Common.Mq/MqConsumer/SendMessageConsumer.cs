using Gucm.Application.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// 
namespace Common.Mq
{
    public class SendMessageConsumer : IConsumer<GdprCreated>
    {
        public Task Consume(ConsumeContext<GdprCreated> context)
        {
            Console.WriteLine($"Receive message value: {context.Message.Id}");

            return Task.CompletedTask;
        }
    }
}
