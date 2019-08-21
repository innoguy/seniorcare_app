using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace XF.Infrastructure.Core
{
    public interface IViewModel
    {
        string Title { get; set; }
        bool IsSelected { get; set; }
        Task Init(object args);
        void OnAppearing();
        void OnDisappearing();
        bool OnBackButtonPressed(Action callback);
    }
}
