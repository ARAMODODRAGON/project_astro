using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Astro.Physics;

namespace Astro.Rendering {
	class Sprite {
		// Parent transform
		public Transform ParentTransform { get; private set; }

		// Texture
		public Texture2D Texture { get; private set; }
		// Source rectangle on the texture
		private Rectangle sourceRectangle;
		public Rectangle SourceRectangle => sourceRectangle;

		public Sprite() {
			// Init Rectangle
			sourceRectangle = new Rectangle();
		}

		public Sprite(Transform parent) : this() {
			// Set the parent transform
			ParentTransform = parent;
		}

		public bool LoadTexture(string filename) {
			// Load the texture
			Texture = ContentLoader.Load<Texture2D>(filename);

			// Check if the texture was set
			if (Texture != null) {
				// Set the source rectangle to the entire image
				sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);

				// Return true to say it was set
				return true;
			}
			// Return false to say it was not set
			return false;
		}
	}
}
