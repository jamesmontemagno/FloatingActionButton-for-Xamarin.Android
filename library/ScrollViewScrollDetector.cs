

using System;
namespace com.refractored.fab
{
  abstract class ScrollViewScrollDetector : IOnScrollChangedListener
  {
    public IOnScrollChangedListener OnScrollChangedListener { get; set; }
    private int lastScrollY;
    public int ScrollThreshold
    {
      get;
      set;
    }
    public abstract void OnScrollUp();
    public abstract void OnScrollDown();

    public void OnScrollChanged(Android.Widget.ScrollView who, int l, int t, int oldl, int oldt)
    {
      if (OnScrollChangedListener != null)
        OnScrollChangedListener.OnScrollChanged(who, l, t, oldl, oldt);

      var isSignificantDelta = Math.Abs(t - lastScrollY) > ScrollThreshold;
      if (isSignificantDelta)
      {
        if (t > lastScrollY)
          OnScrollUp();
        else
          OnScrollDown();
      }

      lastScrollY = t;
    }

  }
}