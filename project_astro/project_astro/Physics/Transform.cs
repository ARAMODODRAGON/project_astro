using Microsoft.Xna.Framework;

namespace Astro.Physics {
	class Transform {
		public Vector2 Position;
		public Vector2 Scale;
		public float RotationInRadians;
		public float RotationInDegrees {
			get => MathHelper.ToDegrees(RotationInRadians);
			set => RotationInRadians = MathHelper.ToRadians(value);
		}

		public Transform() {
			Position = Vector2.Zero;
			Scale = Vector2.One;
		}
	}
}
