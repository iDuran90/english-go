using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour {
  private bool gyroEnabled;
  private Gyroscope gyro;
  private GameObject cameraContainer;
  public Quaternion rot;
  public Text text;

  private void Start() {
    cameraContainer = new GameObject("Camera Container");
    cameraContainer.transform.position = transform.position;
    transform.SetParent(cameraContainer.transform);

    gyroEnabled = EnableGyro();

    StartCoroutine("DoCheck");
  }

  private bool EnableGyro() {
    if (SystemInfo.supportsGyroscope) {
      gyro = Input.gyro;
      gyro.enabled = true;

      cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
      rot = new Quaternion(0, 0, 0, 0);

      return true;
    }

    return false;
  }

  IEnumerator DoCheck()
  {
    for (; ; ) {
      text.text = gyro.attitude.ToString() + "\n" + transform.localRotation.ToString();

      yield return new WaitForSeconds(1f);
    }
  }

  private void Update() {
    if (gyroEnabled) {
      transform.localRotation = gyro.attitude * rot;
    }
  }
}
