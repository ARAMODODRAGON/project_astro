﻿using Microsoft.Xna.Framework;

namespace Astro.Physics {
	abstract class EntityCollider : Collider {
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
		public delegate void CollisionCallBackDelegate();
		/// Delegate decleration
		public CollisionCallBackDelegate OnCollision;

		public void SetParent(Transform parent) {
			Parent = parent;
		}
	}
}
