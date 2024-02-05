using UnityEngine;
using System.Collections;

public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;
	
    [SerializeField]
	public float skinWidth = .015f;
    [SerializeField]
	public float dstBetweenRays = .25f;
	[HideInInspector]
	public int horizontalRayCount;
	[HideInInspector]
	public int verticalRayCount;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	public BoxCollider2D collider;
	public Rigidbody2D rigid;
	[HideInInspector]
	public RaycastOrigins raycastOrigins;

	public virtual void Awake() {
		if (collider == null)
		{
			collider = GetComponentInParent<BoxCollider2D> ();
		}
	}

	public virtual void Start() {
		CalculateRaySpacing ();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);
		
		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}
	
	public void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;
		
		horizontalRayCount = Mathf.Abs(Mathf.RoundToInt (boundsHeight / dstBetweenRays));
		verticalRayCount = Mathf.Abs(Mathf.RoundToInt (boundsWidth / dstBetweenRays));
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}
	
	public struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}
