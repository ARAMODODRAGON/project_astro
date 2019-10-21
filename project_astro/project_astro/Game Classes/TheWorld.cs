
namespace project_astro {
	class TheWorld {
		// singleton
		public static TheWorld singleton { get; private set; }

		// main world objects
		BulletManager bulletManager;

		public void Init() {
			// set singleton
			if (singleton == null) singleton = this;

			// init the main world objects
			bulletManager = new BulletManager();
			bulletManager.Init();
		}

		public void LoadContent() {
			bulletManager.LoadContent();
		}

		public void Update(float delta) {
			bulletManager.Update(delta);
		}

		public void Render() {
			bulletManager.Render();
		}

		public void Exit() {
			// remove singleton
			if (singleton == this) singleton = null;
		}
	}
}
