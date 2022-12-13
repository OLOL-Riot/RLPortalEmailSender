using RLPortalEmailSender.Container.Consumers;
using RLPortalEmailSender.Service;
using RLPortalEmailSender.Service.Impl;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ISMTPService, SMTPService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<MessageToSendConsumer, MessageToSendConsumerDefinition>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var s = Environment.GetEnvironmentVariable("RABBIT_IP") ?? "localhost";
        cfg.Host(s, "/", h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USER"));
            h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASS"));
        });

        cfg.ConfigureEndpoints(context);


    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
        | System.Net.SecurityProtocolType.Tls
        | System.Net.SecurityProtocolType.Tls11
        | System.Net.SecurityProtocolType.Tls13;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
