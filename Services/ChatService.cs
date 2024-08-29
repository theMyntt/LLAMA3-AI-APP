using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Llama_APP.Models;

namespace Llama_APP.Services;

public class ChatService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    private ObservableCollection<MessageModel> messages;

    public ChatService()
    {
        messages = new ObservableCollection<MessageModel>();
        _http = new();
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private async Task<MessageModel?> _sendMessageAsync(JsonObject request)
    {
        Uri uri = new("http://localhost:5000/api/prompt");

        var response = await _http.PostAsJsonAsync(uri, request);
        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(body);
        }

        return JsonSerializer.Deserialize<MessageModel>(body, _jsonSerializerOptions);
    }
    
    public async Task<ObservableCollection<MessageModel>> PromptAsync(string message)
    {
        var request = new JsonObject
        {
            ["message"] = message
        };

        
        var response = await _sendMessageAsync(request);
            
        messages.Add(response);

        return messages;
    }
}