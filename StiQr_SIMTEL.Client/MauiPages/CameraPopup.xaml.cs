using CommunityToolkit.Maui.Views;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using ZXing.Net.Maui;
using static Microsoft.AspNetCore.Components.NavigationManager;
namespace StiQr_SIMTEL.Client.MauiPages;

public partial class CameraPopup
{
    public CameraPopup()
	{
		InitializeComponent();
        
	}
 
    private void ScannerDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            CloseAsync($"{e.Results[0].Value} {e.Results[0].Format}");
        });
        
    }
}