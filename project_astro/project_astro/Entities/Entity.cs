using Astro.Physics;

namespace Astro.Objects {
	class Entity {
		// Active variable
		public bool IsActive;

		// Transform
		public PhysicsTransform Transform;

		// Base Constructor
		public Entity() {
			Transform = new PhysicsTransform();
		}

		// Inherited functions
		public virtual void Init() { }
		public virtual void LoadContent() { }
		public virtual void Update(float delta) { }
		public virtual void Render() { }
		public virtual void Exit() { }

	}
}
