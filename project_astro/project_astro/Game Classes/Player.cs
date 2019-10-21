using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project_astro {
	class Player {
		// player state enum
		public enum PlayerState : byte {
			Frozen		= 0,
			Normal		= 1,
			Slow		= 2,
		}

		// player state
		public int playerHealth { get; private set; }
		public StateMachine<PlayerState> statemachine;

		// player physics variables
		public Vector2 position;
		public Vector2 velocity;
		public CircleCollider circle;

		// player sprite variables
		Texture2D sprite;

		public Player() {
			// init state machine
			statemachine = new StateMachine<PlayerState>();
			statemachine.SetState(PlayerState.Normal);

			/// add normal state
			statemachine.AddEntry(PlayerState.Normal, NormalEnter);
			statemachine.AddUpdate(PlayerState.Normal, NormalUpdate);
			statemachine.AddExit(PlayerState.Normal, NormalExit);

			/// add slow state
			statemachine.AddEntry(PlayerState.Slow, SlowEnter);
			statemachine.AddUpdate(PlayerState.Slow, SlowUpdate);
			statemachine.AddExit(PlayerState.Slow, SlowExit);
		}

		public void Init() {
			position = new Vector2(300, 200);
			velocity = Vector2.Zero;
			circle = new CircleCollider(10f);
			playerHealth = 10;
		}

		public void LoadContent() {
			sprite = ContentLoader.Load<Texture2D>("TestSprite");
		}

		public void Update(float delta) {
			statemachine.Update(delta);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		#region Normal State

		public void NormalEnter(PlayerState from) {

		}

		public void NormalUpdate(float delta) {

		}

		public void NormalExit(PlayerState to) {

		}

		#endregion

		#region Slow State

		public void SlowEnter(PlayerState from) {

		}

		public void SlowUpdate(float delta) {

		}

		public void SlowExit(PlayerState to) {

		}

		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		public void CollideWithBullet(BulletType type) {
			playerHealth -= 1;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		public void Render() {

		}

		public void Exit() {

		}
	}
}
