using System;
using Xamarin.Forms;

using System.Windows.Input;

namespace Refractored.FabControl
{
    /// <summary>
    /// </summary>
    public class FloatingActionButtonView : View
    {
        /// <summary>
        /// Gets the image name property
        /// </summary>
        public static readonly BindableProperty ImageNameProperty = 
            BindableProperty.Create(nameof(ImageName),
                typeof(string),
                typeof(FloatingActionButtonView), 
                string.Empty);


        /// <summary>
        /// Gets or sets the image name from the source (ic_icon.png) for instance
        /// </summary>
        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        /// <summary>
        /// Gets the color normal property
        /// </summary>
        public static readonly BindableProperty ColorNormalProperty =
            BindableProperty.Create(nameof(ColorNormal),
                typeof(Color),
                typeof(FloatingActionButtonView),
                Color.White);

        /// <summary>
        /// Gets or sets the color of the button
        /// </summary>
        public Color ColorNormal
        {
            get { return (Color)GetValue(ColorNormalProperty); }
            set { SetValue(ColorNormalProperty, value); }
        }

        /// <summary>
        /// Gets the color pressed property
        /// </summary>
        public static readonly BindableProperty ColorPressedProperty =
            BindableProperty.Create(nameof(ColorPressed),
                typeof(Color),
                typeof(FloatingActionButtonView),
                Color.White);


        /// <summary>
        /// Gets or sets the color pressed property
        /// </summary>
        public Color ColorPressed
        {
            get { return (Color)GetValue(ColorPressedProperty); }
            set { SetValue(ColorPressedProperty, value); }
        }

        /// <summary>
        /// Gets the ripple color property
        /// </summary>
        public static readonly BindableProperty ColorRippleProperty =
            BindableProperty.Create(nameof(ColorRipple),
                typeof(Color),
                typeof(FloatingActionButtonView),
                Color.White);

        /// <summary>
        /// Gets or sets the ripple color of the floating action button
        /// </summary>
        public Color ColorRipple
        {
            get { return (Color)GetValue(ColorRippleProperty); }
            set { SetValue(ColorRippleProperty, value); }
        }

        /// <summary>
        /// Gets the size property of the floating action button
        /// </summary>
        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size),
                typeof(FloatingActionButtonSize),
                typeof(FloatingActionButtonView),
                FloatingActionButtonSize.Normal);

       /// <summary>
       /// Gets or sets the floating action button size
       /// </summary>
       public FloatingActionButtonSize Size
        {
            get { return (FloatingActionButtonSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// Gets the has shadow property
        /// </summary>
        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow),
                typeof(bool),
                typeof(FloatingActionButtonView),
                true);

        /// <summary>
        /// Gets or sets the has shadow property
        /// </summary>
        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        /// <summary>
        /// Gets the command property
        /// </summary>
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(FloatingActionButtonView), 
            null,
            propertyChanged: (bo, o, n) => ((FloatingActionButtonView)bo).OnCommandChanged());


        /// <summary>
        /// Gets the command 
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = 
            BindableProperty.Create(nameof(CommandParameter),
                typeof(object), 
                typeof(FloatingActionButtonView), 
                null,
                propertyChanged: (bindable, oldvalue, newvalue) => ((FloatingActionButtonView)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

        /// <summary>
        /// Gets or sets the command to execute when clicked
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command parameter
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Executes if enabled or not based on can execute changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = Command;
            if (cmd != null)
                IsEnabled = cmd.CanExecute(CommandParameter);
        }


        /// <summary>
        /// Show Hide Delegate
        /// </summary>
        /// <param name="animate"></param>
        public delegate void ShowHideDelegate(bool animate = true);

        /// <summary>
        /// Attach to list view delegate
        /// </summary>
        /// <param name="listView"></param>
        public delegate void AttachToListViewDelegate(ListView listView);

        /// <summary>
        /// Show the control
        /// </summary>
        public ShowHideDelegate Show { get; set; }

        /// <summary>
        /// Hide the control
        /// </summary>
        public ShowHideDelegate Hide { get; set; }

        /// <summary>
        /// Action to call when clicked
        /// </summary>
        public Action<object, EventArgs> Clicked { get; set; }

        void OnCommandChanged()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
                IsEnabled = true;
        }
    }
}
