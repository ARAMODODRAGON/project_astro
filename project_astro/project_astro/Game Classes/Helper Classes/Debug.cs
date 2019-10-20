using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System;

namespace project_astro {
	class Debug {
		private struct DrawEvent {
			public DrawEvent(int Left_, int Right_, int Top_, int Bottom_, Color color_, bool outlined_) {
				Top = Top_;
				Bottom = Bottom_;
				Right = Right_;
				Left = Left_;
				color = color_;
				outlined = outlined_;
			}
			public int Top;
			public int Bottom;
			public int Left;
			public int Right;
			public Color color;
			public bool outlined;
		}
		//queue
		private static Queue<DrawEvent> drawingEvents;

		//textures
		private Texture2D box;

		public Debug() {
			drawingEvents = new Queue<DrawEvent>();
		}

		public void LoadContent(ContentManager content) {
			box = content.Load<Texture2D>("DebugDrawBox");
		}

		public void Render() {
			DrawEvent drawEvent;
			Rectangle rect = new Rectangle();
			while (drawingEvents.Count > 0) {
				drawEvent = drawingEvents.Dequeue();

				if (!drawEvent.outlined) {
					rect.X = drawEvent.Left;
					rect.Y = drawEvent.Bottom;
					rect.Width = drawEvent.Right - drawEvent.Left;
					rect.Height = drawEvent.Top - drawEvent.Bottom;

					Renderer.Draw(box, rect, drawEvent.color);
				} else {
					// Draw Left Side
					rect.X = drawEvent.Left - 2;
					rect.Y = drawEvent.Bottom - 2;
					rect.Width = 4;
					rect.Height = drawEvent.Top - drawEvent.Bottom;

					Renderer.Draw(box, rect, drawEvent.color);
					
					// Draw Right Side
					rect.X = drawEvent.Right - 2;
					rect.Y = drawEvent.Bottom - 2;
					rect.Width = 4;
					rect.Height = drawEvent.Top - drawEvent.Bottom;

					Renderer.Draw(box, rect, drawEvent.color);

					// Draw Top Side
					rect.X = drawEvent.Left - 2;
					rect.Y = drawEvent.Top - 2;
					rect.Width = drawEvent.Right - drawEvent.Left;
					rect.Height = 4;

					Renderer.Draw(box, rect, drawEvent.color);
					
					// Draw Bottom Side
					rect.X = drawEvent.Left - 2;
					rect.Y = drawEvent.Bottom - 2;
					rect.Width = drawEvent.Right - drawEvent.Left;
					rect.Height = 4;

					Renderer.Draw(box, rect, drawEvent.color);
				}
			}
		}

		public static void DrawBox(int Left, int Right, int Top, int Bottom, Color color, bool outlined = false) {
			drawingEvents.Enqueue(new DrawEvent(Left, Right, Top, Bottom, color, outlined));
		}

		public static void DrawBox(int Left, int Right, int Top, int Bottom, bool outlined = false) {
			drawingEvents.Enqueue(new DrawEvent(Left, Right, Top, Bottom, Color.White, outlined));
		}

		public static void Log(object obj) {
			Console.WriteLine(obj.ToString());
		}

	}
}
