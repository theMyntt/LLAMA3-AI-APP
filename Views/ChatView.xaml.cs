using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Llama_APP.ViewModels;

namespace Llama_APP.Views;

public partial class ChatView : ContentPage
{
    public ChatView()
    {
        InitializeComponent();
        BindingContext = new ChatMessageViewModel();
    }
}