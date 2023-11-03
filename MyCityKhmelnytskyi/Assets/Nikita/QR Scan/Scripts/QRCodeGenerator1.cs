using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using ZXing.QrCode;
using UnityEngine.UI;


public class QRCodeGenerator1 : MonoBehaviour
{
    [SerializeField] private RawImage rawImageReciever;
    [SerializeField] private TMP_InputField inputField;

    private Texture2D encodedTexture;

    private void Start()
    {
        encodedTexture = new Texture2D(256, 256);

    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {

        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width,
            }
        };

        return writer.Write(textForEncoding);
    }
    public void OnClickEncoding()
    {
        EncodeTextToQRCode();
    }


    private void EncodeTextToQRCode()
    {
        string textWrite = string.IsNullOrEmpty(inputField.text) ? "You should enter text" : inputField.text;
        Color32[] convertPixelToTexture = Encode(textWrite, encodedTexture.width, encodedTexture.height);
        encodedTexture.SetPixels32(convertPixelToTexture);
        encodedTexture.Apply();
        rawImageReciever.texture = encodedTexture;
    }


}
