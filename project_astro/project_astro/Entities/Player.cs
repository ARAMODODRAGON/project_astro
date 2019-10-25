using Microsoft.Xna.Framework;
using Astro.Physics;
using Astro.Rendering;
using Astro.IO;

namespace Astro.Objects {
	class Player : Entity {
		// Singleton
		public static Player Singleton { get; private set; }

		// Player state enum
		public enum PlayerState : byte {
			Frozen		= 0,
			Normal		= 1,
			Slow		= 2,
		}
		// State machine
		public StateMachine<PlayerState> statemachine;

		// Other player state variables
		public int Health { get; private set; }

		// Player physics variables
		public CircleCollider circle;

		// Rendering
		private Sprite playersprite;

		// Input shortcuts
		private bool Up => Input.GetKey("Up");
		private bool Down => Input.GetKey("Down");
		private bool Left => Input.GetKey("Left");
		private bool Right => Input.GetKey("Right");

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialization

		// Constriuctor that calls Base
		public Player() : base() {
			// Init singleton
			if (Singleton == null) Singleton = this;
			else PrintError("There are multiple players");

			#region State Machine Init

			// Init state machine
			statemachine = new StateMachine<PlayerState>();
			statemachine.SetState(PlayerState.Normal);

			/// Add normal state
			statemachine.AddEntry(PlayerState.Normal, NormalEnter);
			statemachine.AddUpdate(PlayerState.Normal, NormalUpdate);
			statemachine.AddExit(PlayerState.Normal, NormalExit);

			/// Add slow state
			statemachine.AddEntry(PlayerState.Slow, SlowEnter);
			statemachine.AddUpdate(PlayerState.Slow, SlowUpdate);
			statemachine.AddExit(PlayerState.Slow, SlowExit);

			#endregion

			#region Collision Init
			
			// Create Collider
			circle = new CircleCollider(1f, Transform);

			// Set collision callbacks
			circle.OnCollisionEnter = OnCollisionEnter;
			circle.OnCollisionStay = OnCollisionStay;
			circle.OnCollisionExit = OnCollisionExit;

			#endregion

			// Init sprite|s
			playersprite = new Sprite(Transform);
		}

		// Init
		public override void Init() {
			Health = 10;
			Transform.Position = new Vector2(450f,450f);
			Transform.Scale = new Vector2(2f);
			Transform.Velocity = Vector2.Zero;
			Transform.Acceleration = Vector2.Zero;
		}

		// Load Content
		public override void LoadContent() {
			playersprite.LoadTexture("TestSprite");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override void Update(float delta) {
			if (Up || Down) {
				Transform.Velocity.Y = 0f;
				if (Down) Transform.Velocity.Y += 1000f;
				if (Up) Transform.Velocity.Y -= 1000f;
			} else {
				Transform.Velocity.Y = 0f;
			}
			if (Right || Left) {
				Transform.Velocity.X = 0f;
				if (Right) Transform.Velocity.X += 1000f;
				if (Left) Transform.Velocity.X -= 1000f;
			} else {
				Transform.Velocity.X = 0f;
			}

			Print((Transform.Velocity));

			Transform.PhysicsUpdate(delta);

			//statemachine.Update(delta);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// States

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
		// Collision

		private void OnCollisionEnter(ref CollisionData data) {

		}
		
		private void OnCollisionStay(ref CollisionData data) {

		}
		
		private void OnCollisionExit(ref CollisionData data) {

		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Rendering
		
		public override void Render() {
			playersprite.Render();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Exit

		public override void Exit() {

		}
	}
}
