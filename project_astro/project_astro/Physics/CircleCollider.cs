using Microsoft.Xna.Framework;

namespace Astro.Physics {
	class CircleCollider : CollsionShape {
		public float radius;

		public CircleCollider() {
			radius = 1f;
			Pivot = Vector2.Zero;
		}

		public CircleCollider(Transform parent) {
			radius = 1f;
			Pivot = Vector2.Zero;
		}

		public CircleCollider(float radius) {
			this.radius = radius;
			Pivot = Vector2.Zero;
		}

		public CircleCollider(float radius, Transform parent) {
			this.radius = radius;
			Pivot = Vector2.Zero;
			Parent = parent;
		}
	}
}
