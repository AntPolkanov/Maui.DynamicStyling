using MauiHackDay.DynamicStyling.Resources.Styles.ColorSchemes;
using System.Diagnostics;

namespace MauiHackDay.DynamicStyling
{
    public partial class MainPage : ContentPage
    {
        int count = 1;

        public MainPage()
        {
            InitializeComponent();
            SetColorScheme(new PurpleScheme());
            Application.Current.RequestedThemeChanged += HandleThemeChange;
        }

        private void HandleThemeChange(object? sender, AppThemeChangedEventArgs e)
        {
            var currentTheme = Application.Current.RequestedTheme;
            Trace.TraceInformation($"Theme has changed to {currentTheme}");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Application.Current.RequestedThemeChanged -= HandleThemeChange;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count % 2 == 1)
            {
                CounterBtn.Text = $"Switch color scheme. Current = Purple";
                SetColorScheme(new PurpleScheme());
            }
            else
            {
                CounterBtn.Text = $"Switch color scheme. Current = Red";
                SetColorScheme(new RedScheme());
            }

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void SetColorScheme(ResourceDictionary theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                var toBeRemoved = new List<ResourceDictionary>();
                foreach (var dict in mergedDictionaries)
                {
                    if (dict is PurpleScheme || dict is RedScheme)
                    {
                        toBeRemoved.Add(dict);
                    }
                }

                foreach (var dict in toBeRemoved)
                {
                    mergedDictionaries.Remove(dict);
                }
                mergedDictionaries.Add(theme);
            }
        }
    }

}
