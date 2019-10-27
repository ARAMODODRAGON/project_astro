using Microsoft.Xna.Framework;
using Astro.Physics;

namespace Astro.Rendering {
	class Camera {
		// Singleton
		public static Camera Singleton { get; private set; }

		// Matrix
		private Matrix transform;
		/// Property
		public static Matrix Transform => Singleton.transform;

		// Rect of camera
		public Rectangle bounds;
		/// Properties
		public static Rectangle Bounds => Singleton.bounds;
		public static Point Position {
			get => new Point(Singleton.bounds.X, Singleton.bounds.Y);
			set {
				Singleton.bounds.X = value.X;
				Singleton.bounds.Y = value.Y;
			}
		}
		public static Point Size {
			get => new Point(Singleton.bounds.Width, Singleton.bounds.Height);
			set {
				Singleton.bounds.Width = value.X;
				Singleton.bounds.Height = value.Y;
			}
		}
		public static Point Center {
			get => Singleton.bounds.Center;
			set {
				Singleton.bounds.X = value.X - (Singleton.bounds.Width / 2);
				Singleton.bounds.Y = value.Y - (Singleton.bounds.Height / 2);
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor

		public Camera(Point position, Point size) {
			// Set Singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.LogError("Camera Singleton was not null");

			// Set transform
			SetTransform(position, size);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Matrix and bounds

		public void SetTransform(Point position, Point size) {
			bounds = new Rectangle(position, size);
			transform = Matrix.CreateOrthographicOffCenter(bounds, 100f, -100f);
		}

		public void UpdateTransform() {
			transform = Matrix.CreateOrthographicOffCenter(bounds, 100f, -100f);
		}

		public static Vector2 WorldToScreen(Vector2 position) {
			return Vector2.Transform(position, Transform);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Bound checking

		public static bool IsOutsideBounds(Vector2 point) {
			return Bounds.Contains(point.ToPoint());
		}

		public static bool IsOutsideBounds(Vector2 position, float extents) {
			position.X -= extents;
			position.Y -= extents;
			return Bounds.Contains(new Rectangle(position.ToPoint(), new Point((int)extents * 2, (int)extents * 2)));
		}

	}
}
