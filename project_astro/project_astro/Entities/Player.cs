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
			Frozen = 0,
			Normal = 1,
			MoveToCenter = 2,
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

		// Movement constants
		private const float max_fastspeed = 500f;
		private const float max_slowspeed = 100f;
		private const float acceleration = 5000f;
		private const float deceleration = 10000f;

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

			/// Add Normal state
			statemachine.AddEntry(PlayerState.Normal, NormalEnter);
			statemachine.AddUpdate(PlayerState.Normal, NormalUpdate);
			statemachine.AddExit(PlayerState.Normal, NormalExit);

			/// Add MoveToCenter state
			statemachine.AddEntry(PlayerState.MoveToCenter, CenterEnter);
			statemachine.AddUpdate(PlayerState.MoveToCenter, CenterUpdate);
			statemachine.AddExit(PlayerState.MoveToCenter, CenterExit);

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
			playersprite.Pivot = new Vector2(0.5f, 0.5f);
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
			playersprite.LoadTexture("PlayerSprite1");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override void Update(float delta) {
			statemachine.Update(delta);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Math help

		/// <summary>
		/// If two numbers are close to being equal then it returns true
		/// </summary>
		/// <param name="num1"> The first number to compare </param>
		/// <param name="num2"> The second number to compare </param>
		/// <param name="percision"> The maximum distance between the two numbers </param>
		/// <returns></returns>
		private bool NearlyEqual(float num1, float num2, float percision = 0.01f) { 
			return System.Math.Abs(num1 - num2) <= percision;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// States

		#region Normal State

		public void NormalEnter(PlayerState from) {

		}

		public void NormalUpdate(float delta) {
			//Print("{accel*delta = " + (acceleration * delta) + "} {decel*delta = " + (deceleration * delta) + "}");
			// Do movement
			/// Grab velocity by reference
			ref Vector2 vel = ref Transform.Velocity;
			/// Grab speed(depends on shift) and percision
			float speed = (Shift ? max_slowspeed : max_fastspeed);
			float percision = 100f;

			#region movement code

			if (Up != Down) {                                                       ///// if moving Up xor Down
				if (Up) {															//// if moving up
					if (NearlyEqual(vel.Y, -speed, percision)) vel.Y = -speed;      /// if moving at the same speed
					else if (vel.Y > -speed) vel.Y -= acceleration * delta;         /// if moving slower > accelerate
					else if (vel.Y < -speed) vel.Y += deceleration * delta;         /// if moving faster > decelerate
				}
				if (Down) {															//// if moving down
					if (NearlyEqual(vel.Y, speed, percision)) vel.Y = speed;        /// if moving at the same speed
					else if (vel.Y < speed) vel.Y += acceleration * delta;          /// if moving slower > accelerate
					else if (vel.Y > speed) vel.Y -= deceleration * delta;          /// if moving faster > decelerate
				}
			} else {                                                                //// if not moving Up or Down
				if (NearlyEqual(vel.Y, 0f, percision)) vel.Y = 0;                   /// if not moving
				else if (vel.Y > 0) vel.Y -= deceleration * delta;                  /// if moving down   > decelerate
				else if (vel.Y < 0) vel.Y += deceleration * delta;                  /// if moving up     > decelerate
			}


			if (Left != Right) {													///// if moving left xor right
				if (Left) {															//// if moving left
					if (NearlyEqual(vel.X, -speed, percision)) vel.X = -speed;      /// if moving at the same speed
					else if (vel.X > -speed) vel.X -= acceleration * delta;         /// if moving slower > accelerate
					else if (vel.X < -speed) vel.X += deceleration * delta;         /// if moving faster > decelerate
				}
				if (Right) {														//// if moving right
					if (NearlyEqual(vel.X, speed, percision)) vel.X = speed;        /// if moving at the same speed
					else if (vel.X < speed) vel.X += acceleration * delta;          /// if moving slower > accelerate
					else if (vel.X > speed) vel.X -= deceleration * delta;          /// if moving faster > decelerate
				}
			} else {                                                                //// if not moving Left or Right
				if (NearlyEqual(vel.X, 0f, percision)) vel.X = 0;                   /// if not moving
				else if (vel.X > 0) vel.X -= deceleration * delta;                  /// if moving right  > decelerate
				else if (vel.X < 0) vel.X += deceleration * delta;                  /// if moving left   > decelerate
			}

			#endregion

			// Do physics
			Transform.PhysicsUpdate(delta);
		}

		public void NormalExit(PlayerState to) {

		}

		#endregion

		#region Slow State

		public void CenterEnter(PlayerState from) {

		}

		public void CenterUpdate(float delta) {

		}

		public void CenterExit(PlayerState to) {

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
