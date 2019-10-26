﻿using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Astro.Physics;

namespace Astro.Rendering {
	class Renderer { 

		// Spritebatch & GraphicsDeviceManager
		public static SpriteBatch SpriteBatch => null;
		public static GraphicsDeviceManager Graphics => Game1.singleton.graphics;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Render Begin/End

		public static void RenderBegin() {
			SpriteBatch.Begin();
		}

		public static void RenderBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null,
								 SamplerState samplerState = null, DepthStencilState depthStencilState = null,
								 RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null) {
			SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
		}

		public static void RenderEnd() {
			SpriteBatch.End();
		}
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Drawing directly to screen cooridnates

		#region Normal Drawing

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

		#endregion
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Converting from a world position to a screen position then drawing

		#region World Drawing

		public static void DrawSprite(Texture2D texture, Rectangle? sourceRectangle, Transform transform, Vector2 pivot, Color color, 
			float layerdepth = 0f, SpriteEffects effects = SpriteEffects.None) {
			SpriteBatch.Draw(texture, Camera.WorldToScreen(transform.Position), sourceRectangle, color, 
				transform.RotationInRadians, pivot, transform.Scale, effects, layerdepth);
		}
		
		public static void DrawSprite(Texture2D texture, Rectangle? sourceRectangle, Vector2 position, float rotationInRad, Vector2 scale, Vector2 pivot, 
			Color color, float layerdepth = 0f, SpriteEffects effects = SpriteEffects.None) {
			SpriteBatch.Draw(texture, Camera.WorldToScreen(position), sourceRectangle, color, 
				rotationInRad, pivot, scale, effects, layerdepth);
		}

		public static void DrawSprite(Sprite sprite) {
			SpriteBatch.Draw(sprite.Texture, Camera.WorldToScreen(sprite.Transform.Position), sprite.SourceRectangle, sprite.color,
				sprite.Transform.RotationInRadians, sprite.Pivot, sprite.Transform.Scale, sprite.Effects, sprite.Layer);
		}

		#endregion
		
	}
}

