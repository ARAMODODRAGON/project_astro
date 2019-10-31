using Microsoft.Xna.Framework;
using Astro.Objects.Bullets;

namespace Astro.Physics {
	struct CollisionData {
		public CollisionData(BulletFlags flags) {
			Flags = flags;
		}
		public BulletFlags Flags { get; }
	}
}
