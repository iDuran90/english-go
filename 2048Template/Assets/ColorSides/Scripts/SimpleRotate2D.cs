using UnityEngine;
using System.Collections;

public class SimpleRotate2D : MonoBehaviour 
{
    public Transform Target;
    public float rotateSpeed = 1;

	// Update is called once per frame
	void Update () 
    {
        Target.Rotate(Vector3.forward, -rotateSpeed, Space.World);
	}
}
