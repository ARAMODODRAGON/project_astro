using System.Collections.Generic;


namespace Astro.Objects.BO {
	/// Contains a set of bullets
	class BulletObject {
		// Reference to the Bulletmanager
		private static BulletManager bmanager => BulletManager.Singleton;

		// The Bullet Array
		/// <summary> The array of bullets </summary>
		public Bullet[] bullets;
		/// <summary> The maximum number of bullets that can be created by this bullet object </summary>
		public readonly int COUNT;

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
		// Consrtuctor

		/// <param name="bulletCount"> The Number of bullets to be allocated </param>
		public BulletObject(int bulletCount) {
			// TODO: add BulletObject to the bullet manager


		}
	}
}
