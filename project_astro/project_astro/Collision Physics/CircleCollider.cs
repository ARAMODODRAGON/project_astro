using Microsoft.Xna.Framework;

namespace project_astro {
	class CircleCollider : CollsionShape {
		public float radius;

		public CircleCollider() {
			radius = 1f;
			pivot = Vector2.Zero;
		}

		public CircleCollider(float radius) {
			this.radius = radius;
			pivot = Vector2.Zero;
		}
	}
}
