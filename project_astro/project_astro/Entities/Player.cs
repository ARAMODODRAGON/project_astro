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
		private bool Shift => Input.GetKey("Shift");

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
			Transform.Scale = new Vector2(1f);
			playersprite.Pivot = new Vector2(0.5f);
			ResetPosition();
		}

		public void ResetPosition() {
			// Used to reset the position of just the player
			//Transform.Position = new Vector2(Camera.Size.X / 2, 700f);
			Transform.Position = new Vector2(0, 0);
			Transform.Velocity = Vector2.Zero;
		}

		// Load Content
		public override void LoadContent() {
			playersprite.LoadTexture("TestSprite");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override void Update(float delta) {
			

			//Camera.Center = Transform.Position.ToPoint();
			//Print(Camera.Size + " and " + Transform.Position.ToPoint());

			//statemachine.Update(delta);
			
			Transform.PhysicsUpdate(delta);
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
