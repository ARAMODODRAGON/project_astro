using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Astro.Physics;

namespace Astro.Rendering {
	class Renderer {
		// Singleton
		public static Renderer Singleton { get; private set; }

		// Spritebatch & GraphicsDeviceManager
		public SpriteBatch spriteBatch;
		public GraphicsDeviceManager graphics;

		/// Properties
		public static SpriteBatch SpriteBatch => Singleton.spriteBatch;
		public static GraphicsDeviceManager Graphics => Singleton.graphics;

		// SpriteBatch begin settings
		private SpriteSortMode sortMode;
		private BlendState blendState;
		private SamplerState samplerState;
		private DepthStencilState depthStencilState;
		private RasterizerState rasterizerState;
		///private Effect effect;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor

		public Renderer(Game game) {
			// Set singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.Log("Renderer Singleton was not null");

			// Create GraphicsDeviceManager
			graphics = new GraphicsDeviceManager(game);
			
			// Set viewport
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();
		}

		public void InitSpriteBatch(GraphicsDevice device) {
			// Create SpriteBatch
			spriteBatch = new SpriteBatch(device);
			
			// Create SpriteBatch begin settings
			/// SpriteSortMode
			sortMode = SpriteSortMode.Deferred;

			/// BlendState
			blendState = BlendState.AlphaBlend;

			/// SamplerState
			samplerState = SamplerState.PointClamp;

			/// DepthStencilState
			depthStencilState = DepthStencilState.None;

			/// RasterizerState
			rasterizerState = new RasterizerState();
			rasterizerState.FillMode = FillMode.Solid;
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Instance functions



		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Static functions

		public static void ToggleFullscreen() {
			Graphics.ToggleFullScreen();
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Render Begin/End

		// Used by the static RenderBegin to cleanly pass all the variables
		public void Begin(Matrix? transform = null) {
			spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, transform);
		}

		public void RenderBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null,
								 SamplerState samplerState = null, DepthStencilState depthStencilState = null,
								 RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null) {
			SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
		}

		public void End() {
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

		public static void DrawSprite(Texture2D texture, Rectangle? sourceRectangle, Transform transform, Vector2 pivot, Color color, 
			float layerdepth = 0f, SpriteEffects effects = SpriteEffects.None) {
			SpriteBatch.Draw(texture, transform.Position, sourceRectangle, color, 
				transform.RotationInRadians, pivot * transform.Scale, transform.Scale, effects, layerdepth);
		}
		
		public static void DrawSprite(Texture2D texture, Rectangle? sourceRectangle, Vector2 position, float rotationInRad, Vector2 scale, Vector2 pivot, 
			Color color, float layerdepth = 0f, SpriteEffects effects = SpriteEffects.None) {
			SpriteBatch.Draw(texture, position, sourceRectangle, color, 
				rotationInRad, pivot * scale, scale, effects, layerdepth);
		}

		public static void DrawSprite(Sprite sprite) {
			SpriteBatch.Draw(sprite.Texture, sprite.Transform.Position, sprite.SourceRectangle, sprite.color,
				sprite.Transform.RotationInRadians, sprite.Pivot * sprite.Transform.Scale, sprite.Transform.Scale, sprite.Effects, sprite.Layer);
		}

	}
}

