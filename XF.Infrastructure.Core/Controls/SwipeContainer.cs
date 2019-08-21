using System;
using Xamarin.Forms;

namespace XF.Infrastructure.Core.Controls
{
    public class SwipeContainer : ContentView
    {
        public event EventHandler<SwipedEventArgs> Swipe;

        //private SwipeGestureRecognizer _swipeLeft;
        //private SwipeGestureRecognizer _swipeRight;

        public SwipeContainer()
        {
            //_swipeLeft = new SwipeGestureRecognizer() { Direction = SwipeDirection.Left };
            //_swipeRight = new SwipeGestureRecognizer() { Direction = SwipeDirection.Right };

            //_swipeLeft.Swiped += SwipeLeft_Swiped;
            //_swipeRight.Swiped += SwipeRight_Swiped;

            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Left));
            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Right));
            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Up));
            GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Down));
        }

        public void SwipeLeeftRight(object sender, SwipedEventArgs e)
        {
            Swipe?.Invoke(this, e);
        }

      
        SwipeGestureRecognizer GetSwipeGestureRecognizer(SwipeDirection direction)
        {
            var swipe = new SwipeGestureRecognizer { Direction = direction };
            swipe.Swiped += (sender, e) => Swipe?.Invoke(this, e);
            return swipe;
        }


    }
}
