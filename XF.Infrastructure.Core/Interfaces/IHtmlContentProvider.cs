using System;
using System.Collections.Generic;
using System.Text;

namespace XF.Infrastructure.Core.Interfaces
{
    public interface IHtmlContentProvider
    {
        string GetHtmlContentForFile(string fileName); 
    }
}
