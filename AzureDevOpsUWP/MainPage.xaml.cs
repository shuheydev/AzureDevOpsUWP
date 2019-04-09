using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps.OfflineMaps;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace AzureDevOpsUWP
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            WebView_Main.Navigate(new Uri("https://login.microsoftonline.com/common/oauth2/authorize?client_id=499b84ac-1321-427f-aa17-267ca6975798&site_id=501454&response_mode=form_post&response_type=code+id_token&redirect_uri=https:%2f%2fapp.vssps.visualstudio.com%2f_signedin&nonce=3c0de875-6638-4ce1-a31e-365881a2baab&state=realm%3dapp.vsaex.visualstudio.com%26reply_to%3dhttps%253A%252F%252Fapp.vsaex.visualstudio.com%252Fprofile%252Faccount%253FacquisitionId%253D35900e2d-3cec-497e-97da-867d13652309%2526campaign%253Do~msft~vscom~product-vsts-hero~464%2526account%253Dfirst%2526mkt%253Den-us%2526wt.mc_id%253Do~msft~vscom~product-vsts-hero~464%26ht%3d3%26mkt%3den-US%26nonce%3d3c0de875-6638-4ce1-a31e-365881a2baab&resource=https:%2f%2fmanagement.core.windows.net%2f&cid=3c0de875-6638-4ce1-a31e-365881a2baab&wsucxt=1&mkt=ja-JP&sso_nonce=AQABAAAAAADXzZ3ifr-GRbDT45zNSEFEsYY6q90U5KwXzGFYeJyDZwbd_REKvFUkBX3Tg8uZwXQ-5123-loCqLmkoVbT0C160hMmlraosSwahXgL_5JR-yAA&client-request-id=3c0de875-6638-4ce1-a31e-365881a2baab&mscrid=3c0de875-6638-4ce1-a31e-365881a2baab\r"));

            SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
            {
                if(WebView_Main.CanGoBack)
                {
                    WebView_Main.GoBack();
                    args.Handled = true;
                }
            };
        }

        private async void WebView_Main_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                await WebView_Main.InvokeScriptAsync("eval", new string[] { SetScrollbarScript });
            }
            catch (Exception)
            {
                WebView_Main.NavigateToString("Offline.");
            }
        }

        private string SetScrollbarScript = @"
            function setScrollbar()
            {
                document.body.style.msOverflowStyle='scrollbar';
            }
            setScrollbar();           
        ";
    }
}
