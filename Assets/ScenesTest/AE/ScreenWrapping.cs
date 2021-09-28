using UnityEngine;
using System.Collections;

public class ScreenWrapping : MonoBehaviour
{
	// Whether to use advancedWrapping or not
	public bool advancedWrapping = true;

	Renderer[] renderers;

	bool isWrappingX = false;
	

	// We use ghosts in advanced wrapping to create a nice wrapping illusion
	Transform[] ghosts = new Transform[2];

	float screenWidth;
	float screenHeight;

	Camera cam;

	Vector3 screenPos;
	
	[SerializeField] float relativePos;

    public float RelativePos { get => relativePos; set => relativePos = value; }

    void Start()
	{

		screenWidth = Screen.width;

		screenHeight = Screen.height;

		cam = Camera.main;
		screenPos = cam.WorldToScreenPoint(this.transform.position);
		RelativePos = ((screenPos.x / screenWidth) - 0.5f)*2;
		renderers = GetComponentsInChildren<Renderer>();

		//var cam = Camera.main;
		//var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
		//var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

	
		// screenWidth = screenTopRight.x - screenBottomLeft.x;
		
		if (advancedWrapping)
		{
			
			CreateGhostShips();
		}
	}

	// Update is called once per frame
	void Update()
	{
		screenPos = cam.WorldToScreenPoint(this.transform.position);
		RelativePos = ((screenPos.x / screenWidth) - 0.5f)*2;

		// Debug.Log(screenPos.x);

		if (advancedWrapping)
		{
			AdvancedScreenWrap();
		}
		else
		{
			ScreenWrap();
		}
	}

	void ScreenWrap()
	{
        /*foreach (var renderer in renderers)
		{
			if (renderer.isVisible)
			{
				isWrappingX = false;
				//isWrappingY = false;
				return;
			}
		}
		//if (isWrappingX && isWrappingY)
		if (isWrappingX)
		{
			return;
		}


		*/

        if (screenPos.x > -0.0f * screenWidth && screenPos.x < screenWidth*(1.0f))
        {
			isWrappingX = false;
        }


		var newPosition = transform.position;
		var viewportPosition = cam.WorldToScreenPoint(transform.position);



		if (!isWrappingX && (viewportPosition.x > screenWidth+screenWidth*0.1f))
		{
			newPosition.x = -(newPosition.x * 0.9f);

			// If you had a camera that isn't at X = 0 and Y = 0,
			// you would have to use this instead
			// newPosition.x = Camera.main.transform.position - newPosition.x;

			isWrappingX = true;
		}

		if (!isWrappingX && (viewportPosition.x < -screenWidth*0.1f))
		{
			newPosition.x = -(newPosition.x*0.9f);

			// If you had a camera that isn't at X = 0 and Y = 0,
			// you would have to use this instead
			// newPosition.x = Camera.main.transform.position - newPosition.x;

			isWrappingX = true;
		}

		/*if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
		{
			newPosition.y = -newPosition.y;

			isWrappingY = true;
		}*/

		//Apply new position
		transform.position = newPosition;
	}

	void AdvancedScreenWrap()
	{
		// Move to separate function
		var isVisible = false;
		foreach (var renderer in renderers)
		{
			if (renderer.isVisible)
			{
				isVisible = true;
				break;
			}
		}

		if (!isVisible)
		{
			SwapShips();
		}
	}

	void CreateGhostShips()
	{
		for (int i = 0; i < 2; i++)
		{
			// Ghost ships should be a copy of the main ship
			ghosts[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;

			// But without the screen wrapping component
			DestroyImmediate(ghosts[i].GetComponent<ScreenWrapping>());
		}

		PositionGhostShips();
	}

	void PositionGhostShips()
	{
		// All ghost positions will be relative to the ships (this) transform,
		// so let's star with that.
		var ghostPosition = transform.position;

		// Right.
		ghostPosition.x = transform.position.x + screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[0].position = ghostPosition;


		// Left
		ghostPosition.x = transform.position.x - screenWidth;
		ghostPosition.y = transform.position.y;
		ghosts[1].position = ghostPosition;


		// All ghost ships should have the same rotation as the main ship
		for (int i = 0; i < 2; i++)
		{
			ghosts[i].rotation = transform.rotation;
		}
	}

	void SwapShips()
	{

		foreach (var ghost in ghosts)
		{
			if (ghost.position.x < screenWidth && ghost.position.x > -screenWidth)
			{
				transform.position = ghost.position;

				break;
			}
		}

		PositionGhostShips();
	}

/*	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 160, 48), "Simple Wrapping"))
		{
			SwitchToSimpleWrapping();
		}

		if (GUI.Button(new Rect(200, 20, 160, 48), "Advanced Wrapping"))
		{
			SwitchToAdvancedWrapping();
		}
	}
*/
	void SwitchToSimpleWrapping()
	{
		advancedWrapping = false;

		foreach (var ghost in ghosts)
		{
			Destroy(ghost.gameObject);
		}
	}

	void SwitchToAdvancedWrapping()
	{
		advancedWrapping = true;

		CreateGhostShips();
	}

}