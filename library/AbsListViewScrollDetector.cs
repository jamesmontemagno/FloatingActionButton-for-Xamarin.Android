

using Android.Widget;
using System;
namespace com.refractored.fab
{
  abstract class AbsListViewScrollDetector : Java.Lang.Object, AbsListView.IOnScrollListener
  {
    private int lastScrollY;
    private int previousVisibleItem;


    public AbsListView.IOnScrollListener OnScrollListener { get; set; }

    public AbsListView ListView { get; set; }
    public int ScrollThreshold { get; set; }

    public abstract void OnScrollUp();
    public abstract void OnScrollDown();

    private bool IsSameRow(int firstVisibleItem)
    {
      return firstVisibleItem == previousVisibleItem;
    }

    private int TopItemScrollY
    {
      get
      {
        if (ListView == null || ListView.GetChildAt(0) == null)
          return 0;

        var topChild = ListView.GetChildAt(0);
        return topChild.Top;
      }
    }
    
    public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
    {
      if (OnScrollListener != null)
        OnScrollListener.OnScroll(view, firstVisibleItem, visibleItemCount, totalItemCount);

      if (totalItemCount == 0)
        return;

      if (IsSameRow(firstVisibleItem))
      {
        var newScrollY = TopItemScrollY;
        var isSignificantDelta = Math.Abs(lastScrollY - newScrollY) > ScrollThreshold;
        if (isSignificantDelta)
        {
          if (lastScrollY > newScrollY)
            OnScrollUp();
          else
            OnScrollDown();
        }
        lastScrollY = newScrollY;
      }
      else
      {
        if (firstVisibleItem > previousVisibleItem)
          OnScrollUp();
        else
          OnScrollDown();

        lastScrollY = TopItemScrollY;
        previousVisibleItem = firstVisibleItem;
      }
    }


    public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
    {
      if (OnScrollListener != null)
        OnScrollListener.OnScrollStateChanged(view, scrollState);
    }
  }
}