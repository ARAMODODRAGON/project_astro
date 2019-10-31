///using Microsoft.Xna.Framework;
using Astro.Levels;
using Astro.Objects;
using Astro.Objects.Bullets;

namespace Astro {
	class TheWorld : Interfaces.IScriptable {
		// singleton
		public static TheWorld Singleton { get; private set; }

		// main world objects
		private Level level;
		private Managers.BulletManager bulletManager;
		private Player player;
		private EntityManager entityManager;

		public void Init() {
			// set singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.LogError("TheWorld Singleton was not null");

			// init the main world objects
			/// bullet manager
			bulletManager = new Managers.BulletManager();
			bulletManager.Init();
			
			/// EntityManager
			entityManager = new EntityManager();
			entityManager.Init();

			/// player object
			player = new Player();
			player.Init();

			/// test level
			level = new TestLevelB();
			level.Init();
		}

		public void LoadContent() {
			player.LoadContent();
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
			// Render world objects
			player.Render();
			bulletManager.Render();
		}

		public void Exit() {
			// remove singleton
			if (Singleton == this) Singleton = null;

			// Call Exit on world objects
			level?.Exit(); /// if it's null then it wont call the function
			player.Exit();
			entityManager.Exit();
			bulletManager.Exit();
		}
	}
}
