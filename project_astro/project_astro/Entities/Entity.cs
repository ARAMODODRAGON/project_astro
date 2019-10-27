using Astro.Physics;
using Astro.IO;

namespace Astro.Objects {
	class Entity : Interfaces.IScriptable {
		// Active variable
		public bool IsActive;

		// Transform
		public PhysicsTransform Transform { get; private set; }

		// Base Constructor
		public Entity() {
			IsActive = true;
			Transform = new PhysicsTransform();
		}

		protected void Print(object obj) {
			Debug.Log(obj);
		}
		protected void PrintError(object obj) {
			Debug.LogError(obj);
		}

		// From IScriptable interface
		public virtual void Init() { }
		public virtual void LoadContent() { }
		public virtual void Update(float delta) { }
		public virtual void Render() { }
		public virtual void Exit() { }
	}
}
