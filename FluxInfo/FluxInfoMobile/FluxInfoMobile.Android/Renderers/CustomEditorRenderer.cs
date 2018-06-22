using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using FluxInfoMobile.Droid.Renderers;
using FluxInfoMobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEditorControl), typeof(CustomEditorRenderer))]
namespace FluxInfoMobile.Droid.Renderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        bool initial = true;
        Drawable originalBackground;

        public CustomEditorRenderer(Context context) : base(context)
        {}

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (initial)
                {
                    originalBackground = Control.Background;
                    initial = false;
                }

            }

            if (e.NewElement != null)
            {
                var customControl = (ExtendedEditorControl)Element;
                if (customControl.HasRoundedCorner)
                {
                    ApplyBorder();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var customControl = (ExtendedEditorControl)Element;

            if (ExtendedEditorControl.HasRoundedCornerProperty.PropertyName == e.PropertyName)
            {
                if (customControl.HasRoundedCorner)
                {
                    ApplyBorder();
                }
                else
                {
                    this.Control.Background = originalBackground;
                }
            }
        }

        void ApplyBorder()
        {
            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(10);
            gd.SetStroke(2, Color.Black.ToAndroid());
            this.Control.Background = gd;
        }
    }
}