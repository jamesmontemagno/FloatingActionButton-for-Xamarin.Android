
using Android.Content;
using Android.Util;
using Android.Widget;
namespace com.refractored.fab
{
  public class ObservableScrollView : ScrollView
  {
    /// <summary>
    /// Gets or sets the on scroll changed listener
    /// </summary>
    public IOnScrollChangedListener OnScrollChangedListener
    {
      get;
      set;
    }

    public ObservableScrollView(Context context)
      : base(context)
    {
    }

    public ObservableScrollView(Context context, IAttributeSet attrs) 
      : base (context, attrs)
    {
    }

    public ObservableScrollView(Context context, IAttributeSet attrs, int defStyles)
      : base(context, attrs, defStyles)
    {
    }

    protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
    {
      base.OnScrollChanged(l, t, oldl, oldt);
      if (OnScrollChangedListener != null)
      {
        OnScrollChangedListener.OnScrollChanged(this, l, t, oldl, oldt);
      }
    }

  }
}