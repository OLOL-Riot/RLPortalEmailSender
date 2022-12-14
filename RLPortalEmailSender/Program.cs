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
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailCred"));

builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<MessageToSendConsumer, MessageToSendConsumerDefinition>();

    x.UsingRabbitMq((context, cfg) =>
    {
        RabbitOptions rabbitOptions = new();
        rabbitOptions.RABBIT_IP = builder.Configuration.GetSection("RABBIT_IP").Get<string>();
        rabbitOptions.RABBITMQ_DEFAULT_USER = builder.Configuration.GetSection("RABBITMQ_DEFAULT_USER").Get<string>();
        rabbitOptions.RABBITMQ_DEFAULT_PASS = builder.Configuration.GetSection("RABBITMQ_DEFAULT_PASS").Get<string>();
        cfg.Host(rabbitOptions.RABBIT_IP, "/", h =>
        {
            h.Username(rabbitOptions.RABBITMQ_DEFAULT_USER);
            h.Password(rabbitOptions.RABBITMQ_DEFAULT_PASS);
        });

        cfg.ConfigureEndpoints(context);


    });
});




var app = builder.Build();


System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
    | System.Net.SecurityProtocolType.Tls
    | System.Net.SecurityProtocolType.Tls11
    | System.Net.SecurityProtocolType.Tls13;

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
