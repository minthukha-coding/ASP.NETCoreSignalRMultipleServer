using SignalRMultipleServerTestingConsole;

Console.WriteLine("Starting SignarlR service");
Console.ReadKey();

SignalRService service = new SignalRService();
await service.PushNotification();

Console.ReadKey();