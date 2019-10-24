using Microsoft.Xna.Framework;

namespace Astro.Physics {
	class PhysicsTransform : Transform {
		/// Provided by Transform:
		/// public Vector2 Position
		/// public Vector2 Scale
		public Vector2 Velocity;
		public Vector2 Acceleration;


		public void PhysicsUpdate(float delta) {
			Velocity += Acceleration * delta;
			Position += Velocity * delta;
		}
	}
}
