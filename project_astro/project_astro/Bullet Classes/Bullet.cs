using System;
using Microsoft.Xna.Framework;

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
		public Vector2 Position;
		public float XPos {
			get => Position.X;
			set => Position.X = value;
		}
		public float YPos {
			get => Position.Y;
			set => Position.Y = value;
		}
		public float Degrees;
		public float Radians {
			get => MathHelper.ToRadians(Degrees);
			set => Degrees = MathHelper.ToDegrees(value);
		}
		public float Speed;
		public Vector2 Velocity {
			get => new Vector2((float)-Math.Sin(Radians), (float)Math.Cos(Radians)) * Speed;
			set {
				Radians = (float)Math.Atan2(value.Y, value.X);
				Speed = value.Length();
			}
		}
		public int LastCollision;
		public float ColSize;
	}

}
