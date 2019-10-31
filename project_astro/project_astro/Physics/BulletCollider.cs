using Astro.Objects.Bullets;
using Astro.Objects;

namespace Astro.Physics {
	class BulletCollider : Collider {
		// Reference to the bulletobject
		public BulletObject bObject { get; private set; }

		// Constructors
		public BulletCollider(BulletObject bObject) {
			// Set the bullet object
			this.bObject = bObject;
		}

		
	}
}
