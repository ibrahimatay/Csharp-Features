var builder = WebApplication.CreateBuilder(args);

// Keyed services
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0#keyed-services
builder.Services.AddKeyedSingleton<INotificationService, SmsService>(Constants.SMS);
builder.Services.AddKeyedSingleton<INotificationService, EmailService>(Constants.Email);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/notify", (Notification notification) =>
{
    var notificationService = GetNotificationService(notification, app.Services);
    notificationService?.Notify(notification.Message);

    return Results.Created();
    
    //https://github.com/ibrahimatay/Csharp-Features/blob/master/StaticLocalFunctions/Program.cs
    static INotificationService? GetNotificationService(Notification notification, IServiceProvider serviceProvider) => notification.Channel switch
    {
        "sms" => serviceProvider.GetKeyedService<INotificationService>(Constants.SMS),
        "email" => serviceProvider.GetKeyedService<INotificationService>(Constants.Email),
        _ => throw new NotSupportedException()
    };
});

app.Run();

public record Notification(string Message, string Channel);

static class Constants
{
    public const string SMS = "SMS";
    public const string Email = "Email";
}


interface INotificationService
{
    void Notify(string message);
}

class SmsService : INotificationService
{
    public void Notify(string message)=> Console.WriteLine($"Sent SMS! Message: {message}");
}

class EmailService: INotificationService
{
    public void Notify(string message)=> Console.WriteLine($"Sent Email!  Message: {message}");
}