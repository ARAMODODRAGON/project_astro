using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Astro.Rendering;
using Astro.AMath;

namespace Astro {

	public class Game1 : Game {
		public static Game1 singleton { get; private set; }

		// Rendering
		private Renderer renderer;
		private Camera camera;

		// The world
		private TheWorld theWorld;

		// Input/Output 
		private IO.Debug debug;
		private IO.Input input;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public Game1() {
			singleton = this;
			//renderer = new Renderer(this, new Point(1600, 900));
			renderer = new Renderer(this, new Point(1280, 720));
			camera = new Camera(new Point(0, 0), new Point(1920, 1080));
			Content.RootDirectory = "Content";
			debug = new IO.Debug();
			input = new IO.Input();
			theWorld = new TheWorld();
		}

		protected override void Initialize() {
			// The world
			theWorld.Init();

			// Init input
			input.Init();

			// Call base
			base.Initialize();

		}

		protected override void LoadContent() {
			// Create a sprite batch
			renderer.InitSpriteBatch(GraphicsDevice);

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
				Renderer.ToggleFullscreen();
			f11ispressed = Keyboard.GetState().IsKeyDown(Keys.F11);

			// Update the world
			theWorld.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

			// Call base
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			// Start drawing
			GraphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
			camera.UpdateTransform();
			renderer.Begin(Camera.Transform);

			// Render TheWorld
			theWorld.Render();
			// Debug render
			debug.Render();

			// Stop drawing
			renderer.End();
			base.Draw(gameTime);
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void EndRun() {
			// End the world
			theWorld.Exit();


			base.EndRun();
		}
	}
}
