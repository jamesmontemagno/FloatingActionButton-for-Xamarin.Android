﻿using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;
using Refractored.Fab;
using AndroidX.AppCompat.App;
using ActionBar = AndroidX.AppCompat.App.ActionBar;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using FloatingActionButton = Refractored.Fab.FloatingActionButton;
using AndroidX.RecyclerView.Widget;

namespace FabSample
{
    [Activity(Label = "FabSample", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : AppCompatActivity, ActionBar.ITabListener, IDialogInterfaceOnClickListener
  {
    int count = 1;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      InitActionBar();
    }

    private void InitActionBar()
    {
      if (SupportActionBar == null)
        return;

      var actionBar = SupportActionBar;
      actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;

      var tab1 = actionBar.NewTab();
      tab1.SetTabListener(this);
      tab1.SetText("ListView");
      actionBar.AddTab(tab1);

      var tab2 = actionBar.NewTab();
      tab2.SetTabListener(this);
      tab2.SetText("RecyclerView");
      actionBar.AddTab(tab2);

      var tab3 = actionBar.NewTab();
      tab3.SetTabListener(this);
      tab3.SetText("ScrollView");
      actionBar.AddTab(tab3);

	  if ((int)Build.VERSION.SdkInt >= 21)
	  {
		var tab4 = actionBar.NewTab();
			  tab4.SetTabListener(this);
			  tab4.SetText("Custom FAB");
			  actionBar.AddTab(tab4);
	  }
	  
    }

    public void OnTabReselected(ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
    {
    }

    public void OnTabSelected(ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
    {
      switch (tab.Text)
      {
        case "ListView":
          ft.Replace(Android.Resource.Id.Content, new ListViewFragment());
          break;
        case "RecyclerView":
          ft.Replace(Android.Resource.Id.Content, new RecyclerViewFragment());
          break;
        case "ScrollView":
          ft.Replace(Android.Resource.Id.Content, new ScrollViewFragment());
          break;
		case "Custom FAB":
		  ft.Replace(Android.Resource.Id.Content, new CustomFABFragment());
		  break;
      }
    }

    public void OnTabUnselected(ActionBar.Tab tab, AndroidX.Fragment.App.FragmentTransaction ft)
    {
      
    }

    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.main, menu);
      return base.OnCreateOptionsMenu(menu);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      if(item.ItemId == Resource.Id.about)
      {
        var text = (TextView)LayoutInflater.Inflate(Resource.Layout.about_view, null);
        text.TextFormatted = (Html.FromHtml(GetString(Resource.String.about_body)));
        new AlertDialog.Builder(this)
        .SetTitle(Resource.String.about)
        .SetView(text)
        .SetInverseBackgroundForced(true)
        .SetPositiveButton(Android.Resource.String.Ok, this).Create().Show();
      }
      return base.OnOptionsItemSelected(item);
    }

    public void OnClick(IDialogInterface dialog, int which)
    {
      dialog.Dismiss();
    }
  }

  public class ListViewFragment : AndroidX.Fragment.App.Fragment, IScrollDirectorListener, AbsListView.IOnScrollListener
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var root = inflater.Inflate(Resource.Layout.fragment_listview, container, false);

      var list = root.FindViewById<ListView>(Android.Resource.Id.List);
      var adapter = new ListViewAdapter(Activity, Resources.GetStringArray(Resource.Array.countries));
      list.Adapter = adapter;

      var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
      fab.AttachToListView(list, this, this);
      fab.Click += (sender, args) =>
        {
          Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
        };
      return root;
    }

    public void OnScrollDown()
    {
      Console.WriteLine("ListViewFragment: OnScrollDown");
    }

    public void OnScrollUp()
    {
      Console.WriteLine("ListViewFragment: OnScrollUp");
    }

    public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
    {
      Console.WriteLine("ListViewFragment: OnScroll");
    }

    public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
    {
      Console.WriteLine("ListViewFragment: OnScrollChanged");
    }
  }

  public class RecyclerViewFragment : AndroidX.Fragment.App.Fragment, IScrollDirectorListener
  {
    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var root = inflater.Inflate(Resource.Layout.fragment_recyclerview, container, false);

      var recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recycler_view);
      recyclerView.HasFixedSize = true;
      recyclerView.SetItemAnimator(new DefaultItemAnimator());
      recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
      recyclerView.AddItemDecoration(new DividerItemDecoration(Activity, DividerItemDecoration.VerticalList));

      var adapter = new RecyclerViewAdapter(Activity, Resources.GetStringArray(Resource.Array.countries));
      recyclerView.SetAdapter(adapter);

      var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
      fab.AttachToRecyclerView(recyclerView, this);
      fab.Size = FabSize.Mini;
      fab.Enabled = false;
      fab.Click += (sender, args) =>
      {
        Toast.MakeText(Activity, "FAB Clicked!", ToastLength.Short).Show();
      };
      return root;
    }

    public void OnScrollDown()
    {
      Console.WriteLine("RecyclerViewFragment: OnScrollDown");
    }

    public void OnScrollUp()
    {
      Console.WriteLine("RecyclerViewFragment: OnScrollUp");
    }
  }

  public class ScrollViewFragment : AndroidX.Fragment.App.Fragment, IOnScrollChangedListener, IScrollDirectorListener
  {

    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
      var root = inflater.Inflate(Resource.Layout.fragment_scrollview, container, false);

      var scrollView = root.FindViewById<ObservableScrollView>(Resource.Id.scroll_view);
      var list = root.FindViewById<LinearLayout>(Resource.Id.list);

      var countries = Resources.GetStringArray(Resource.Array.countries);
      foreach(var country in countries)
      {
        var textView = (TextView)inflater.Inflate(Resource.Layout.list_item, container, false);
        var values = country.Split(',');
        var name = values[0];
        var flagResId = Resources.GetIdentifier(values[1], "drawable", Activity.PackageName);
        textView.Text = name;
        textView.SetCompoundDrawablesWithIntrinsicBounds(flagResId, 0, 0, 0);
        list.AddView(textView);
      }

      var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
      fab.AttachToScrollView(scrollView, this, this);
      return root;
    }
    public void OnScrollDown()
    {
      Console.WriteLine("ScrollViewFragment: OnScrollDown");
    }

    public void OnScrollUp()
    {
      Console.WriteLine("ScrollViewFragment: OnScrollUp");
    }

    public void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt)
    {
      Console.WriteLine("ScrollViewFragment: OnScrollChanged");
    }
  }

  public class CustomFABFragment : AndroidX.Fragment.App.Fragment, IScrollDirectorListener, AbsListView.IOnScrollListener
  {
	  public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	  {
		  var root = inflater.Inflate(Resource.Layout.fragment_listview_custom_fab, container, false);

		  var list = root.FindViewById<ListView>(Android.Resource.Id.List);
		  var adapter = new ListViewAdapter(Activity, Resources.GetStringArray(Resource.Array.countries));
		  list.Adapter = adapter;

		  var fab = root.FindViewById<CustomFAB>(Resource.Id.fab);
		  fab.AttachToListView(list, this, this);
		  int x = 0;
		  fab.Click += (sender, args) =>
		  {
			  fab.SelectedIndex = ++x % 2;
		  };
		  return root;
	  }

	  public void OnScrollDown()
	  {
		  Console.WriteLine("ListViewFragment: OnScrollDown");
	  }

	  public void OnScrollUp()
	  {
		  Console.WriteLine("ListViewFragment: OnScrollUp");
	  }

	  public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
	  {
		  Console.WriteLine("ListViewFragment: OnScroll");
	  }

	  public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
	  {
		  Console.WriteLine("ListViewFragment: OnScrollChanged");
	  }
  }
}

