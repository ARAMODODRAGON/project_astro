using System.Collections.Generic;


namespace Astro.Objects {
	class EntityManager {
		// Singleton
		public static EntityManager Singleton { get; private set; }

		// The Enemy list & player reference
		private List<Enemy> enemies;
		private Player player;

		// The BulletObject that all enemies use
		public Bullets.BulletObject mainBullletObject;

		public void Init() {
			if (Singleton == null) Singleton = this;
			else IO.Debug.Log("The EntityManager Singleton was not null");

			enemies = new List<Enemy>();
			mainBullletObject = new Bullets.BulletObject(1024, false);
		}
		
		public void Exit() {
			if (Singleton == this) Singleton = null;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public void DoCollisionsWithBullets(Bullets.BulletObject bullets) {
			if (bullets.CollidesWithEnemies) {
				for (int i = 0; i < bullets.Count; i++) {

					// TODO: do collision

				}
			} else {
				for (int i = 0; i < bullets.Count; i++) {

					// TODO: do collision

				}
			}
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public void AddEnemy(Enemy enemy) {
			enemies.Add(enemy);
		}
		public void RemoveEnemy(Enemy enemy) {
			enemies.Remove(enemy);
		}
		public void SetPlayer(Player player) {
			this.player = player;
		}
		public void RemovePlayer() {
			player = null;
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}
}
