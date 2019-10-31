using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Astro.Rendering;

namespace Astro.Objects.Bullets {
	class BulletData {
		// Singleton
		public static BulletData Singleton { get; private set; }

		// Sprite and transform (temporary)
		private Sprite bulletSprite;
		private Physics.Transform transform;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialisation

		// Init
		public void Init() {
			// Set singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.LogError("Bulletdata singleton was not null");

			// Temp
			/// Create Transform
			transform = new Physics.Transform();
			/// Create sprite
			bulletSprite = new Sprite(transform);
		}

		// Load Content
		public void LoadContent() {
			bulletSprite.LoadTexture("BulletTestSprite");
		}

		// Exit
		public void Exit() {
			// Remove singleton
			if (Singleton == this) Singleton = null;

			// Destroy sprite
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Check the BulletType and call the corisponding update/render function
		
		public void UpdateBullet(BulletObject bob, float delta) {
			// Find and call the correct function for this bullet
			for (int i = 0; i < bob.Count; i++) {
				if (bob[i].logicType == BulletLogic.None) continue;
				bob[i].TimeSinceAwake += delta;
				switch (bob[i].logicType) {
					case BulletLogic.MoveLinear:
						UpdateBullet_Linear(ref bob[i], delta);
						break;
					case BulletLogic.MoveRadial:
						UpdateBullet_Radial(ref bob[i], delta);
						break;
					default: break;
				}
			}
		}

		public void RenderBullet(BulletObject bob) {
			for (int i = 0; i < bob.Count; i++) {
				switch (bob[i].drawType) {
					case BulletDraw.Circle:
						DrawBullet_Circle(ref bob[i]);
						break;
					case BulletDraw.FireBall:
						DrawBullet_Fireball(ref bob[i]);
						break;
					default: break;
				}
			}
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Update functions

		private void UpdateBullet_Linear(ref Bullet bullet, float delta) {
			bullet.position += bullet.radial.ToVector2() * delta;
		}

		private void UpdateBullet_Radial(ref Bullet bullet, float delta) {

		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Render functions

		private void DrawBullet_Circle(ref Bullet bullet) {
			transform.Position = bullet.position;
			transform.Scale = Vector2.Zero;
			transform.RotationInRadians = bullet.radial.Radians;
			bulletSprite.color = bullet.color;
			bulletSprite.Render();
		}

		private void DrawBullet_Fireball(ref Bullet bullet) {

		}

	}
}
