using System;
using Microsoft.Xna.Framework;

namespace project_astro {
	///the bullets enum
	enum BulletType {
		None = 0,
		Directional,
	}

	///creates shortcuts for objects using the BulletManager class
	static class Bullet {
		private static int SIZE => BulletManager.SIZE;
		private static ref int[,] bulletIntData => ref BulletManager.singleton.bulletIntData;
		private static ref float[,] bulletFloatData => ref BulletManager.singleton.bulletFloatData;

		#region Bullet Data & Math

		private static float AngleToX(float angle) {
			return (float)-Math.Sin(angle);
		}

		private static float AngleToY(float angle) {
			return (float)-Math.Cos(angle);
		}

		#endregion

		#region Bullet Creation

		private static void SpawnSingleAt(int index, BulletType type, float xpos, float ypos, float speed, float direction, float colRadius) {
			bulletIntData[index, 0] = (int)type;    /// set the type
			bulletIntData[index, 1] = -1;           /// set the last collision to -1 to reperesent none
			bulletFloatData[index, 0] = 0f;         /// set the time since start to 0
			bulletFloatData[index, 1] = direction;  /// set the direction
			bulletFloatData[index, 2] = speed;      /// set the initial speed
			bulletFloatData[index, 3] = colRadius;     /// set the radius
			bulletFloatData[index, 4] = xpos;       /// set the x position
			bulletFloatData[index, 5] = ypos;       /// set the y position
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
			// first find an unused slot in the array
			int index = 0;
			while (index < SIZE && bulletIntData[index, 0] != 0) ++index; /// while index is valid and bullet type is 0 (BulletType.None)
			// if there isnt one then return false
			if (index == SIZE) {
				Debug.Log("The number of bullets have reached the max count(" + SIZE + ") of" + type);
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
			float relangle = 0f;

			// start looping through and creating each bullet
			while (index < SIZE && spawnedCount != count) { /// while the index is valid and hasn't spawned all the bullets

				// when there is an empty slot create a bullet
				if (bulletIntData[index, 0] == 0) {
					// set the relative angle of this bullet
					/// used for the position on the circle
					/// or for its rotation
					relangle = anglediff * spawnedCount;
					// spawn bullet
					SpawnSingleAt(index, type, AngleToX(startingRotation + relangle) * circleRadius,
						AngleToY(startingRotation + relangle) * circleRadius, speed, outDirection + relangle, bulletRadius);
					//increase the number of spawned bullets
					++spawnedCount;
				}

				// goto next index
				++index;
			}

			// if it failed to spawn all the bullets return false
			if (spawnedCount < count) {
				Debug.Log("Failed to spawn " + (count - spawnedCount) + " in cirle of BulletType: " + type);
				return false;
			}

			return true;
		}

		#endregion

	}

	///the manager for all bullets in game
	class BulletManager {
		// singleton
		public static BulletManager singleton { get; private set; }

		#region The Bullet List

		// bullet list
		public const int SIZE = 100;
		public int[,] bulletIntData;
		//  [SIZE, 2]
		/// [0] = type
		/// [1] = last collision
		public float[,] bulletFloatData;
		//  [SIZE, 6]
		/// [0] = time since creation
		/// [1] = direction in degrees
		/// [2] = speed
		/// [3] = radius of collider
		/// [4] = X position
		/// [5] = Y position

		// accessing bullets by properties
		/// you would set the index to the bullet you want access to 
		private int Index = 0;
		/// then use the properties
		private ref int Type => ref bulletIntData[Index, 0];
		private ref int LastEnemy => ref bulletIntData[Index, 1];
		private ref float TimeSinceStart => ref bulletFloatData[Index, 0];
		private ref float Direction => ref bulletFloatData[Index, 1];
		private ref float Speed => ref bulletFloatData[Index, 2];
		private ref float CircleRadius => ref bulletFloatData[Index, 3];
		private ref float XPos => ref bulletFloatData[Index, 4];
		private ref float YPos => ref bulletFloatData[Index, 5];

		// accessing bullets by functions
		//private ref int Type(int index) => ref bulletIntData[index, 0];

		#endregion

		#region Initialization

		public void Init() {
			//set singleton
			if (singleton == null) singleton = this;

			bulletIntData = new int[SIZE, 2];
			bulletFloatData = new float[SIZE, 6];
		}

		public void LoadContent() {

		}

		#endregion

		#region Math

		// TODO: write all math functions

		private static float AngleToX(float angle) {
			return (float)-Math.Sin(angle);
		}

		private static float AngleToY(float angle) {
			return (float)-Math.Cos(angle);
		}


		#endregion

		#region Bullet Logic

		private void DestroyBullet(int index) {
			bulletIntData[index, 0] = 0;
		}

		private void DestroyBullet() {

		}

		public void Update(float delta) {

		}

		#endregion

		#region Rendering Bullets

		public void Render() {

		}

		#endregion

		public void Exit() {
			//remove singleton
			if (singleton == this) singleton = null;
		}
	}
}
