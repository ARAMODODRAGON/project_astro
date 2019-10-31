using Astro.Physics;
using Astro.IO;

namespace Astro.Objects {
	class Entity : Interfaces.IScriptable {
		// Active variable
		public bool IsActive;

		// Main Components
		public PhysicsTransform transform;
		public CircleCollider collider;

		// Health Components
		protected readonly float MAX_HEALTH;
		private float health;
		protected float Health {
			get => health;
			set {
				if (value > MAX_HEALTH) 
					health = MAX_HEALTH;
				if (value < 0) 
					health = 0f;
				else 
					health = value;
			}
		}

		// Base Constructor
		public Entity(float maxHealth) {
			MAX_HEALTH = maxHealth;
			IsActive = true;
			transform = new PhysicsTransform();
			collider = new CircleCollider(1f, transform) {
				OnCollision = OnCollision
			};
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

		protected virtual void OnCollision() { }


	}
}
