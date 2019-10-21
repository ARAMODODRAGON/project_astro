using Microsoft.Xna.Framework.Input;

namespace project_astro {
	class TestLevelA : Level {
		private float timer1 = 0f;
		private float timer2 = 0f;
		private float timer3 = 0f;
		private float angle1 = 0f;
		private float angle2 = 0f;
		private float angle3 = 0f;

		private int state = 0;
		private bool keystate = false;

		public override void Init() {

		}

		public override void LoadContent() {

		}

		public override void Update(float delta) {
			if (!keystate && Keyboard.GetState().IsKeyDown(Keys.Space)) {
				state++;
				if (state > 2) state = 0;
				Bullets.Clear();
			}
			keystate = Keyboard.GetState().IsKeyDown(Keys.Space);

			switch (state) {
				case 0: {
					timer1 += delta;

					if (timer1 > 0.2f) {
						timer1 -= 0.2f;
						Bullets.SpawnSingle(BulletType.Basic, 300f, 400f, 80f, angle1, 20f);
						Bullets.SpawnSingle(BulletType.Basic, 300f, 400f, 80f, -angle1 + 90f, 20f);
						angle1 += 20f;
					}
					break;
				}
				case 1: {
					timer2 += delta;

					if (timer2 > 0.5f) {
						timer2 -= 0.5f;
						Bullets.SpawnCircle(BulletType.Basic, 300f, 400f, 10, 50f, 40f, angle2, 0f, 20f);
						angle2 += 90f;
					}
					break;
				}
				case 2: {
					timer3 += delta;

					if (timer3 > 0.6f) {
						timer3 -= 0.6f;
						Bullets.SpawnRange(BulletType.Basic, 300f, 400f, 4, 20f, 60f, 90f, angle3, 0f, 20f);

						angle3 += 45;
					}

					break;
				}
				default:
					break;
			}

		}

		public override void Exit() {

		}

	}
}
