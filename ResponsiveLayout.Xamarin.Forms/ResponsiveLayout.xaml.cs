using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResponsiveLayout.Xamarin.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsiveLayout : ContentView
    {
        internal enum WidthType
        {
            XS, SM, MD, LG, XL
        }


        public View ContentXS { get; set; }
        public View ContentSM { get; set; }
        public View ContentMD { get; set; }
        public View ContentLG { get; set; }
        public View ContentXL { get; set; }


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
            var w = Application.Current?.MainPage?.Width ?? Width;
            //if ((Grid?.WidthReference ?? ResponsiveGridWidthReferences.MainPage) == ResponsiveGridWidthReferences.Local)
            //    w = Grid.Width;
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

        private void _OnSizeChanged(object sender, EventArgs e)
        {
            var type = _GetWidthType();
            switch (type)
            {
                case WidthType.XS:
                    Content = _TakeFirstNonNull(ContentXS, ContentSM, ContentMD, ContentLG, ContentXL);
                    //Content = ContentXS;
                    break;

                case WidthType.SM:
                    Content = _TakeFirstNonNull(ContentSM, ContentXS, ContentMD, ContentLG, ContentXL);
                    break;

                case WidthType.MD:
                    Content = _TakeFirstNonNull(ContentMD, ContentSM, ContentXS, ContentLG, ContentXL);
                    break;

                case WidthType.LG:
                    Content = _TakeFirstNonNull(ContentLG, ContentMD, ContentSM, ContentXS, ContentXL);
                    break;

                case WidthType.XL:
                    Content = _TakeFirstNonNull(ContentXL, ContentLG, ContentMD, ContentSM, ContentXS);
                    break;
            }
        }


        private T _TakeFirstNonNull<T>(params T[] values) where T : class => values?.FirstOrDefault(x => x != null);
    }
}