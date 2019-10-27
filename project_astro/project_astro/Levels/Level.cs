

namespace Astro.Levels {
	abstract class Level : Interfaces.IScriptable {
		public static Level singleton { get; private set; }

		public Level() {
			if (singleton == null) singleton = this;
			else IO.Debug.Log("The level singleton isn't null");
		}

		~Level() {
			if (singleton == this) singleton = null;
		}

		public abstract void Init();
		public abstract void LoadContent();
		public abstract void Update(float delta);
		public virtual void Render() { }
		public abstract void Exit();
	}
}
