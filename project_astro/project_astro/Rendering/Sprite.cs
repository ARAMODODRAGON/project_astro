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
		private Rectangle? sourceRectangle;
		public Rectangle? SourceRectangle => sourceRectangle;

		// Color modifier
		public Color color;
		public float FAlpha {
			get => color.A / 255f;
			set => color.A = (byte)(value * 255);
		}
		public byte Alpha {
			get => color.A;
			set => color.A = value;
		}

		// Pivot, layer & destination rectangle
		public Vector2 Pivot;
		public int Layer;
		public Rectangle destinationRectangle;
		public Rectangle DestinationRectangle => destinationRectangle;

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
			sourceRectangle = null;
			destinationRectangle = new Rectangle();
			color = Color.White;
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
				// Set the source and destination rectangles to the entire image
				sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
				destinationRectangle.Width = Texture.Width;
				destinationRectangle.Height = Texture.Height;
				// Return true to say it was set
				return true;
			}
			// Return false to say it was not set
			return false;
		}

		#endregion

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public void Render() {
			// TODO: add rotation and origin
			destinationRectangle.X = (int)(Transform.Position.X - Texture.Width * Pivot.X);
			destinationRectangle.Y = (int)(Transform.Position.Y - Texture.Height * Pivot.Y);
			Renderer.DrawSprite(Texture, destinationRectangle, sourceRectangle, color, Layer, Effects);
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
