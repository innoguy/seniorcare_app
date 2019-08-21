using Android.Content;
using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Infrastructure.Core.Controls;
using XF.Infrastructure.Core.Droid.GestureListeners;
using XF.Infrastructure.Core.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtListView), typeof(ExtListViewRenderer))]
namespace XF.Infrastructure.Core.Droid.Renderers
{



    public class ExtListViewRenderer : ListViewRenderer
    {
        readonly CustomSwipeGestureListener _listener;
        readonly GestureDetector _detector;
        public ExtListViewRenderer(Context context) : base(context)
        {
            _listener = new CustomSwipeGestureListener();
            _detector = new GestureDetector(context, _listener);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            bool result = base.DispatchTouchEvent(e);
            if (_detector != null)
            {
                _detector.OnTouchEvent(e);
            }
            return result;

        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            base.OnTouchEvent(ev);

            if (_detector != null)
                return _detector.OnTouchEvent(ev);

            return false;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                _listener.OnSwipeLeft -= OnSwipeLeft;
                _listener.OnSwipeRight -= OnSwipeRight;
            }

            if (e.OldElement == null)
            {
                _listener.OnSwipeLeft += OnSwipeLeft; ;
                _listener.OnSwipeRight += OnSwipeRight;
            }
        }

        private void OnSwipeLeft(object sender, EventArgs e)
        {
            var swipeContainer = ((ExtListView)Element).GetSwipeContainer();
            if (swipeContainer != null)
            {
                swipeContainer.SwipeLeeftRight(Element, new SwipedEventArgs(null, SwipeDirection.Left));
            }
        }

        private void OnSwipeRight(object sender, EventArgs e)
        {
            var swipeContainer = ((ExtListView)Element).GetSwipeContainer();
            if (swipeContainer != null)
            {
                swipeContainer.SwipeLeeftRight(Element, new SwipedEventArgs(null, SwipeDirection.Right));
            }
        }
    }
}