using Microsoft.Xna.Framework;
using Astro.Physics;

namespace Astro.Rendering {
	static class Camera {
		// View matrix
		private static Matrix ProjectionMatrix;
		private static Matrix ViewMatrix;

		private static float top;
		private static float bottom;
		private static float left;
		private static float right;
		public static float Top {
			get => top + Position.X;
			set => top = value;
		}
		public static float Bottom {
			get => bottom + Position.X;
			set => bottom = value;
		}
		public static float Left {
			get => left + Position.X;
			set => left = value;
		}
		public static float Right {
			get => right + Position.X;
			set => right = value;
		}

		private static Vector3 Position;

		public static void SetProjection(float width, float height) {
			Matrix.CreateOrthographic(width, height, 100, -100, out ProjectionMatrix);
			left = 0;
			right = width;
			top = 0;
			bottom = height;
		}

		public static void SetView(Vector3 position) {
			ViewMatrix = Matrix.CreateLookAt(position, Vector3.Forward, Vector3.Up);
			Position = position;
		}

		public static Vector2 WorldToScreen(Vector2 position) {
			//Vector2 temp = Vector2.Zero;
			//Vector2.Transform(ref position, ref ViewMatrix, out temp);
			//Vector2.Transform(ref temp, ref ProjectionMatrix, out position);

			return Vector2.Transform(Vector2.Transform(position, ViewMatrix), ProjectionMatrix);
		}

		#region Checking bounds

		public static bool IsOutsideBounds(Vector2 point) {
			return point.X > Right
				|| point.X < Left
				|| point.Y > Top
				|| point.Y < Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, Rectangle rect) {
			return (point.X - rect.Width) > Right
				|| (point.X + rect.Width) < Left
				|| (point.Y + rect.Height) > Top
				|| (point.Y - rect.Height) < Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, CircleCollider circle) {
			return (point.X - circle.radius) > Right
				|| (point.X + circle.radius) < Left
				|| (point.Y + circle.radius) > Top
				|| (point.Y - circle.radius) < Bottom;
		}

		public static bool IsOutsideBounds(Vector2 point, float radius) {
			return (point.X - radius) > Right
				|| (point.X + radius) < Left
				|| (point.Y + radius) > Top
				|| (point.Y - radius) < Bottom;
		}
		
		#endregion
	}
}
