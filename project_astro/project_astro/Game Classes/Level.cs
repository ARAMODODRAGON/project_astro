using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_astro {
	abstract class Level {
		public static Level singleton { get; private set; }

		public Level() {
			if (singleton == null) singleton = this;
			else Debug.Log("The level singleton isn't null");
		}

		~Level() {
			if (singleton == this) singleton = null;
		}

		public abstract void Init();
		public abstract void LoadContent();
		public abstract void Update(float delta);
		public abstract void Exit();
	}
}
