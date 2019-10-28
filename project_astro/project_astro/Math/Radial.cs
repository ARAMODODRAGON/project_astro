using Microsoft.Xna.Framework;

namespace Astro.AMath {
	/// Used for bullet math
	struct Radial {
		// Static readonly
		/// <summary> Shorthand for Radial(270) </summary>
		public static readonly Radial UP;
		/// <summary> Shorthand for Radial(90) </summary>
		public static readonly Radial DOWN;
		/// <summary> Shorthand for Radial(180) </summary>
		public static readonly Radial LEFT;
		/// <summary> Shorthand for Radial(0) </summary>
		public static readonly Radial RIGHT;
		
		// Static Constructor to init radonly variables
		static Radial() {
			UP = new Radial(180f);
			DOWN = new Radial(0);
			LEFT = new Radial(90f);
			RIGHT = new Radial(270);
		}

		// Fields
		public float Degrees;
		public float Length;

		// Properties
		public float Radians {
			get => Degrees * BMath.DEGTORAD;
			set => Degrees = value * BMath.RADTODEG;
		}

		// Constructors
		public Radial(float degrees = 0f, float length = 1f) {
			Degrees = degrees;
			Length = length;
		}
		public Radial(Vector2 direction) {
			Length = direction.Length();
			Degrees = BMath.VecToDegrees(direction);
		}

		// Functions
		public Vector2 ToVector2() {
			return BMath.DegreesToVec(Degrees) * Length;
		}

		// Inherited
		public override string ToString() {
			return "{Degrees: " + Degrees + "; Speed: " + Length + "}";
		}
	}
}
