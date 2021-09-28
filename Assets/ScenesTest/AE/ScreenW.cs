using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenW : MonoBehaviour
{
    Camera cam;
    float screenWidth;
    float screenHeight;
    [SerializeField] float relativePos;
    bool isWrappingX = false;
    Vector3 screenPos;
    public float RelativePos { get => relativePos; set => relativePos = value; }
	void Start()
	{
		cam = Camera.main;
		screenHeight = 2.0f * cam.transform.position.z * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
		screenWidth = screenHeight * cam.aspect;

	}

    void Update()
    {
        screenPos = cam.WorldToScreenPoint(this.transform.position);
        RelativePos = ((screenPos.x / screenWidth) - 0.5f) * 2;

		if (screenPos.x > -0.11f * screenWidth && screenPos.x < screenWidth * (1.11f))
		{
			isWrappingX = false;
		}


		var newPosition = transform.position;
		var viewportPosition = cam.WorldToScreenPoint(transform.position);



		if (!isWrappingX && (viewportPosition.x > screenWidth + screenWidth * 0.1f))
		{
			newPosition.x = -(newPosition.x * 0.9f);


			isWrappingX = true;
		}

		if (!isWrappingX && (viewportPosition.x < -screenWidth * 0.1f))
		{
			newPosition.x = -(newPosition.x * 0.9f);


			isWrappingX = true;
		}

		
		transform.position = newPosition;
	}
}
