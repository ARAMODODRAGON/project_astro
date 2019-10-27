using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Interfaces {
	interface IScriptable {
		void Init();
		void LoadContent();
		void Update(float delta);
		void Render();
		void Exit();
	}
}
