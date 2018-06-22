using Xamarin.Forms;

namespace FluxInfoMobile.Renderers
{
    /// <summary>
    /// Extended editor control.
    ///     * Etend le composant Editor si le text est trop long
    ///     * Arrondir les bords de l'Editor (Visible quand le fond n'est pas de même couleur que l'Editor :P)
    /// </summary>
    public class ExtendedEditorControl : Editor
    {
        public static BindableProperty HasRoundedCornerProperty
        = BindableProperty.Create(nameof(HasRoundedCorner), typeof(bool), typeof(ExtendedEditorControl), false);

        public static BindableProperty IsExpandableProperty
        = BindableProperty.Create(nameof(IsExpandable), typeof(bool), typeof(ExtendedEditorControl), false);

        public bool IsExpandable
        {
            get { return (bool)GetValue(IsExpandableProperty); }
            set { SetValue(IsExpandableProperty, value); }
        }
        public bool HasRoundedCorner
        {
            get { return (bool)GetValue(HasRoundedCornerProperty); }
            set { SetValue(HasRoundedCornerProperty, value); }
        }

        // Constructeur de la classe
        // Abonnement sur l'évennement TextChanged
        public ExtendedEditorControl()
        {
            TextChanged += OnTextChanged;
        }

        // Destructeur de la classe
        // Désabonnement sur l'évennement TextChanged
        ~ExtendedEditorControl()
        {
            TextChanged -= OnTextChanged;
        }

        // Si la property IsExpandable est à vrai,
        // on appelle InvalidateMesure() pour redimensionner l'Editor si le text est trop long
        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsExpandable) InvalidateMeasure();
        }
    }
}
