using System;
using Microsoft.Xna.Framework;
using Astro.AMath;

namespace Astro.Objects.BO {
	/// Bullets enums
	// Logic type
	enum BulletLogic {
		None = 0,
		MoveLinear,
		MoveRadial,
	}
	
	// Draw Shapes
	enum BulletDraw : int {
		// Shapes
		Circle = 0,
		FireBall,
	}

	// Extra Flags
	[Flags] enum BulletFlags : short {
		///Default = 0,
		Test0 = 1 << 0,
		Test1 = 1 << 1,
		Test2 = 1 << 2,
		Test3 = 1 << 3,
		Test4 = 1 << 4,
		Test5 = 1 << 5,
		Test6 = 1 << 6,
		Test7 = 1 << 7,
	}

	/// Creates shortcuts for objects using the BulletManager class
	struct Bullet {
		// Variables
		public BulletLogic logicType;
		public BulletDraw drawType;
		private BulletFlags Flags;
		public float TimeSinceAwake;
		public Vector2 position;
		public Vector2 linear;
		public Radial radial;
		public int LastCollision;
		public float ColSize;

		public bool HasFlags(BulletFlags flags) {
			return (Flags & flags) == flags;
		}
		public void SetFlags(BulletFlags flags) {
			Flags = flags;
		}
		public void AddFlags(BulletFlags flags) {
			Flags |= flags;
		}
		public void RemoveFlags(BulletFlags flags) {
			Flags &= ~flags;
		}
	}

}
