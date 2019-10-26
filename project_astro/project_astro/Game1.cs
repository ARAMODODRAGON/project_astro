using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Astro.Rendering;

namespace Astro {

	public class Game1 : Game {
		public static Game1 singleton { get; private set; }

		// Rendering
		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;

		/// Rendering related variables
		

		// The world
		TheWorld theWorld;

		// Input/Output 
		IO.Debug debug;
		IO.Input input;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public Game1() {
			singleton = this;
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			debug = new IO.Debug();
			input = new IO.Input();
			theWorld = new TheWorld();
		}

		protected override void Initialize() {
			// Set viewport
			//GraphicsDevice.Viewport = new Microsoft.Xna.Framework.Graphics.Viewport(0, 0, 1280, 720);
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();

			// Disable smoothing
			

			// Set the camera
			Camera.SetView(Vector3.Zero);
			Camera.SetProjection(5f, 5f);

			// The world
			theWorld.Init();

			// Init input
			input.Init();

			// Call base
			base.Initialize();
		}

		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load the world
			theWorld.LoadContent();

			// Load the debug content
			debug.LoadContent();
		}
		protected override void UnloadContent() { }

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		bool f11ispressed = false;

		protected override void Update(GameTime gameTime) {
			// Get inputs
			input.UpdateInput();

			// Exit if escape is pressed
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.F11) && !f11ispressed)
				graphics.ToggleFullScreen();
			f11ispressed = Keyboard.GetState().IsKeyDown(Keys.F11);

			// Update the world
			theWorld.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

			// Call base
			base.Update(gameTime);
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void Draw(GameTime gameTime) {
			// Start drawing
			GraphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
			Renderer.RenderBegin(SpriteSortMode.FrontToBack);

			// Render TheWorld
			theWorld.Render();
			// Debug render
			debug.Render();

			// Stop drawing
			Renderer.RenderEnd();
			base.Draw(gameTime);
		}

		protected override void EndRun() {
			// End the world
			theWorld.Exit();


			base.EndRun();
		}
	}
}
