using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Astro.Physics;
using Astro.IO;

namespace Astro.Rendering {
	class Sprite {
		// Parent transform
		public Transform Transform { get; private set; }

		// Texture
		public Texture2D Texture { get; private set; }
		// Source rectangle on the texture
		private Rectangle sourceRectangle;
		public Rectangle SourceRectangle => sourceRectangle;

		// Color modifier
		public Color spritecolor;
		public float FAlpha {
			get => spritecolor.A / 255f;
			set => spritecolor.A = (byte)(value * 255);
		}
		public byte Alpha {
			get => spritecolor.A;
			set => spritecolor.A = value;
		}

		// Pivot & layer
		public Vector2 Pivot;
		public int Layer;

		// Sprite effects
		public SpriteEffects Effects {
			get {
				if (FlipVertically && FlipHorizontally)
					return SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
				else if (FlipVertically)
					return SpriteEffects.FlipVertically;
				else if (FlipHorizontally)
					return SpriteEffects.FlipHorizontally;
				else
					return SpriteEffects.None;
			}
		}
		public bool FlipVertically;
		public bool FlipHorizontally;

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////

		#region Initialization

		public Sprite() {
			// Init variables
			sourceRectangle = new Rectangle();
			spritecolor = Color.White;
			Pivot = Vector2.Zero;
			FlipHorizontally = false;
			FlipVertically = false;
			Layer = 0;
		}

		public Sprite(Transform parent) : this() {
			// Set the parent transform
			Transform = parent;
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

		#endregion
		
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public void Render() {
			Renderer.DrawSprite(Texture, sourceRectangle, Transform, Pivot, spritecolor, Layer, Effects);
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
