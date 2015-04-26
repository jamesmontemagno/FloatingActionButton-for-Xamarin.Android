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
using Android.Support.V7.Widget;

namespace FabSample
{
  public class RecyclerViewAdapter : RecyclerView.Adapter
  {
    private Context context;
    private string[] data;
    public RecyclerViewAdapter(Context context, string[] data)
    {
      this.context = context;
      this.data = data;
    }


    
    public class ViewHolder : RecyclerView.ViewHolder
    {
      public TextView TextView { get; set; }
      public ViewHolder(TextView v) : base(v)
      {
        TextView = v;
      }
    }

    public override int ItemCount
    {
      get { return data.Length; }
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
      var values = data[position].Split(',');
      var name = values[0];
      var flagResId = context.Resources.GetIdentifier(values[1], "drawable", context.PackageName);
      ((ViewHolder)holder).TextView.Text = name;
      ((ViewHolder)holder).TextView.SetCompoundDrawablesWithIntrinsicBounds(flagResId, 0, 0, 0);
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
      var text = (TextView)LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item, parent, false);
      return new ViewHolder(text);
    }
  }
}