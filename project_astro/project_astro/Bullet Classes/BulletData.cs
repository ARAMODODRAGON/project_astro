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

		public void UpdateBullet(ref Bullet bullet) {
			// Find and call the correct function for this bullet
			switch (bullet.Type) {
				case BulletType.None: break; /// Type none should do nothing
				case BulletType.Basic:
					UpdateBullet_Basic(ref bullet);
					break;
				case BulletType.Basic_Red:
					UpdateBullet_Basic(ref bullet);
					break;
				case BulletType.Basic_blue:
					UpdateBullet_Basic(ref bullet);
					break;
				default:
					break;
			}
		}

		public void RenderBullet(ref Bullet bullet) {
			// TODO: add proper functionality

			transform.Position = bullet.Position;
			transform.RotationInRadians = bullet.Radians;
			
			switch (bullet.Type) {
				case BulletType.None: break;
				case BulletType.Basic:
					bulletSprite.color = Color.White;
					bulletSprite.Render();
					break;
				case BulletType.Basic_Red:
					bulletSprite.color = Color.Red;
					bulletSprite.Render();
					break;
				case BulletType.Basic_blue:
					bulletSprite.color = Color.Blue;
					bulletSprite.Render();
					break;
				default: IO.Debug.Log("Couldn't render bullet type: " + bullet.Type); break;
			}

		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Update functions

		private static void UpdateBullet_Basic(ref Bullet bullet) {

		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Render functions



	}
}
