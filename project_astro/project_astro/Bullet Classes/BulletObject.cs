using System.Collections.Generic;
using Microsoft.Xna.Framework; 

namespace Astro.Objects.BO {
	/// Contains a set of bullets
	class BulletObject {
		// Reference to the Bulletmanager
		private static Managers.BulletManager bulletManager => Managers.BulletManager.Singleton;

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
		public BulletDelegate UpdateBullet;
		public BulletDelegate RenderBullet;

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
		public bool Contains(BulletLogic type) {
			for (int i = 0; i < Count; i++) {
				// If they match then return true
				if (buarray[i].logicType == type) return true;
			}
			// If there wasnt one of that type then 
			return false;
		}

		public void Clear() {
			// Clears the bullets by setting them all to BulletType.None
			for (int i = 0; i < Count; i++) {
				buarray[i].logicType = 0;
			}
		}

		#region Spawning

		public void SpawnSingleAt(BulletFlags type, float direction, float speed, Vector2 position) {

		}

		

		#endregion

	}
}
