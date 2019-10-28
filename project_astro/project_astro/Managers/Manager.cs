using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Managers {
	abstract class Manager : Interfaces.IScriptable {

		public abstract void Init();
		public virtual void LoadContent() { }
		public abstract void Render();
		public abstract void Update(float delta);
		public abstract void Exit();
	}
}
