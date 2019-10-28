using System;
using Microsoft.Xna.Framework;
using Astro.AMath;

namespace Astro.Objects.BO {
	/// The bullets enum
	enum BulletType {
		None = 0,
		Basic,
		Basic_Red,
		Basic_blue,
	}

	/// Creates shortcuts for objects using the BulletManager class
	struct Bullet {
		public BulletType Type;
		public int IntType {
			get => (int)Type;
			set => Type = (BulletType)value;
		}
		public float TimeSinceAwake;
		public Vector2 cordPos;
		public Radial radial;
		public int LastCollision;
		public float ColSize;
	}

}
