# Floating Action Button Details

Android floating action button which reacts on scrolling events. Becomes visible when an attached target is scrolled up and invisible when scrolled down.

![FAB Sample Material](https://raw.githubusercontent.com/jamesmontemagno/FloatingActionButton-for-Xamarin.Android/master/art/demo.gif)

```xml
<FrameLayout xmlns:android="http://schemas.android.com/apk/res/android"
             xmlns:fab="http://schemas.android.com/apk/res-auto"
             android:layout_width="match_parent"
             android:layout_height="match_parent">

    <ListView
            android:id="@android:id/list"
            android:layout_width="match_parent"
            android:layout_height="match_parent" />

    <com.refractored.fab.FloatingActionButton
            android:id="@+id/fab"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:layout_gravity="bottom|right"
            android:layout_margin="16dp"
            android:src="@drawable/ic_action_content_new"
            fab:fab_colorNormal="@color/primary"
            fab:fab_colorPressed="@color/primary_pressed"
            fab:fab_colorRipple="@color/ripple" />
</FrameLayout>
```

**This library requires AppCompat v21+**