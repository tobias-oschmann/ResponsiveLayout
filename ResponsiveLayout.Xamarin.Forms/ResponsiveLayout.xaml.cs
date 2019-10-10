using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResponsiveLayout.Xamarin.Forms
{
    public enum ResponsiveLayoutMode
    {
        Width, Idiom, Platform, Orientation
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsiveLayout : ContentView
    {
        internal enum WidthType
        {
            XS, SM, MD, LG, XL
        }

        private WidthType? _prevWidthType;
        private DataTemplate _prevTemplate;

        private Dictionary<DataTemplate, View> _contentCache;


        public static readonly BindableProperty LayoutModeProperty =
            BindableProperty.Create(nameof(LayoutMode), typeof(ResponsiveLayoutMode), typeof(ResponsiveLayout), defaultValue: ResponsiveLayoutMode.Width, defaultBindingMode: BindingMode.OneWay, propertyChanged: _OnLayoutModeChanged);

        private static void _OnLayoutModeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ResponsiveLayout rl)
                rl._RefreshLayout();
        }

        public ResponsiveLayoutMode LayoutMode
        {
            get => (ResponsiveLayoutMode)GetValue(LayoutModeProperty);
            set => SetValue(LayoutModeProperty, value);
        }

        public static readonly BindableProperty EnableContentCachingProperty =
            BindableProperty.Create(nameof(EnableContentCaching), typeof(bool), typeof(ResponsiveLayout), defaultValue: false, defaultBindingMode: BindingMode.OneWay, propertyChanged: _OnEnableContentCachingChanged);

        private static void _OnEnableContentCachingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ResponsiveLayout rl))
                return;
            if (rl.EnableContentCaching)
                rl._contentCache = new Dictionary<DataTemplate, View>();
            else
            {
                rl._contentCache.Clear();
                rl._contentCache = null;
            }
        }

        public bool EnableContentCaching
        {
            get => (bool)GetValue(EnableContentCachingProperty);
            set => SetValue(EnableContentCachingProperty, value);
        }


        public ResponsiveLayout()
        {
            InitializeComponent();
            SizeChanged += _OnSizeChanged;
        }
        ~ResponsiveLayout()
        {
            SizeChanged -= _OnSizeChanged;
        }

        private WidthType _GetWidthType()
        {
            var w = Application.Current?.MainPage?.Width ?? 0;
            if (w >= 1200)
                return WidthType.XL;
            else if (w >= 992)
                return WidthType.LG;
            else if (w >= 768)
                return WidthType.MD;
            else if (w > 576)
                return WidthType.SM;
            return WidthType.XS;
        }

        private DataTemplate _GetTemplate()
        {
            switch (LayoutMode)
            {
                case ResponsiveLayoutMode.Width:
                    var type = _GetWidthType();
                    if (type == _prevWidthType)
                        return _prevTemplate;
                    return _GetTemplateForWidth(type);

                case ResponsiveLayoutMode.Idiom:
                    return _GetTemplateForIdiom(Device.Idiom);

                case ResponsiveLayoutMode.Platform:
                    return _GetTemplateForPlatform(Device.RuntimePlatform);

                case ResponsiveLayoutMode.Orientation:
                    return _GetTemplateForOrientation((Application.Current?.MainPage?.Height ?? 0) > (Application.Current?.MainPage?.Width ?? 0));
            }
            return null;
        }

        private void _OnSizeChanged(object sender, EventArgs e)
        {
            _RefreshLayout();
        }

        private void _RefreshLayout()
        {
            var template = _GetTemplate();
            if (template == _prevTemplate)
                return;
            _prevTemplate = template;
            if (template == null)
            {
                Content = null;
                return;
            }

            View content = null;
            if (EnableContentCaching && _contentCache.ContainsKey(template))
                    content = _contentCache[template];
            if (content == null)
            {
                content = (View)template?.CreateContent();
                if (EnableContentCaching)
                    _contentCache[template] = content;
            }
            Content = content;
        }


        private T _TakeFirstNonNull<T>(params T[] values) where T : class => values?.FirstOrDefault(x => x != null);
    }
}