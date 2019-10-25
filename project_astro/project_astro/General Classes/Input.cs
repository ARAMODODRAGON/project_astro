using Microsoft.Xna.Framework.Input;

namespace Astro.IO {
	class Input {
		// Singleton
		public static Input Singleton { get; private set; }

		/// Button State
		enum ButtonState {
			HeldUp = 0,
			Down = 1,
			HeldDown = 2,
			Up = 3
		}
		/// Button
		struct Button {
			public ButtonState state;
			public bool IsPressed => state == ButtonState.Down || state == ButtonState.HeldDown;
			public bool IsUnpressed => state == ButtonState.Up || state == ButtonState.HeldUp;
			public Keys Key;
		}

		// Buttons
		private int BUTTON_COUNT = 8;
		private Button[] buttons;

		public void Init() {
			if (Singleton == null) Singleton = this;
			else Debug.LogError("The Input singleton was not null");

			buttons = new Button[BUTTON_COUNT];

			buttons[0].Key = Keys.Up;
			buttons[1].Key = Keys.Down;
			buttons[2].Key = Keys.Left;
			buttons[3].Key = Keys.Right;
			buttons[4].Key = Keys.LeftShift;
			buttons[5].Key = Keys.Z;
			buttons[6].Key = Keys.X;
			buttons[7].Key = Keys.C;
		}

		public void UpdateInput() {
			KeyboardState keyboardState = Keyboard.GetState();

			for (int i = 0; i < BUTTON_COUNT; i++) {
				if (buttons[i].IsPressed && keyboardState.IsKeyUp(buttons[i].Key))
					buttons[i].state = ButtonState.Up;
				else if (buttons[i].IsUnpressed && keyboardState.IsKeyDown(buttons[i].Key))
					buttons[i].state = ButtonState.Down;
				else if (buttons[i].state == ButtonState.Down)
					buttons[i].state = ButtonState.HeldDown;
				else if (buttons[i].state == ButtonState.Up)
					buttons[i].state = ButtonState.HeldUp;
			}
		}

		#region Get Input 

		public static bool GetKey(string keyname) {
			switch (keyname) {
				case "Up": 
					return Singleton.buttons[0].IsPressed;
				case "Down": 
					return Singleton.buttons[1].IsPressed;
				case "Left": 
					return Singleton.buttons[2].IsPressed;
				case "Right": 
					return Singleton.buttons[3].IsPressed;
				case "Shift": 
					return Singleton.buttons[4].IsPressed;
				case "Z": 
					return Singleton.buttons[5].IsPressed;
				case "X": 
					return Singleton.buttons[6].IsPressed;
				case "C":
					return Singleton.buttons[7].IsPressed;
				default: return false;
			}
		}
		
		public static bool GetKeyDown(string keyname) {
			switch (keyname) {
				case "Up": 
					return Singleton.buttons[0].state == ButtonState.Down;
				case "Down": 
					return Singleton.buttons[1].state == ButtonState.Down;
				case "Left": 
					return Singleton.buttons[2].state == ButtonState.Down;
				case "Right": 
					return Singleton.buttons[3].state == ButtonState.Down;
				case "Shift": 
					return Singleton.buttons[4].state == ButtonState.Down;
				case "Z": 
					return Singleton.buttons[5].state == ButtonState.Down;
				case "X": 
					return Singleton.buttons[6].state == ButtonState.Down;
				case "C":
					return Singleton.buttons[7].state == ButtonState.Down;
				default: return false;
			}
		}
		
		public static bool GetKeyUp(string keyname) {
			switch (keyname) {
				case "Up": 
					return Singleton.buttons[0].state == ButtonState.Up;
				case "Down": 
					return Singleton.buttons[1].state == ButtonState.Up;
				case "Left": 
					return Singleton.buttons[2].state == ButtonState.Up;
				case "Right": 
					return Singleton.buttons[3].state == ButtonState.Up;
				case "Shift": 
					return Singleton.buttons[4].state == ButtonState.Up;
				case "Z": 
					return Singleton.buttons[5].state == ButtonState.Up;
				case "X": 
					return Singleton.buttons[6].state == ButtonState.Up;
				case "C":
					return Singleton.buttons[7].state == ButtonState.Up;
				default: return false;
			}
		}
		
		#endregion

		public void Exit() {
			if (Singleton == this) Singleton = null;
		}
	}
}
