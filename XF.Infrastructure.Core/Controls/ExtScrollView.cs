using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class ExtScrollView : ScrollView
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
