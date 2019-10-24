using Microsoft.Xna.Framework.Input;

namespace Astro.Levels {
	class TestLevelA : Level {
		private float timer1 = 0f;
		private float timer2 = 0f;
		private float timer3 = 0f;
		private float timer4 = 0f;
		private float angle1 = 0f;
		private float angle2 = 0f;
		private float angle3 = 0f;
		private float angle4 = 0f;

		private int state = 1;
		private bool keystate = false;

		public override void Init() {

		}

		public override void LoadContent() {

		}

		public override void Update(float delta) {
			if (!keystate && Keyboard.GetState().IsKeyDown(Keys.Space)) {
				state++;
				if (state > 3) state = 0;
				BulletManager.ClearBullets();
			}
			keystate = Keyboard.GetState().IsKeyDown(Keys.Space);

			switch (state) {
				case 0: {
					timer1 += delta;

					if (timer1 > 0.2f) {
						timer1 -= 0.2f;
						BulletManager.SpawnSingle(BulletType.Basic, 50f, 50f, 80f, angle1 + 45f, 20f);
						BulletManager.SpawnSingle(BulletType.Basic, 50f, 50f, 80f, -angle1 - 45f, 20f);
						angle1 += 20f;
					}
					break;
				}
				case 1: {
					timer2 += delta;

					if (timer2 > 0.07f) {
						timer2 -= 0.07f;
						BulletManager.SpawnCircle(BulletType.Basic, 50f, 50f, 10, 5f, 90f, angle2, 0f, 20f);
						angle2 += 30f;
					}
					break;
				}
				case 2: {
					timer3 += delta;

					if (timer3 > 0.6f) {
						timer3 -= 0.6f;
						BulletManager.SpawnRange(BulletType.Basic_Red, 50f, 50f, 4, 5f, 60f, 90f, angle3, 0f, 20f);

						angle3 += 45;
					}

					break;
				}
				case 3: {
					timer4 += delta;

					if (timer4 > 0.2f) {
						timer4 -= 0.1f;
						BulletManager.SpawnSingle(BulletType.Basic_blue, 50f, 50f, 150f, angle4, 20f);

						angle4 += 10f;
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
