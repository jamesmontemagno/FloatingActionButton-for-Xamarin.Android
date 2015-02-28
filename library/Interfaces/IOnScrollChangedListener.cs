

using Android.Widget;
namespace com.refractored.fab
{
  public interface IOnScrollChangedListener
  {
    void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt);
  }
}