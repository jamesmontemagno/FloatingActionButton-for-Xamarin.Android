
/*
 * Android Google+ like floating action button, which reacts tofn the attached ListView scrolling events.
 * 
 * Derived & ported from Oleksandr Melnykov: https://github.com/makovkastar/FloatingActionButton
 * 
 * Ported by: James Montemagno http://github.com/jamesmontemagno
 * 
 */

#if __ANDROID_12__
using Android.Animation;
#endif
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;

namespace com.refractored.fab
{
  public enum FabSize
  {
    Normal,
    Mini
  }
    [Register("com.refractored.fab.FloatingActionButton")]
  public class FloatingActionButton : ImageButton, ViewTreeObserver.IOnPreDrawListener
  {
    private const int TranslateDurationMillis = 200;

    #if __ANDROID_12__
    private ITimeInterpolator interpolator = new AccelerateDecelerateInterpolator ();
    #endif
    private bool marginsSet;
    

    private int shadowSize;

    private int scrollThreshold;

    /// <summary>
    /// Gets if FAB is visible
    /// </summary>
    public bool Visible { get; private set; }

    private int colorNormal;

    /// <summary>
    /// Gets or sets the normal color
    /// </summary>
    public int ColorNormal {
      get { return colorNormal; }
      set {
        if (colorNormal == value)
          return;

        colorNormal = value;
        UpdateBackground ();
      }
    }

    /// <summary>
    /// Sets the color normal by res id
    /// </summary>
    /// <param name="colorresId"></param>
    public void SetColorNormalResId (int colorResId)
    {
      ColorNormal = Resources.GetColor (colorResId);
    }

    private int colorPressed;

    /// <summary>
    /// Gets or sets pressed color
    /// </summary>
    public int ColorPressed {
      get { return colorPressed; }
      set {
        if (colorPressed == value)
          return;

        colorPressed = value;
        UpdateBackground ();
      }
    }

    /// <summary>
    /// Sets color pressed by res id
    /// </summary>
    /// <param name="colorResId"></param>
    public void SetColorPressedResId (int colorResId)
    {
      ColorPressed = Resources.GetColor (colorResId);
    }



    private int colorDisabled;

    /// <summary>
    /// Gets or sets the normal color
    /// </summary>
    public int ColorDisabled {
      get { return colorDisabled; }
      set {
        if (colorDisabled == value)
          return;

        colorDisabled = value;
        UpdateBackground ();
      }
    }

    /// <summary>
    /// Sets the color normal by res id
    /// </summary>
    /// <param name="colorresId"></param>
    public void SetColorDisabledResId (int colorResId)
    {
      ColorDisabled = Resources.GetColor (colorResId);
    }

    private int colorRipple;

    /// <summary>
    /// Gets or sets ripple color
    /// </summary>
    public int ColorRipple {
      get { return colorRipple; }
      set {
        if (colorRipple == value)
          return;

        colorRipple = value;
        UpdateBackground ();
      }
    }

    /// <summary>
    /// Sets color ripple by res id
    /// </summary>
    /// <param name="colorResId"></param>
    public void SetColorRippleResId (int colorResId)
    {
      ColorRipple = Resources.GetColor (colorResId);
    }

    private bool hasShadow;

    /// <summary>
    /// Gets or sets if it has a shadow
    /// </summary>
    public bool HasShadow {
      get { return hasShadow; }
      set {
        if (hasShadow == value)
          return;

        hasShadow = value;
        UpdateBackground ();
      }
    }

    private FabSize size = FabSize.Normal;

    public FabSize Size {
      get { return size; }
      set {
        if (size == value)
          return;

        size = value;
        UpdateBackground ();
      }
    }

    /// <summary>
    /// Show the FAB
    /// </summary>
    /// <param name="animate">If you want to animate, default true</param>
    public void Show (bool animate = true)
    {
      Toggle (true, animate, false);
    }

    /// <summary>
    /// Hide the FAB
    /// </summary>
    /// <param name="animate">If you want to animate, default true</param>
    public void Hide (bool animate = true)
    {
      Toggle (false, animate, false);
    }

	public FloatingActionButton(IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
		: base(handle, transfer)
	{
	}

    public FloatingActionButton (Context context)
      : this (context, null)
    {
    }

    public FloatingActionButton (Context context, IAttributeSet attrs)
      : base (context, attrs)
    {
      Init (context, attrs);
    }

    public FloatingActionButton (Context context, IAttributeSet attrs, int defStyle)
      : base (context, attrs, defStyle)
    {
      Init (context, attrs);
    }

    private bool lastToggleAnimate;

    private void Toggle (bool visible, bool animate, bool force)
    {
      if (Visible != visible || force) {
        Visible = visible;
        lastToggleAnimate = animate;
        int height = Height;
        if (height == 0 && !force) {
          var vto = ViewTreeObserver;
          if (vto.IsAlive) {
            vto.AddOnPreDrawListener (this);
            return;
          }
        }
        var translationY = visible ? 0 : height + MarginBottom;
        if (animate) {

          if ((int)Build.VERSION.SdkInt >= 12) {
            #if __ANDROID_12__
            Animate ().SetInterpolator (interpolator)
            .SetDuration (TranslateDurationMillis)
            .TranslationY (translationY);
            #endif
          } else {
            var oldY = !visible ? 0 : height + MarginBottom;
            var animation = new TranslateAnimation (0, 0, oldY, translationY);
            animation.Duration = TranslateDurationMillis;
            if (visible)
            {
              animation.AnimationStart += (sender, e) => {
                this.Visibility = ViewStates.Visible;
              };
            }   
            else {
              animation.AnimationEnd += (sender, e) => {
                this.Visibility = ViewStates.Gone; 
              };
            }
            this.StartAnimation (animation);

          }
        } else {

          if ((int)Build.VERSION.SdkInt >= 11) {
            #if __ANDROID_11__
            this.TranslationY = translationY;
            #endif
          }
          else{
            var oldY = !visible ? 0 : height + MarginBottom;
            var animation = new TranslateAnimation (0, 0, oldY, translationY);
            animation.Duration = 0;

            if (visible)
            {
              animation.AnimationStart += (sender, e) => {
                this.Visibility = ViewStates.Visible;
              };
            }   
            else {
              animation.AnimationEnd += (sender, e) => {
                this.Visibility = ViewStates.Gone; 
              };
            }
            this.StartAnimation (animation);
          }

        }

        if (!HasHoneycombApi) {
          Clickable = visible;
        }
      }
    }

    protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
    {
      base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
      var theSize = Resources.GetDimensionPixelSize (size == FabSize.Normal ?
        Resource.Dimension.fab_size_normal :
        Resource.Dimension.fab_size_mini);
      if (hasShadow && !HasLollipopApi) {
        theSize += shadowSize * 2;
        SetMarginsWithoutShadow ();
      }
      SetMeasuredDimension (theSize, theSize);
    }

    private void Init (Context context, IAttributeSet attributeSet)
    {
      Visible = true;
      colorNormal = Resources.GetColor (Resource.Color.fab_material_blue_500);
      colorPressed = DarkenColor(colorNormal);
      colorRipple = LightenColor (colorNormal);
      colorDisabled = Resources.GetColor (Android.Resource.Color.DarkerGray);
      size = FabSize.Normal;
      hasShadow = true;
      scrollThreshold = Resources.GetDimensionPixelOffset (Resource.Dimension.fab_scroll_threshold);
      shadowSize = Resources.GetDimensionPixelSize (Resource.Dimension.fab_shadow_size);
      if (attributeSet != null)
        InitAttributes (context, attributeSet);

      UpdateBackground ();
    }

    private void InitAttributes (Context context, IAttributeSet attributeSet)
    {
      var attr = context.ObtainStyledAttributes (attributeSet, Resource.Styleable.FloatingActionButton, 0, 0);
      if (attr == null)
        return;

      try {
       
        colorNormal = attr.GetColor (Resource.Styleable.FloatingActionButton_fab_colorNormal, Resources.GetColor (Resource.Color.fab_material_blue_500));
        colorPressed = attr.GetColor (Resource.Styleable.FloatingActionButton_fab_colorPressed, DarkenColor(colorNormal));
        colorRipple = attr.GetColor (Resource.Styleable.FloatingActionButton_fab_colorRipple, LightenColor(colorNormal));
        colorDisabled = attr.GetColor (Resource.Styleable.FloatingActionButton_fab_colorDisabled, Resources.GetColor (Android.Resource.Color.DarkerGray));
        hasShadow = attr.GetBoolean (Resource.Styleable.FloatingActionButton_fab_shadow, true);
        size = (FabSize)attr.GetInt (Resource.Styleable.FloatingActionButton_fab_size, 0);
      } catch (Exception ex) {
      } finally {
        attr.Recycle ();
      }
    }

    private void UpdateBackground ()
    {
      var drawable = new StateListDrawable ();
      drawable.AddState (new int[] { Android.Resource.Attribute.StatePressed }, CreateDrawable (colorPressed));
      drawable.AddState (new int[] { -Android.Resource.Attribute.StateEnabled }, CreateDrawable (colorDisabled));
      drawable.AddState (new int[] { }, CreateDrawable (colorNormal));
      SetBackgroundCompat (drawable);
    }

    private Drawable CreateDrawable (int color)
    {
      var ovalShape = new OvalShape ();
      var shapeDrawable = new ShapeDrawable (ovalShape);
      shapeDrawable.Paint.Color = new Color (color);
      if (hasShadow && !HasLollipopApi) {
        var shadowDrawable = Resources.GetDrawable (size == FabSize.Normal ? 
          Resource.Drawable.fab_shadow :
          Resource.Drawable.fab_shadow_mini);

        var layerDrawable = new LayerDrawable (new Drawable[]{ shadowDrawable, shapeDrawable });
        layerDrawable.SetLayerInset (1, shadowSize, shadowSize, shadowSize, shadowSize);
        return layerDrawable;
      } else {
        return shapeDrawable;
      }
    }

    private void SetMarginsWithoutShadow ()
    {
      if (marginsSet)
        return;

      var layoutParams = LayoutParameters as ViewGroup.MarginLayoutParams;
      if (layoutParams == null)
        return;

      var leftMargin = layoutParams.LeftMargin - shadowSize;
      var topMargin = layoutParams.TopMargin - shadowSize;
      var rightMargin = layoutParams.RightMargin - shadowSize;
      var bottomMargin = layoutParams.BottomMargin - shadowSize;

      layoutParams.SetMargins (leftMargin, topMargin, rightMargin, bottomMargin);
      RequestLayout ();
      marginsSet = true;
    }

    #if __ANDROID_21__
    private class MyOutlineProvider : ViewOutlineProvider
    {
      Android.Content.Res.Resources res;
      FabSize fabSize;

      public MyOutlineProvider (Android.Content.Res.Resources res, FabSize size)
      {
        this.res = res;
        this.fabSize = size;
      }

      public override void GetOutline (View view, Outline outline)
      {
        var size = res.GetDimensionPixelSize (fabSize == FabSize.Normal ? Resource.Dimension.fab_size_normal : Resource.Dimension.fab_size_mini);
        outline.SetOval (0, 0, size, size);
      }
    
    }
    #endif

    private void SetBackgroundCompat (Drawable drawable)
    {
      if (HasLollipopApi) {
#if __ANDROID_21__
        var elevation = 0.0f;
        if (hasShadow) {
          elevation = Elevation > 0.0f ? Elevation : Resources.GetDimensionPixelSize (Resource.Dimension.fab_elevation_lollipop);
        }

        Elevation = elevation;
        var states = new int[][] { new int[]{ } };
        var rippleDrawable = new RippleDrawable (new Android.Content.Res.ColorStateList (states, new int[]{ colorRipple }), drawable, null);
        OutlineProvider = new MyOutlineProvider (Resources, size);
        ClipToOutline = true;
        Background = rippleDrawable;
#endif
      } else if (HasJellyBeanApi) {
        #if __ANDROID_16__
        Background = drawable;
        #endif
      } else {
        SetBackgroundDrawable (drawable);
      }
    }

    private int MarginBottom {
      get {
        var layoutParams = LayoutParameters as ViewGroup.MarginLayoutParams;
        if (layoutParams != null) {
          return layoutParams.BottomMargin;
        }
        return 0;
      }
    }

    public bool OnPreDraw ()
    {
      var currentVto = ViewTreeObserver;
      if (currentVto.IsAlive) {
        currentVto.RemoveOnPreDrawListener (this);
      }
      Toggle (Visible, lastToggleAnimate, true);
      return true;
    }


    private bool HasLollipopApi {
      get { return (int)Build.VERSION.SdkInt >= 21; }
    }

    private bool HasJellyBeanApi {
      get { return (int)Build.VERSION.SdkInt >= 16; }
    }


    private bool HasHoneycombApi {
      get { return (int)Build.VERSION.SdkInt >= 11; }
    }

    private static int DarkenColor (int color)
    {
      var hsv = new float[3];
      Color.ColorToHSV (new Color (color), hsv);
      hsv [2] *= 0.9f;
      return Color.HSVToColor (hsv);
    }

    private static int LightenColor (int color)
    {
      var hsv = new float[3];
      Color.ColorToHSV (new Color (color), hsv);
      hsv [2] *= 1.1f;
      return Color.HSVToColor (hsv);
    }

    public void AttachToListView (AbsListView listView, IScrollDirectorListener scrollDirectionListener = null, AbsListView.IOnScrollListener onScrollListener = null)
    {
      var scrollDetector = new AbsListViewScrollDetectorImpl (this);
      scrollDetector.ScrollDirectionListener = scrollDirectionListener;
      scrollDetector.OnScrollListener = onScrollListener;
      scrollDetector.ListView = listView;
      scrollDetector.ScrollThreshold = scrollThreshold;
      listView.SetOnScrollListener (scrollDetector);
    }

    public void AttachToRecyclerView (RecyclerView recyclerView, IScrollDirectorListener scrollDirectionListener = null, RecyclerView.OnScrollListener onScrollListener = null)
    {
      var scrollDetector = new RecyclerViewScrollDetectorImpl (this);
      scrollDetector.ScrollDirectionListener = scrollDirectionListener;
      scrollDetector.OnScrollListener = onScrollListener;
      scrollDetector.ScrollThreshold = scrollThreshold;
      recyclerView.SetOnScrollListener (scrollDetector);
    }

    public void AttachToScrollView (ObservableScrollView scrollView, IScrollDirectorListener scrollDirectionListener = null, IOnScrollChangedListener onScrollChangedListener = null)
    {
      var scrollDetector = new ScrollViewScrollDetectorImpl (this);
      scrollDetector.ScrollDirectionListener = scrollDirectionListener;
      scrollDetector.OnScrollChangedListener = onScrollChangedListener;
      scrollDetector.ScrollThreshold = scrollThreshold;
      scrollView.OnScrollChangedListener = scrollDetector;
    }

    internal class AbsListViewScrollDetectorImpl : AbsListViewScrollDetector
    {
      public IScrollDirectorListener ScrollDirectionListener { get; set; }


      FloatingActionButton fab;

      public AbsListViewScrollDetectorImpl (FloatingActionButton fab)
      {
        this.fab = fab;
      }


      
      public override void OnScrollDown ()
      {
        fab.Show ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollDown ();
      }


      public override void OnScrollUp ()
      {
        fab.Hide ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollUp ();
      }
    }

    internal class RecyclerViewScrollDetectorImpl : RecyclerViewScrollDetector
    {
      public IScrollDirectorListener ScrollDirectionListener { get; set; }

      FloatingActionButton fab;

      public RecyclerViewScrollDetectorImpl (FloatingActionButton fab)
      {
        this.fab = fab;
      }

      public override void OnScrollDown ()
      {
        fab.Show ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollDown ();
      }


      public override void OnScrollUp ()
      {
        fab.Hide ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollUp ();
      }
    }

    internal class ScrollViewScrollDetectorImpl : ScrollViewScrollDetector
    {
      public IScrollDirectorListener ScrollDirectionListener { get; set; }

      FloatingActionButton fab;

      public ScrollViewScrollDetectorImpl (FloatingActionButton fab)
      {
        this.fab = fab;
      }

      public override void OnScrollDown ()
      {
        fab.Show ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollDown ();
      }


      public override void OnScrollUp ()
      {
        fab.Hide ();
        if (ScrollDirectionListener != null)
          ScrollDirectionListener.OnScrollUp ();

      }

    }


  }
}