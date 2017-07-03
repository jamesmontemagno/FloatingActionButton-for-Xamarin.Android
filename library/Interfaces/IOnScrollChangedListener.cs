

using Android.Widget;
namespace Refractored.Fab
{
    public interface IOnScrollChangedListener
    {
        void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt);
    }
}