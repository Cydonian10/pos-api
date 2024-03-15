using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;




namespace PuntoVenta.Modules.BardCode
{
    [Route("api/bardCode")]
    [ApiController]
    public class BardCodeController : ControllerBase
    {
        [HttpGet]
        public string Generate(string data)
        {

            var qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode bitmapByteQRCode = new BitmapByteQRCode(qrCodeData);

            var bitMap = bitmapByteQRCode.GetGraphic(20);

            using(var ms = new MemoryStream())
            {
                ms.Write(bitMap);
                byte[] byteImage = ms.ToArray();
                return Convert.ToBase64String(byteImage);
            }
        }

    }

}
