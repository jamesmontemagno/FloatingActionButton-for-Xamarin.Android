using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics.Drawables;

namespace FabSample
{
	[Register("com.refractored.fab.CustomFloatingActionButton")]
	public class CustomFAB : com.refractored.fab.FloatingActionButton
	{
		

		public CustomFAB (IntPtr handle, Android.Runtime.JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public CustomFAB(Context context)
			: this(context, null)
		{
		}

		public CustomFAB(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
		}

		public CustomFAB(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
		}

		private int selectedIndex;

		public int SelectedIndex
		{
			get
			{
				return selectedIndex;
			}
			set
			{
				if (value < 0 || value > 2)
					throw new ArgumentOutOfRangeException("value");
				JumpDrawablesToCurrentState();
				selectedIndex = value;
				RefreshDrawableState();
				Invalidate();
			}
		}


		public override int[] OnCreateDrawableState(int extraSpace)
		{
			int[] states = base.OnCreateDrawableState(extraSpace+1);
			switch (selectedIndex)
			{
				case 0:
					return states;

				case 1:
					return MergeDrawableStates(states, new int[] { Android.Resource.Attribute.StateLast });

				default:
					return states;
			}
		}
	}
}