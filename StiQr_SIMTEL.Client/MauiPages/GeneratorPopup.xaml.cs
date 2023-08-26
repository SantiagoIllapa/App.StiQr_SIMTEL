using ZXing.Net.Maui.Controls;

namespace StiQr_SIMTEL.Client.MauiPages;

public partial class GeneratorPopup
{
	
	public GeneratorPopup(string url,string plate)
	{
		
		InitializeComponent();
		labelPlate.Text = plate;
        generatorLab.Value = url;
    }

}