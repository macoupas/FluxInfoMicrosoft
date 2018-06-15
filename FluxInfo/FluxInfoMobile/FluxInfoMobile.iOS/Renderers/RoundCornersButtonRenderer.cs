using FluxInfoMobile.iOS.Renderers;
using FluxInfoMobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundCornersButton), typeof(RoundCornersButtonRenderer))]
namespace FluxInfoMobile.iOS.Renderers
{
    public class RoundCornersButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);


            if (Control != null)
            {
                Control.Layer.CornerRadius = 22;
                Control.ClipsToBounds = true;
            }
        }
    }
}
