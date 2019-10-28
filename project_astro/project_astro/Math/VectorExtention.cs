using Microsoft.Xna.Framework;

namespace Astro.AMath {
	static class VectorExtention {
		public static Radial ToRadial(this Vector2 vector) {
			return new Radial(vector);
		}
	}
}
