using EmailNotificationWebHook.Consumer;
using EmailNotificationWebHook.Service;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IEmailService, EmailService>();
// RabbitMQ Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WebhookConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("email-webhook", e =>
        {
            e.ConfigureConsumer<WebhookConsumer>(context);
        });
    });
});

var app = builder.Build();

app.MapPost("/email-webhook", ([FromBody] EmailDTO emailDTO, IEmailService emailService) =>
{
    string result = emailService.SendEmail(emailDTO);

    return Task.FromResult(result);
});

app.Run();
