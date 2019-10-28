using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Astro.Objects.BO {
	/// Contains a set of bullets
	class BulletObject {
		// Reference to the Bulletmanager
		private static BulletManager bulletManager => BulletManager.Singleton;

		// The Bullet Array
		private Bullet[] buarray; /// = new Bullet[Count];

		// The Bullet count
		/// <summary> The maximum number of bullets that can be created by this bullet object </summary>
		public int Count { get; private set; }

		// Says if enemies should collide with the bullets
		public bool CollidesWithEnemies { get; set; }
		public bool DoCollision { get; set; }

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Bullet delegates
		public delegate void BulletDelegate(ref Bullet bullet);

		/// Dictionary for defining bullet delegates
		private Dictionary<BulletType, BulletDelegate> updateDel;
		private Dictionary<BulletType, BulletDelegate> renderDel;

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor & Destructor

		/// <param name="bulletCount"> The Number of bullets to be allocated </param>
		public BulletObject(int bulletCount, bool collidesWithEnemies = false) {
			// Add the object
			bulletManager.AddBulletObject(this);

			// Set the array and count
			Count = bulletCount;
			buarray = new Bullet[Count];

			// Set if it should collide with enemies instead of the player
			CollidesWithEnemies = collidesWithEnemies;
			DoCollision = true;

			// Create Dictionaries
			updateDel = new Dictionary<BulletType, BulletDelegate>();
			renderDel = new Dictionary<BulletType, BulletDelegate>();
		}

		~BulletObject() {
			// Remove this object
			bulletManager.RemoveBulletObject(this);
		}
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Bullet handling

		// Property for accessing bullets
		public ref Bullet this[int index] => ref buarray[index];

		// Checks if the array contains a bullet of this type
		public bool Contains(BulletType type) {
			for (int i = 0; i < Count; i++) {
				// If they match then return true
				if (buarray[i].Type == type) return true;
			}
			// If there wasnt one of that type then 
			return false;
		}

		public void Clear() {
			// Clears the bullets by setting them all to BulletType.None
			for (int i = 0; i < Count; i++) {
				buarray[i].Type = BulletType.None;
			}
		}

		#region Spawning

		public void SpawnSingleAt(BulletType type, float direction, float speed, Vector2 position) {

		}

		

		#endregion

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Bullet delegate

		// Adding and removing delegates
		public void AddUpdateDel(BulletType key, BulletDelegate del) {
			if (!updateDel.ContainsKey(key))
				updateDel.Add(key, del);
			else IO.Debug.LogError("There is already a delegate keyed by " + key);
		}
		public void RemoveUpdateDel(BulletType key) {
			if (updateDel.ContainsKey(key))
				updateDel.Remove(key);
			else IO.Debug.LogError("There is no delegate keyed by " + key);
		}
		public void AddRenderDel(BulletType key, BulletDelegate del) {
			if (!renderDel.ContainsKey(key))
				renderDel.Add(key, del);
			else IO.Debug.LogError("There is already a delegate keyed by " + key);
		}
		public void RemoveRenderDel(BulletType key) {
			if (renderDel.ContainsKey(key))
				renderDel.Remove(key);
			else IO.Debug.LogError("There is no delegate keyed by " + key);
		}

		// Calling delegates
		/// <summary> Tries to call the update corisponding to that key. Returns false if it failed </summary>
		public bool TryCallUpdate(int index) {
			// If it does not contain that key return false
			if (!updateDel.ContainsKey(buarray[index].Type))
				return false;

			// Invoke
			updateDel[buarray[index].Type].Invoke(ref buarray[index]);

			// Return true
			return true;
		}
		/// <summary> Tries to call the render corisponding to that key. Returns false if it failed </summary>
		public bool TryCallRender(int index) {
			// If it does not contain that key return false
			if (!renderDel.ContainsKey(buarray[index].Type))
				return false;

			// Invoke
			renderDel[buarray[index].Type].Invoke(ref buarray[index]);

			// Return true
			return true;
		}
	}
}
