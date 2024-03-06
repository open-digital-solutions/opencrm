using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.QRCode;
using OpenCRM.Core.Web.Models;
using QRCoder;
using System.Drawing;

namespace OpenCRM.Web.QrCode
{
    public class QrModel : PageModel
    {
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();
        public string? QrCode { get; set; }

        public QrModel()
        {
            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Qr",
                Page = "Qr",
                Url = "/Qr"
            });
        }
        public void OnPost()
        {
            string texto = Request.Form["texto"];

            if (!string.IsNullOrEmpty(texto))
            {
                /* QRCodeGenerator qrGenerator = new QRCodeGenerator();
                 QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
                 PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                 byte[] qrCodeImage = qrCode.GetGraphic(20);
                 QrCode = Convert.ToBase64String(qrCodeImage);*/

                Bitmap qrCodeImage = QRCodeService.GenerateQRCodeColored(texto);
                byte[] qrCodeBytes = QRCodeService.BitmapToBytes(qrCodeImage);
                QrCode = Convert.ToBase64String(qrCodeBytes);
            }
        }
    }
}
