using System.Collections.Generic;
using Astro.Objects.Bullets;

namespace Astro.Managers {
	/// The manager for all bullets in game
	class BulletManager : Manager {
		// singleton
		public static BulletManager Singleton { get; private set; }

		// The BulletObject List
		private List<BulletObject> bObjects;

		// The BulletData class used to update and render bullets
		private BulletData bulletData;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialization

		public override void Init() {
			// Set singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.LogError("BulletManager Singleton was not null");

			// Init bullet object list
			bObjects = new List<BulletObject>();

			// Init bullet data objecct
			bulletData = new BulletData();
			bulletData.Init();

			ClearBullets();
		}

		public override void LoadContent() {
			// Load the bullet data content
			bulletData.LoadContent();
		}

		public override void Exit() {
			// Exit
			bulletData.Exit();
		}

		~BulletManager() {
			// Remove singleton
			if (Singleton == this) Singleton = null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// BulletObject handling

		// Adding to / Removing from the list
		public void AddBulletObject(BulletObject obj) {
			// Add the bullet object
			bObjects.Add(obj);

		}
		public void RemoveBulletObject(BulletObject obj) {
			bObjects.Remove(obj);
		}

		// Clearing bullets
		public static void ClearBullets() {
			// Clears all bullets to BulletType.None
			for (int i = 0; i < Singleton.bObjects.Count; ++i) {
				Singleton.bObjects[i].Clear();
			}
		}

		/*
		#region Bullet Creation

		private static void SpawnSingleAt(int index, BulletType type, float xpos, float ypos, float speed, float angle, float colRadius) {
			Singleton.bullets[index].Type = type;    /// set the type
			Singleton.bullets[index].LastCollision = -1;           /// set the last collision to -1 to reperesent none
			Singleton.bullets[index].TimeSinceAwake = 0f;         /// set the time since awake to 0
			Singleton.bullets[index].Degrees = angle;  /// set the direction
			Singleton.bullets[index].Speed = speed;      /// set the initial speed
			Singleton.bullets[index].ColSize = colRadius;     /// set the radius
			Singleton.bullets[index].XPos = xpos;       /// set the x position
			Singleton.bullets[index].YPos = ypos;       /// set the y position
		}

		/// <summary>
		/// Creates a single bullet
		/// </summary>
		/// <param name="type">Type of bullet to spawn</param>
		/// <param name="xpos">The position of the bullet</param>
		/// <param name="ypos">The position of the bullet</param>
		/// <param name="speed">The initial speed at which it travels</param>
		/// <param name="direction">The direction that it faces</param>
		/// <param name="radius">The radius of the bullets collider</param>
		/// <returns>Returns whether the bullet could spawn or not</returns>
		public static bool SpawnSingle(BulletType type, float xpos, float ypos, float speed, float direction, float radius = 1f) {
			// redo the positions
			xpos = BMath.XToWorld(xpos);
			ypos = BMath.YToWorld(ypos);

			// first find an unused slot in the array
			int index = 0;
			while (index < SIZE && Singleton.bullets[index].Type != 0) ++index; /// while index is valid and bullet type is 0 (BulletType.None)
			// if there isnt one then return false
			if (index == SIZE) {
				IO.Debug.Log("The number of bullets have reached the max count(" + SIZE + ") of" + type);
				return false;
			}

			// if there is an empty slot spawn one bullet
			SpawnSingleAt(index, type, xpos, ypos, speed, direction, radius);

			return true;
		}

		/// <summary>
		/// Spawns a circle of bullets around a position
		/// </summary>
		/// <param name="type">Type of bullet to spawn</param>
		/// <param name="xCenter">Center of the circle</param>
		/// <param name="yCenter">Center of the circle</param>
		/// <param name="count">Number of bullets to spawn</param>
		/// <param name="circleRadius">The radius of the circle</param>
		/// <param name="speed">The initial speed of the bullets</param>
		/// <param name="startingRotation">The initial rotation of the first bullet</param>
		/// <param name="outDirection">The direction which the bullets will face relative to the position on the circle</param>
		/// <param name="bulletRadius">The size of each bulllets collider</param>
		/// <returns>Returns whether or not all bullets were spawned</returns>
		public static bool SpawnCircle(BulletType type, float xCenter, float yCenter, int count, float circleRadius, float speed,
									   float startingRotation, float outDirection = 0f, float bulletRadius = 1f) {
			int index = 0;
			int spawnedCount = 0;
			float anglediff = 360f / count;
			float relangle;

			xCenter = BMath.XToWorld(xCenter);
			yCenter = BMath.YToWorld(yCenter);

			// start looping through and creating each bullet
			while (index < SIZE && spawnedCount != count) { /// while the index is valid and hasn't spawned all the bullets

				// when there is an empty slot create a bullet
				if (Singleton.bullets[index].Type == 0) {
					// set the relative angle of this bullet
					/// used for the position on the circle
					/// or for its rotation
					relangle = anglediff * spawnedCount;
					// spawn bullet
					SpawnSingleAt(index, type, BMath.XToWorld(BMath.AngleToX(startingRotation + relangle)) * circleRadius + xCenter,
						BMath.YToWorld(BMath.AngleToY(startingRotation + relangle)) * circleRadius + yCenter,
						speed, outDirection + relangle + startingRotation, bulletRadius);
					//increase the number of spawned bullets
					++spawnedCount;
				}

				// goto next index
				++index;
			}

			// if it failed to spawn all the bullets return false
			if (spawnedCount < count) {
				IO.Debug.Log("Failed to spawn " + (count - spawnedCount) + " in cirle of BulletType: " + type);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Spawns the bullets in a range of a circle
		/// </summary>
		/// <param name="type">Type of bullet to spawn</param>
		/// <param name="xCenter">Center of the circle</param>
		/// <param name="yCenter">Center of the circle</param>
		/// <param name="count">Number of bullets to spawn</param>
		/// <param name="circleRadius">The radius of the circle</param>
		/// <param name="speed">The initial speed of the bullets</param>
		/// <param name="range">The the range which bullets will spawn</param>\
		/// <param name="rotation">The initial rotation of the first bullet</param>
		/// <param name="outDirection">The direction which the bullets will face relative to the position on the circle</param>
		/// <param name="bulletRadius">The size of each bulllets collider</param>
		/// <returns></returns>
		public static bool SpawnRange(BulletType type, float xCenter, float yCenter, int count, float circleRadius, float speed, float range,
									  float rotation, float outDirection = 0f, float bulletRadius = 0f) {
			int index = 0;
			int spawnedCount = 0;
			float anglediff = range / count; // distance between bullets
			float relangle;

			xCenter = BMath.XToWorld(xCenter);
			yCenter = BMath.YToWorld(yCenter);

			// start looping through and creating each bullet
			while (index < SIZE && spawnedCount < count) { /// while the index is valid and hasn't spawned all the bullets

				// when there is an empty slot create a bullet
				if (Singleton.bullets[index].Type == 0) {
					// set the relative angle of this bullet
					/// used for the position on the circle
					/// and for its rotation
					relangle = anglediff * spawnedCount;
					// spawn bullet
					SpawnSingleAt(index, type, BMath.AngleToX(rotation + relangle) * circleRadius + xCenter,
						BMath.AngleToY(rotation + relangle) * circleRadius + yCenter, speed, outDirection + relangle + rotation, bulletRadius);
					//increase the number of spawned bullets
					++spawnedCount;
				}

				// goto next index
				++index;
			}


			return true;
		}

		#endregion
		*/

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Logic

		public override void Update(float delta) {
			// Update each object
			for (int i = 0; i < bObjects.Count; i++) {
				// if it fails to find a delegate then call update in the BulletData object
				if (bObjects[i].UpdateBullet == null)
					bulletData.UpdateBullet(bObjects[i], delta);
				else {
					for (int j = 0; j < bObjects[i].Count; j++) {
						bObjects[i].UpdateBullet.Invoke(ref bObjects[i][j], delta);
					}
				}
			}
		}

		public void CollideAgainstPlayer() {
			// TODO: write functionality and add player argument
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Rendering Bullets

		public override void Render() {
			for (int i = 0; i < bObjects.Count; i++) {
				if (bObjects[i].RenderBullet == null) {
					bulletData.RenderBullet(bObjects[i]);
				} else {
					for (int j = 0; j < bObjects[i].Count; j++) {
						bObjects[i].RenderBullet.Invoke(ref bObjects[i][j]);
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Collision

		public void DoCollision() {
			for (int i = 0; i < bObjects.Count; i++) {
				Objects.EntityManager.Singleton.DoCollisionsWithBullets(bObjects[i]);
			}
		}

	}
}
