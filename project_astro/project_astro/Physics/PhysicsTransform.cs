using Microsoft.Xna.Framework;

namespace Astro.Physics {
	class PhysicsTransform : Transform {
		/// Provided by Transform:
		/// public Vector2 Position
		/// public Vector2 Scale
		public Vector2 Velocity;

		// Collider
		public Collider collider = null;

		public PhysicsTransform() : base() {
			Velocity = Vector2.Zero;
		}

		public void PhysicsUpdate(float delta) {
			Position += Velocity * delta;
		}
	}
}
