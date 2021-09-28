using UnityEngine;
using System.Collections;

public class ScreenWrapper : MonoBehaviour
{
	// Whether to use advancedWrapping or not

	Renderer[] renderers;

	bool isWrappingX = false;
	bool isWrappingY = false;
	Camera cam;

	// We use ghosts in advanced wrapping to create a nice wrapping illusion
	float screenWidth;
	float screenHeight;

	float relativePos;

    public float RelativePos { get => relativePos; set => relativePos = value; }

    void Start()
	{
		renderers = GetComponentsInChildren<Renderer>();

		cam = Camera.main;
		screenHeight = 2.0f * cam.transform.position.z * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
		screenWidth = screenHeight * cam.aspect;
	}

	// Update is called once per frame
	void Update()
	{
		RelativePos = (cam.WorldToViewportPoint(transform.position).x-0.5f)*2;
		//Debug.Log("Estoy en " +RelativePos);
		ScreenWrap();
	}

	void ScreenWrap()
	{
		// If all parts of the object are invisible we wrap it around
		foreach (var renderer in renderers)
		{
			if (renderer.isVisible)
			{
				isWrappingX = false;
				return;
			}
		}

		if (isWrappingX)
		{
			return;
		}

		Vector3 newPosition = transform.position;

		Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);


		if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
		{
		
			newPosition.x = Camera.main.transform.position.x - newPosition.x;

			isWrappingX = true;
		}

		//Apply new position
		transform.position = newPosition;
	}

	
	
	
	


}