using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Astro.Rendering;

namespace Astro.Objects.BO {
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

		public void UpdateBullet(BulletObject bob) {
			// Find and call the correct function for this bullet
			for (int i = 0; i < bob.Count; i++) {
				switch (bob[i].logicType) {
					case BulletLogic.None: break;
					case BulletLogic.MoveLinear:
						UpdateBullet_Linear(ref bob[i]);
						break;
					case BulletLogic.MoveRadial:
						UpdateBullet_Radial(ref bob[i]);
						break;
					default:
						break;
				}
			}
		}

		public void RenderBullet(ref Bullet bullet) {
			// TODO: add proper functionality

			transform.Position = bullet.position;
			//transform.RotationInRadians = bullet.radial.rota;

		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Update functions

		private static void UpdateBullet_Linear(ref Bullet bullet) {

		}

		private static void UpdateBullet_Radial(ref Bullet bullet) {

		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Render functions



	}
}
