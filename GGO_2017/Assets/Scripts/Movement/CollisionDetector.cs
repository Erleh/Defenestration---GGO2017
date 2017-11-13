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

	public BoxCollider2D objCollider;
	RaycastOrigins raycastOrigins;
		
	public void VerticalCollision(ref Vector3 velocity)
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

			Debug.DrawRay(rayOrigins, Vector2.up * directionY * rayLength, Color.red);

			if(hit)
			{
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;
			}
		}
	}

	public void HorizontalCollision(ref Vector3 velocity)
	{
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;

		for(int i = 0; i < horizontalRayCount; i++)
		{
			Vector2 rayOrigins = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigins += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigins, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigins, Vector2.right * directionX * rayLength, Color.red);

			if(hit)
			{
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;
			}
		}
	}

	//Starting points for all raycasts to come off GameObjects
	//Skin width is to make sure that the raycasts come off just a little bit inside of the hitbox
	//that way, even if the collider is up against a wall, it is still sure to cast the raycast to hit the wall
	//rather than starting within the wall
	public void UpdateRayCastOrigins()
	{
		//Bounds is the way to register the size of the hitbox
		Bounds bounds = objCollider.bounds;
		bounds.Expand(skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	//Calculate the space in between shot raycasts
	public void CalculateRaySpacing()
	{
		Bounds bounds = objCollider.bounds;
		bounds.Expand(skinWidth * -2);

		//Make max and min value for raycast count
		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		//Space inbetween raycasts
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	//Defines where the the "anchors" for reference of the shape begins
	struct RaycastOrigins
	{
		public Vector2 topLeft, topRight, bottomLeft, bottomRight;
	}
}
