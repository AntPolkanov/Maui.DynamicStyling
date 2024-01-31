using System.Diagnostics;

namespace MauiHackDay.DynamicStyling
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
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

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
