using Microsoft.Xna.Framework;
using Astro.Physics;
using Astro.Rendering;
using Astro.IO;
using Astro.AMath;
using Astro.Objects.Bullets;

namespace Astro.Objects {
	class Player : Entity {
		// Singleton
		public static Player Singleton { get; private set; }

		// Player state stuff
		/// Player state enum
		public enum PlayerState : byte {
			Frozen = 0,
			Normal = 1,
			MoveToCenter = 2,
		}
		/// State machine
		public StateMachine<PlayerState> statemachine;

		// Rendering
		private Sprite playersprite;

		// Bullets
		BulletObject bObject;
		private float shootTimer;
		private const float shootDelay = 0.1f;

		// Input shortcuts
		private bool Up => Input.GetKey("Up");
		private bool Down => Input.GetKey("Down");
		private bool Left => Input.GetKey("Left");
		private bool Right => Input.GetKey("Right");
		private bool Shift => Input.GetKey("Shift");
		private bool Shoot => Input.GetKey("Z");

		// Movement constants
		private const float max_fastspeed = 500f;
		private const float max_slowspeed = 100f;
		private const float acceleration = 5000f;
		private const float deceleration = 10000f;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialization

		// Constriuctor that calls Base
		public Player() : base(100) {

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

			// Init sprite|s
			playersprite = new Sprite(transform);
		}

		// Init
		public override void Init() {
			// Init singleton
			if (Singleton == null) Singleton = this;
			else PrintError("There are multiple players");

			Health = 10f;
			shootTimer = 0f;
			transform.Scale = new Vector2(1f);
			playersprite.Origin = new Vector2(0.5f, 0.5f);
			ResetPosition();
			bObject = new Bullets.BulletObject(256, true);
			
			EntityManager.Singleton.SetPlayer(this);
		}
		
		public override void Exit() {
			if (Singleton == this) Singleton = null;

			EntityManager.Singleton.RemovePlayer();
		}

		public void ResetPosition() {
			// Used to reset the position of just the player
			//Transform.Position = new Vector2(Camera.Size.X / 2, 700f);
			transform.Position = new Vector2(0, 0);
			transform.Velocity = Vector2.Zero;
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
		// States

		#region Normal State

		public void NormalEnter(PlayerState from) {

		}

		public void NormalUpdate(float delta) {
			// Do Movement
			/// Grab velocity by reference
			ref Vector2 vel = ref transform.Velocity;
			/// Grab speed(depends on shift) and percision
			float speed = (Shift ? max_slowspeed : max_fastspeed);
			float percision = 100f;

			#region movement code

			if (Up != Down) {															///// if moving Up xor Down
				if (Up) {																//// if moving up
					if (BMath.NearlyEqual(vel.Y, -speed, percision)) vel.Y = -speed;	/// if moving at the same speed
					else if (vel.Y > -speed) vel.Y -= acceleration * delta;				/// if moving slower > accelerate
					else if (vel.Y < -speed) vel.Y += deceleration * delta;				/// if moving faster > decelerate
				}
				if (Down) {																//// if moving down
					if (BMath.NearlyEqual(vel.Y, speed, percision)) vel.Y = speed;		/// if moving at the same speed
					else if (vel.Y < speed) vel.Y += acceleration * delta;				/// if moving slower > accelerate
					else if (vel.Y > speed) vel.Y -= deceleration * delta;				/// if moving faster > decelerate
				}
			} else {																	//// if not moving Up or Down
				if (BMath.NearlyEqual(vel.Y, 0f, percision)) vel.Y = 0;					/// if not moving
				else if (vel.Y > 0) vel.Y -= deceleration * delta;						/// if moving down   > decelerate
				else if (vel.Y < 0) vel.Y += deceleration * delta;						/// if moving up     > decelerate
			}


			if (Left != Right) {														///// if moving left xor right
				if (Left) {																//// if moving left
					if (BMath.NearlyEqual(vel.X, -speed, percision)) vel.X = -speed;	/// if moving at the same speed
					else if (vel.X > -speed) vel.X -= acceleration * delta;				/// if moving slower > accelerate
					else if (vel.X < -speed) vel.X += deceleration * delta;				/// if moving faster > decelerate
				}
				if (Right) {															//// if moving right
					if (BMath.NearlyEqual(vel.X, speed, percision)) vel.X = speed;		/// if moving at the same speed
					else if (vel.X < speed) vel.X += acceleration * delta;				/// if moving slower > accelerate
					else if (vel.X > speed) vel.X -= deceleration * delta;				/// if moving faster > decelerate
				}
			} else {																	//// if not moving Left or Right
				if (BMath.NearlyEqual(vel.X, 0f, percision)) vel.X = 0;					/// if not moving
				else if (vel.X > 0) vel.X -= deceleration * delta;						/// if moving right  > decelerate
				else if (vel.X < 0) vel.X += deceleration * delta;						/// if moving left   > decelerate
			}

			#endregion

			/// Do physics
			transform.PhysicsUpdate(delta);

			// Do Shoot
			if (Shoot) shootTimer += delta;
			if (shootTimer > shootDelay) {
				shootTimer -= shootDelay;

				bObject.SpawnSingleAt(BulletLogic.MoveLinear, BulletDraw.Circle, Radial.UP, transform.Position, Color.White);
			}
		}

		public void NormalExit(PlayerState to) {

		}

		#endregion

		#region Center State

		public void CenterEnter(PlayerState from) {

		}

		public void CenterUpdate(float delta) {

		}

		public void CenterExit(PlayerState to) {

		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Non state specific

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Collision

		protected override void OnCollision() {
			base.OnCollision();


		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Rendering

		public override void Render() {
			playersprite.Render();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
