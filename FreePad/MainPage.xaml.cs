using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Input.Inking.Core;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FreePad
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);
            Window.Current.SetTitleBar(AppTitleBar);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(255, 136, 138, 142);
            titleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(255, 32, 34, 37);
            titleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(255, 176, 177, 179);
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(255, 40, 43, 46);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(255, 205, 205, 206);
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(255, 43, 46, 50);
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Color.FromArgb(255, 136, 138, 142);
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(255, 32, 34, 37);
        }


        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Get the size of the caption controls area and back button 
            // (returned in logical pixels), and move your content around as necessary.
            LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
            //TitleBarButton.Margin = new Thickness(0, 0, coreTitleBar.SystemOverlayRightInset, 0);

            // Update title bar control size as needed to account for system size changes.
            AppTitleBar.Height = coreTitleBar.Height;
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
