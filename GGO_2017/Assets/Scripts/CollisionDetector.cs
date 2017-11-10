using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	public LayerMask collisionMask;

	const float skinWidth = .015f;

	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;

	float horizontalRaySpacing;
	float verticalRaySpacing;

	BoxCollider2D obj;
	RaycastOrigins raycastOrigins;

	// Use this for initialization
	void Start () 
	{
		obj = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateRayCastOrigins();
	}

	//To work on - Need something to trigger the activation of verical collision
	void VerticalCollision(ref Vector3 velocity)
	{
		//To find the direction of the y velocity based of the movement of the object
		//if falling = -1 : if going up = 1
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;

		for(int i = 0; i < verticalRayCount; i++)
		{
			Vector2 rayOrigins = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigins += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigins, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
		
			if(hit)
			{
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
			}
		}
	}

	//Starting points for all raycasts to come off GameObjects
	//Skin width is to make sure that the raycasts come off just a little bit inside of the hitbox
	//that way, even if the collider is up against a wall, it is still sure to cast the raycast to hit the wall
	//rather than starting within the wall
	void UpdateRayCastOrigins()
	{
		//Bounds is the way to register the size of the hitbox
		Bounds bounds = obj.bounds;
		bounds.Expand(skinWidth * -2);

		Debug.Log(bounds);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	//Calculate the space in between shot raycasts
	void CalculateRaySpacing()
	{
		Bounds bounds = obj.bounds;
		bounds.Expand(skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins
	{
		public Vector2 topLeft, topRight, bottomLeft, bottomRight;
	}
}
