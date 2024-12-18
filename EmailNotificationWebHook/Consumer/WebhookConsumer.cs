using MassTransit;
using Shared.DTOs;

namespace EmailNotificationWebHook.Consumer;

public class WebhookConsumer(HttpClient httpClient) : IConsumer<EmailDTO>
{
    public async Task Consume(ConsumeContext<EmailDTO> context)
    {
        var result = await httpClient.PostAsJsonAsync("https://localhost:7039/email-webhook",
            new EmailDTO(context.Message.Title, context.Message.Content));

        result.EnsureSuccessStatusCode();
    }
}
