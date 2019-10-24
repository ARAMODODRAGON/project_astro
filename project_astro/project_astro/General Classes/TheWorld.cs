using Microsoft.Xna.Framework;
using Astro.Levels;
using Astro.Objects;

namespace Astro {
	class TheWorld {
		// singleton
		public static TheWorld singleton { get; private set; }

		// main world objects
		Level level;
		BulletManager bulletManager;
		Player player;

		public void Init() {
			// set singleton
			if (singleton == null) singleton = this;

			// init the main world objects

			/// player object
			player = new Player();

			/// bullet manager
			bulletManager = new BulletManager();
			bulletManager.Init();

			/// test level
			level = new TestLevelB();
			level.Init();
		}

		public void LoadContent() {
			bulletManager.LoadContent();
			level.LoadContent();
		}

		public void Update(float delta) {
			// update world objects
			player.Update(delta);
			bulletManager.Update(delta);
			level.Update(delta);

			//Debug.DrawBox(49, 51, 150, 0, Color.Black);
		}

		public void Render() {
			bulletManager.Render();
		}

		public void Exit() {
			// remove singleton
			if (singleton == this) singleton = null;

			// Exits from level if it exists
			level?.Exit();
		}
	}
}
