using System.Collections.Generic;


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

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Bullet delegates
		public delegate void BulletUpdateDelegate(ref Bullet bullet);

		/// Dictionary for defining bullet delegates
		private Dictionary<BulletType, BulletUpdateDelegate> bulletUpdateDit;

		/// Adding and removing delegates
		public void AddUpdateDel(BulletType key, BulletUpdateDelegate del) {
			if (!bulletUpdateDit.ContainsKey(key))
				bulletUpdateDit.Add(key, del);
			else IO.Debug.LogError("There is already a delegate keyed by " + key);
		}
		public void RemoveUpdateDel(BulletType key) {
			if (bulletUpdateDit.ContainsKey(key))
				bulletUpdateDit.Remove(key);
			else IO.Debug.LogError("There is no delegate keyed by " + key);
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructor & Destructor

		/// <param name="bulletCount"> The Number of bullets to be allocated </param>
		public BulletObject(int bulletCount) {
			// Add the object
			bulletManager.AddBulletObject(this);

			// Set the array and count
			Count = bulletCount;
			buarray = new Bullet[Count];
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

		}

	}
}
