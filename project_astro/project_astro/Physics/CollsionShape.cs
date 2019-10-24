using Microsoft.Xna.Framework;

namespace Astro.Physics {
	abstract class CollsionShape {
		// The Parent Transform
		/// <summary>
		/// The parent transform that the collider is relative to
		/// </summary>
		public Transform Parent { get; protected set; }
		// World Position
		/// <summary>
		/// Gives the colliders position in the world. 
		/// </summary>
		public Vector2 WorldPosition {
			get => (Parent != null ? Pivot + Parent.Position : Pivot);
			set {
				if (Parent != null) Parent.Position = value - Pivot;
				else Pivot = value;
			}
		}
		// Pivot
		/// <summary>
		/// The position relative to the parent transform
		/// </summary>
		public Vector2 Pivot;

		// Collision Callback
		/// Delegate definition
		public delegate void CollisionCallBackDelegate(ref CollisionData data);
		/// Delegate decleration
		public CollisionCallBackDelegate OnCollisionEnter;
		public CollisionCallBackDelegate OnCollisionStay;
		public CollisionCallBackDelegate OnCollisionExit;

		public void SetParent(Transform parent) {
			Parent = parent;
		}
	}
}
