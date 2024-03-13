namespace OpenCRM.Access
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
                AutoRotate = true,
                Multiple = true   
            };

            barcodeReader.BarcodesDetected += barcodeReader_BarcodesDetected;

        }

        private void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results?.FirstOrDefault();
            
            if (first is null)
            {
                return;
            }

            Dispatcher.DispatchAsync(async () =>
            {
                await DisplayAlert("Barcode detected: ", first.Value, "OK");
            });
        }

    }
}