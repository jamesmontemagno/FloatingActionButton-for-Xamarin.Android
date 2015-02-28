# Getting Started with Floating Action Button


**1)** Add the Component to your Project


**2)** Add the ``com.refractored.fab.FloatingActionButton`` to your layout XML file. The button should be placed in the bottom right corner of the screen. The width and height of the floating action button are hardcoded to **56dp** for the normal and **40dp** for the mini button as specified in the [guidlines].

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

**3)** Attach the FAB to ``AbsListView``, ``RecyclerView`` or ``ScrollView`` :

```csharp
var listView = FindViewById<ListView>(Android.Resource.Id.list);
var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
fab.AttachToListView(listView);
```

Check the sample project to see how to use custom listeners if you need to track scroll events.

**4)** Add the namespace ``xmlns:fab="http://schemas.android.com/apk/res-auto"`` to your layout file.

+ Set the button type (normal or mini) via the ``fab_type`` xml attribute (default is normal):

    ```xml
    fab:fab_type="mini"
    ```
    or
    ```csharp
    fab.Size = FabSize.Mini;
    ```
+ Set the normal and pressed colors via the xml attributes:

    ```xml
    fab:fab_colorNormal="@color/primary"
    fab:fab_colorPressed="@color/primary_pressed"
    ```
    or
    ```csharp
    fab.ColorNormal = Resources.GetColor(Resource.Color.primary);
    fab.ColorPressed = Resources.GetColor(Resource.Color.primary_pressed);
    ```
    
+ Enable/disable the button shadow with the ``fab_shadow`` xml attribite (it's enabled by default):

    ```xml
    fab:fab_shadow="false"
    ```
    or
    ```csharp
    fab..HasShadow = false;
    ```
    
+ Show/hide the button expliciltly:
    
    ```csharp
    fab.Show();
    fab.Hide();
    
    fab.Show(false); // Show without an animation
    fab.Hide(false); // Hide without an animation
    ```
    
+ Specify the ripple color for API 21+:

    ```xml
    fab:fab_colorRipple="@color/ripple"
    ```

    or
   ```java
   fab.ColorRipple = Resources.GetColor(Resource.Color.ripple);
   ```

**5)** Set an icon for the ``FloatingActionButton`` using ``android:src`` xml attribute. Use drawables of size **24dp** as specified by [guidlines]. Icons of desired size can be generated with [Android Asset Studio].
