using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRMultipleServerTestingConsole;
public class SignalRService
{
    //private readonly CustomSettingModel _setting;
    //private readonly HubConnection _connection;
    private readonly HubConnection[] _connections;

    public SignalRService()
    {
        try
        {
            //_setting = setting.CurrentValue;
            //_connection = new HubConnectionBuilder()
            //    .WithUrl(_setting.IBankingHubConnectionUrl)
            //    .Build();

            string[] urls = { "https://localhost:7000/chathub", "https://localhost:7001/chathub" }; // get ibanking servers
            _connections = new HubConnection[urls.Length];
            for (int i = 0; i < urls.Length; i++)
            {
                _connections[i] = new HubConnectionBuilder()
                .WithUrl(urls[i])
                .Build();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
    }

    public async Task PushNotification()
    {
        //if (_connection.State == HubConnectionState.Disconnected)
        //    await _connection.StartAsync();
        //await _connection.InvokeAsync("PushNotification", reqModel);

        foreach (var connection in _connections)
        {
            if (connection.State == HubConnectionState.Disconnected)
                await connection.StartAsync();
            await connection.InvokeAsync("SendMessage", "Console App", "HI From Console");
        }
    }
}