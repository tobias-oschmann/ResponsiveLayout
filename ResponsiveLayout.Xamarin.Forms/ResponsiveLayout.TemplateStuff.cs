using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ResponsiveLayout.Xamarin.Forms
{
    public partial class ResponsiveLayout
    {
        public DataTemplate ContentTemplateXS { get; set; }
        public DataTemplate ContentTemplateSM { get; set; }
        public DataTemplate ContentTemplateMD { get; set; }
        public DataTemplate ContentTemplateLG { get; set; }
        public DataTemplate ContentTemplateXL { get; set; }

        public DataTemplate ContentTemplateIdiomDefault { get; set; }
        public DataTemplate ContentTemplateIdiomPhone { get; set; }
        public DataTemplate ContentTemplateIdiomTablet { get; set; }
        public DataTemplate ContentTemplateIdiomDesktop { get; set; }
        public DataTemplate ContentTemplateIdiomTV { get; set; }
        public DataTemplate ContentTemplateIdiomWatch { get; set; }

        public DataTemplate ContentTemplatePlatformDefault { get; set; }
        public DataTemplate ContentTemplatePlatformIOS { get; set; }
        public DataTemplate ContentTemplatePlatformAndroid { get; set; }
        public DataTemplate ContentTemplatePlatformUWP { get; set; }
        public DataTemplate ContentTemplatePlatformMacOS { get; set; }
        public DataTemplate ContentTemplatePlatformGTK { get; set; }
        public DataTemplate ContentTemplatePlatformTizen { get; set; }
        public DataTemplate ContentTemplatePlatformWPF { get; set; }

        public DataTemplate ContentTemplatePortrait { get; set; }
        public DataTemplate ContentTemplateLandscape { get; set; }


        private DataTemplate _GetTemplateForWidth(WidthType type)
        {
            switch (type)
            {
                case WidthType.XS:
                    return _TakeFirstNonNull(ContentTemplateXS, ContentTemplateSM, ContentTemplateMD, ContentTemplateLG, ContentTemplateXL);

                case WidthType.SM:
                    return _TakeFirstNonNull(ContentTemplateSM, ContentTemplateXS, ContentTemplateMD, ContentTemplateLG, ContentTemplateXL);

                case WidthType.MD:
                    return _TakeFirstNonNull(ContentTemplateMD, ContentTemplateSM, ContentTemplateXS, ContentTemplateLG, ContentTemplateXL);

                case WidthType.LG:
                    return _TakeFirstNonNull(ContentTemplateLG, ContentTemplateMD, ContentTemplateSM, ContentTemplateXS, ContentTemplateXL);

                case WidthType.XL:
                    return _TakeFirstNonNull(ContentTemplateXL, ContentTemplateLG, ContentTemplateMD, ContentTemplateSM, ContentTemplateXS);
            }
            return null;
        }

        private DataTemplate _GetTemplateForIdiom(TargetIdiom idiom)
        {
            switch (idiom)
            {
                case TargetIdiom.Desktop:
                    return _TakeFirstNonNull(ContentTemplateIdiomDesktop, ContentTemplateIdiomDefault);

                case TargetIdiom.Phone:
                    return _TakeFirstNonNull(ContentTemplateIdiomPhone, ContentTemplateIdiomDefault);

                case TargetIdiom.Tablet:
                    return _TakeFirstNonNull(ContentTemplateIdiomTablet, ContentTemplateIdiomDefault);

                case TargetIdiom.TV:
                    return _TakeFirstNonNull(ContentTemplateIdiomTV, ContentTemplateIdiomDefault);

                case TargetIdiom.Watch:
                    return _TakeFirstNonNull(ContentTemplateIdiomWatch, ContentTemplateIdiomDefault);
            }
            return ContentTemplateIdiomDefault;
        }

        private DataTemplate _GetTemplateForPlatform(string platform)
        {
            switch (platform)
            {
                case Device.iOS:
                    return _TakeFirstNonNull(ContentTemplatePlatformIOS, ContentTemplatePlatformDefault);

                case Device.Android:
                    return _TakeFirstNonNull(ContentTemplatePlatformAndroid, ContentTemplatePlatformDefault);

                case Device.UWP:
                    return _TakeFirstNonNull(ContentTemplatePlatformUWP, ContentTemplatePlatformDefault);

                case Device.macOS:
                    return _TakeFirstNonNull(ContentTemplatePlatformMacOS, ContentTemplatePlatformDefault);

                case Device.GTK:
                    return _TakeFirstNonNull(ContentTemplatePlatformGTK, ContentTemplatePlatformDefault);

                case Device.Tizen:
                    return _TakeFirstNonNull(ContentTemplatePlatformTizen, ContentTemplatePlatformDefault);

                case Device.WPF:
                    return _TakeFirstNonNull(ContentTemplatePlatformWPF, ContentTemplatePlatformDefault);
            }
            return ContentTemplateIdiomDefault;
        }

        private DataTemplate _GetTemplateForOrientation(bool portrait)
        {
            if (portrait)
                return _TakeFirstNonNull(ContentTemplatePortrait, ContentTemplateLandscape);
            else
                return _TakeFirstNonNull(ContentTemplateLandscape, ContentTemplatePortrait);
        }
    }
}
