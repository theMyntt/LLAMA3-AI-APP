using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Llama_APP.Models;
using Llama_APP.Services;

namespace Llama_APP.ViewModels;

public partial class ChatMessageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<MessageModel> messages;

    [ObservableProperty] 
    private string prompt;

    private ChatService service = new();
    
    public ICommand GetMessages { get; set; }

    public ChatMessageViewModel()
    {
        GetMessages = new Command(async () => await GetMessageAsync());
    }

    public async Task GetMessageAsync()
    {
        Messages = await service.PromptAsync(Prompt);
    }
}