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

namespace FabSample
{
  public class ViewHolder : Java.Lang.Object
  {
    public TextView TextView { get; set; }
  }
  public class ListViewAdapter : BaseAdapter
  {
    private Context context;
    private string[] data;

    public ListViewAdapter(Context context, string[] data)
    {
      this.context = context;
      this.data = data;
    }

    public override int Count
    {
      get { return data.Length; }
    }

    public override Java.Lang.Object GetItem(int position)
    {
      return data[position];
    }

    public override long GetItemId(int position)
    {
      return position;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
      ViewHolder viewHolder;
      if(convertView == null)
      {
        convertView = LayoutInflater.From(context).Inflate(Resource.Layout.list_item, parent, false);
        viewHolder = new ViewHolder();
        viewHolder.TextView = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
        convertView.Tag = viewHolder;
      }
      else
      {
        viewHolder = (ViewHolder)convertView.Tag;
      }

      var values = data[position].Split(',');
      var name = values[0];
      var flagresId = context.Resources.GetIdentifier(values[1], "drawable", context.PackageName);
      viewHolder.TextView.Text = name;
      viewHolder.TextView.SetCompoundDrawablesWithIntrinsicBounds(flagresId, 0, 0, 0);
      return convertView;
    }
  }
}