using Microsoft.Xna.Framework;


namespace Astro.AMath {
	// Contains all bullet related math
	static class BMath {
		// Constants
		public const float RADTODEG = 57.295779513082320876798154814105f;
		public const float DEGTORAD = 0.01745329251994329576923690768489f;
		public const float PI = 3.1415926535897932384626433832795f;
		public const double DBLRADTODEG = 57.295779513082320876798154814105d;
		public const double DBLDEGTORAD = 0.01745329251994329576923690768489d;
		public const double DBLPI = 3.1415926535897932384626433832795d;
		private const float VERYSMALL = 0.00001f;

		// Angle to coordinate convertions
		public static float DegreesToX(float degrees) {
			return (float)-System.Math.Sin(degrees * DEGTORAD);
		}

		public static float DegreesToY(float degrees) {
			return (float)System.Math.Cos(degrees * DEGTORAD);
		}

		public static Vector2 DegreesToVec(float degrees) {
			// Convert to radians
			degrees *= DEGTORAD;
			// Create vector
			Vector2 vector = new Vector2(-(float)System.Math.Sin(degrees), (float)System.Math.Cos(degrees));
#if DEBUG
			// Check for very small
			if (vector.X < VERYSMALL && vector.X > -VERYSMALL) vector.X = 0;
			if (vector.Y < VERYSMALL && vector.Y > -VERYSMALL) vector.Y = 0;
#endif
			// Return Vector
			return vector;
		}

		public static float RadiansToX(float radians) {
			return (float)-System.Math.Sin(radians);
		}

		public static float RadiansToY(float radians) {
			return (float)System.Math.Cos(radians);
		}

		public static Vector2 RadiansToVec(float radians) {
			// Create and return vector
			Vector2 vector = new Vector2(-(float)System.Math.Sin(radians), (float)System.Math.Cos(radians));
#if DEBUG
			// Check for very small
			if (vector.X < VERYSMALL && vector.X > -VERYSMALL) vector.X = 0;
			if (vector.Y < VERYSMALL && vector.Y > -VERYSMALL) vector.Y = 0;
#endif
			// Return Vector
			return vector;
		}

		public static float VecToDegrees(Vector2 vector) {
			return (float)System.Math.Atan2(vector.X, vector.Y) * RADTODEG;
		}

		public static float VecToRadians(Vector2 vector) {
			return (float)System.Math.Atan2(vector.Y, vector.X);
		}

		public static float XToWorld(float x) {
			return x / 100f * 600f;
		}

		public static float YToWorld(float y) {
			return y / 150f * 900f;
		}

		// Equals

		
		/// <summary>
		/// If two numbers are close to being equal then it returns true
		/// </summary>
		/// <param name="num1"> The first number to compare </param>
		/// <param name="num2"> The second number to compare </param>
		/// <param name="percision"> The maximum distance between the two numbers </param>
		/// <returns></returns>
		public static bool NearlyEqual(float num1, float num2, float percision = 0.01f) {
			return System.Math.Abs(num1 - num2) <= percision;
		}



	}

}
