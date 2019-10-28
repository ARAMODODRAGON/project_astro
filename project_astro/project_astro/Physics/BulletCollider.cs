using Astro.Objects.BO;
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

		public bool DoesOverlap(int bullet, Entity entity) {
			switch (bObject[bullet].logicType) {
				case BulletFlags.Basic:

					break;
				case BulletFlags.Basic_Red:
					break;
				case BulletFlags.Basic_blue:
					break;
				default:
					break;
			}

			return false;
		}
	}
}
