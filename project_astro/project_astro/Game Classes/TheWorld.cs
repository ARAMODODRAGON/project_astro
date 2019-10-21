
namespace project_astro {
	class TheWorld {
		// singleton
		public static TheWorld singleton { get; private set; }

		// main world objects
		Level level;
		BulletManager bulletManager;

		public void Init() {
			// set singleton
			if (singleton == null) singleton = this;

			// init the main world objects

			/// bullet manager
			bulletManager = new BulletManager();
			bulletManager.Init();

			/// test level
			level = new TestLevelA();
			level.Init();
		}

		public void LoadContent() {
			bulletManager.LoadContent();
			level.LoadContent();
		}

		public void Update(float delta) {
			bulletManager.Update(delta);
			level.Update(delta);
		}

		public void Render() {
			bulletManager.Render();
		}

		public void Exit() {
			// remove singleton
			if (singleton == this) singleton = null;

			if (level != null) level.Exit();
		}
	}
}
