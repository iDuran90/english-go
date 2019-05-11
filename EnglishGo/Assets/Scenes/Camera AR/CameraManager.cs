using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {
  private bool camAvailable;
  private WebCamTexture backCam;
  private Texture defaultBackground;

  public RawImage backgroud;
  public AspectRatioFitter fit;

  private void Start()
  {
    defaultBackground = backgroud.texture;
    WebCamDevice[] devices = WebCamTexture.devices;

    if (devices.Length == 0) {
      camAvailable = false;
      return;
    }

    for (int i = 0; i < devices.Length; i++) {
      if (!devices[i].isFrontFacing) {
        backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
      }
    }

    if (backCam == null) {
      return;
    }

    backCam.Play();
    backgroud.texture = backCam;

    camAvailable = true;
  }

  private void Update()
  {
    if (!camAvailable) return;

    float ratio = (float)backCam.width / (float)backCam.height;
    fit.aspectRatio = ratio;

    float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
    backgroud.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

    int orient = -backCam.videoRotationAngle;
    backgroud.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
  }
}
