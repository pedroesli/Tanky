using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHype : MonoBehaviour
{
    public float minimum = 0.0f;
    public float maximum = 1.0f;
    public float speed;
    private Camera cam;
    private float savedCamSize;
    static float t = 0.0f;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        savedCamSize = cam.orthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = Mathf.Lerp(minimum, maximum, t) + savedCamSize;
        t += 0.5f * Time.deltaTime * speed;

        if(t> 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
    private void OnDisable()
    {
        cam.orthographicSize = savedCamSize;
    }
}
