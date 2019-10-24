using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Astro.Physics;

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
		private Texture2D spriteTexture;
		private Rectangle spriteRect;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialization

		// Constriuctor that calls Base
		public Player() : base() {
			// Init singleton
			if (Singleton == null) Singleton = this;
			else Debug.Log("There are multiple players");

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
		}

		public override void Init() {
			Health = 10;
			Transform.Position = Vector2.Zero;
			Transform.Velocity = Vector2.Zero;
			Transform.Acceleration = Vector2.Zero;
		}

		public override void LoadContent() {
			spriteTexture = ContentLoader.Load<Texture2D>("TestSprite");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override void Update(float delta) {
			statemachine.Update(delta);
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

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Exit

		public override void Exit() {

		}
	}
}
