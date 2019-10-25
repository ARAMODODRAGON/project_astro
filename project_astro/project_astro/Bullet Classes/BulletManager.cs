using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Astro.Rendering;

namespace Astro.Objects.BO {
	// Contains all bullet related math
	static class BMath {

		public static float AngleToX(float angle) {
			return (float)-Math.Sin(angle * Math.PI / 180f);
		}

		public static float AngleToY(float angle) {
			return (float)Math.Cos(angle * Math.PI / 180f);
		}

		public static float XToWorld(float x) {
			return x / 100f * 600f;
		}

		public static float YToWorld(float y) {
			return y / 150f * 900f;
		}

	}

	/// The manager for all bullets in game
	class BulletManager {
		// singleton
		public static BulletManager Singleton { get; private set; }
		
		// The Bullet Object List
		private List<BulletObject> objects;

		// Texture
		private Texture2D bulletDefault;

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Initialization

		public void Init() {
			// Set singleton
			if (Singleton == null) Singleton = this;
			else IO.Debug.LogError("BulletManager Singleton was not null");

			// Init bullet object list
			objects = new List<BulletObject>();

			ClearBullets();
		}

		public static void ClearBullets() {
			// clears all bullets to BulletType.None
			for (int i = 0; i < Singleton.objects.Count; ++i) {
				Singleton.objects[i].Clear();
			}
		}

		public void LoadContent() {
			bulletDefault = ContentLoader.Load<Texture2D>("BulletTestSprite");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// BulletObject handling

		// Adding to / Removing from the list
		public void AddBulletObject(BulletObject obj) {
			// Add the bullet object
			objects.Add(obj);

		}
		public void RemoveBulletObject(BulletObject obj) {
			objects.Remove(obj);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

		public void Update(float delta) {
			// Update each object
			for (int i = 0; i < objects.Count; i++) {
				// Update the bullets within the object
				// TODO: add bullet update logic
			}
		}

		// TODO: write functionality and add player argument
		public void CollideAgainstPlayer() {

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		#region Rendering Bullets

		public void Render() {
			Rectangle rect = new Rectangle();
			Vector2 origin = new Vector2(75f);
			// render each bullet
			for (int i = 0; i < SIZE; i++) {
				//set the rect
				rect.X = (int)(bullets[i].XPos);
				rect.Y = (int)(bullets[i].YPos);
				rect.Width = (int)bullets[i].ColSize;
				rect.Height = (int)bullets[i].ColSize;
				switch (bullets[i].Type) {
					case BulletType.None: break;
					case BulletType.Basic:
						//draw the basic bullet using the 'bulletDefault' Texture2D
						Renderer.NormalDraw(bulletDefault, rect, null, Color.White, bullets[i].Radians, origin, SpriteEffects.None, 0f);
						break;
					case BulletType.Basic_Red:
						//draw the basic bullet using the 'bulletDefault' Texture2D
						Renderer.NormalDraw(bulletDefault, rect, null, Color.Red, bullets[i].Radians, origin, SpriteEffects.None, 0f);
						break;
					case BulletType.Basic_blue:
						//draw the basic bullet using the 'bulletDefault' Texture2D
						Renderer.NormalDraw(bulletDefault, rect, null, Color.Blue, bullets[i].Radians, origin, SpriteEffects.None, 0f);
						break;
					default: IO.Debug.Log("Couldn't render bullet type: " + bullets[i].Type); break;
				}
			}
		}

		#endregion

		public void Exit() {
			//remove singleton
			if (Singleton == this) Singleton = null;
		}
	}
}
