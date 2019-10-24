using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Astro {
	static class Renderer {
		// Spritebatch & GraphicsDeviceManager
		public static SpriteBatch SpriteBatch => Game1.singleton.spriteBatch;
		public static GraphicsDeviceManager Graphics => Game1.singleton.graphics;

		#region Rendering

		public static void RenderBegin() {
			SpriteBatch.Begin();
		}

		public static void RenderBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null,
								 SamplerState samplerState = null, DepthStencilState depthStencilState = null,
								 RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null) {
			SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
		}

		public static void NormalDraw(Texture2D texture, Rectangle destinationRectangle, Color color) {
			SpriteBatch.Draw(texture, destinationRectangle, color);
		}

		public static void NormalDraw(Texture2D texture, Vector2 position, Color color) {
			SpriteBatch.Draw(texture, position, color);
		}

		public static void NormalDraw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color) {
			SpriteBatch.Draw(texture, position, sourceRectangle, color);
		}

		public static void NormalDraw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation,
				   Vector2 origin, SpriteEffects effects, float layerDepth) {
			SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
		}

		public static void NormalDraw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color) {
			SpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
		}

		public static void NormalDraw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin,
				   Vector2 scale, SpriteEffects effects, float layerDepth) {
			SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
		}

		public static void NormalDraw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin,
				   float scale, SpriteEffects effects, float layerDepth) {
			SpriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
		}

		public static void RenderEnd() {
			SpriteBatch.End();
		}

		#endregion

	}
}

