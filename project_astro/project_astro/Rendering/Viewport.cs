using Microsoft.Xna.Framework;

namespace Astro {
	static class Viewport {
		// View matrix
		public static Matrix ViewMatrix { get; private set; }

		public static float Top { get; private set; }
		public static float Bottom { get; private set; }
		public static float Left { get; private set; }
		public static float Right { get; private set; }

		public static void SetView(Vector3 position) {
			ViewMatrix = Matrix.CreateLookAt(position, Vector3.Forward, Vector3.Up);
		}

		public static void SetProjection() {

		}

		public static bool IsOutsideBounds(Vector2 point) {
			return point.X > Right 
				|| point.X < Left 
				|| point.Y < Top 
				|| point.Y > Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, Rectangle rect) {
			return (point.X - rect.Width) > Right 
				|| (point.X + rect.Width) < Left 
				|| (point.Y + rect.Height) < Top 
				|| (point.Y - rect.Height) > Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, CircleCollider circle) {
			return (point.X - circle.radius) > Right 
				|| (point.X + circle.radius) < Left 
				|| (point.Y + circle.radius) < Top 
				|| (point.Y - circle.radius) > Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, float radius) {
			return (point.X - radius) > Right 
				|| (point.X + radius) < Left 
				|| (point.Y + radius) < Top 
				|| (point.Y - radius) > Bottom;
		}
	}
}
