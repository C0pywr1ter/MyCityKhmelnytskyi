using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage rawImageBackground;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI textOut;
    [SerializeField] private RectTransform scanZone;

    private bool isCamAvailable;
    private WebCamTexture cameraTexture;

    private void Start()
    {
        SetUpCamera();
    }
    private void Update()
    {
        UpdateCameraRender();
    }
    private void UpdateCameraRender()
    {
        if (isCamAvailable == false)
        {
            return;
        }
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3 (0, 0, orientation); 
    }
    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            isCamAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }

        }
        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailable = true;
    }
    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                textOut.text = result.Text;
                
            }
            else
            {
                textOut.text = "fail to read QR Code";
            }
        }
        catch
        {
            textOut.text = "fail in try";
        }
    }
    public void OnClickScan()
    {
            Scan(); 
    }
}
