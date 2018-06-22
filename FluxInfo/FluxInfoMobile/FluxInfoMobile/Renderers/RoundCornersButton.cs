using Xamarin.Forms;

namespace FluxInfoMobile.Renderers
{
    public class RoundCornersButton: Button
    {
        public static BindableProperty CornerRadiusProperty
        = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundCornersButton), 0);

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public RoundCornersButton()
        {
            BorderWidth = 2;
            BorderColor = Color.Black;
            BackgroundColor = Color.White;
        }
    }
}
