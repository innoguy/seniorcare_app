using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XF.Infrastructure.Core.Controls
{
    public interface ITabsViewModel  : IViewModel
    {
        IEnumerable<IViewModel> TabItems { get; set; }
        IViewModel SelectedTab { get; set; }
        ICommand TabItemTappedCommand { get; }
    }
}
