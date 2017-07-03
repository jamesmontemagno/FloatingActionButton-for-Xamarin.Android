

using Android.Runtime;
using Android.Support.V7.Widget;
using System;
namespace Refractored.Fab
{
    abstract class RecyclerViewScrollDetector : RecyclerView.OnScrollListener
    {
        public RecyclerViewScrollDetector() : base()
        {

        }
        public RecyclerViewScrollDetector(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public int ScrollThreshold { get; set; }
        public RecyclerView.OnScrollListener OnScrollListener { get; set; }
        public abstract void OnScrollUp();
        public abstract void OnScrollDown();
        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            if (OnScrollListener != null)
                OnScrollListener.OnScrolled(recyclerView, dx, dy);

            var isSignificantDelta = Math.Abs(dy) > ScrollThreshold;
            if (isSignificantDelta)
            {
                if (dy > 0)
                    OnScrollUp();
                else
                    OnScrollDown();
            }
        }

        public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
        {
            if (OnScrollListener != null)
                OnScrollListener.OnScrollStateChanged(recyclerView, newState);

            base.OnScrollStateChanged(recyclerView, newState);
        }
    }
}