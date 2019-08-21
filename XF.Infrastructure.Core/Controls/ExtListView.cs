using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class ExtListView : Xamarin.Forms.ListView
    {
        public SwipeContainer GetSwipeContainer()
        {
            SwipeContainer result = null;
            var parent = this.Parent;
            while (parent != null)
            {
                var x = parent as SwipeContainer;
                if (x != null)
                {
                    result = x;
                    break;
                }
                else
                {
                    parent = parent.Parent;
                }
            }

            return result;
        }
    }
}
