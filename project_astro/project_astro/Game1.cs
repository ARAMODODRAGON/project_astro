using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_astro {

	public class Game1 : Game {
		public static Game1 singleton { get; private set; }

		// rendering
		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;

		// the world
		TheWorld theWorld;

		// debug 
		Debug debug;

		public Game1() {
			singleton = this;
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			debug = new Debug();
			theWorld = new TheWorld();
		}

		protected override void Initialize() {
			// set viewport
			GraphicsDevice.Viewport = new Viewport(-340, 0, 940, 100);
			graphics.PreferredBackBufferWidth = 600;
			graphics.PreferredBackBufferHeight = 800;
			graphics.ApplyChanges();

			// the world
			theWorld.Init();

			// call base
			base.Initialize();
		}

		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// the world
			theWorld.LoadContent();
		}

		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			theWorld.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			// Start drawing
			GraphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
			Renderer.RenderBegin();

			theWorld.Render();

			// Stop drawing
			Renderer.RenderEnd();
			base.Draw(gameTime);
		}
	}
}
