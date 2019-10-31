using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Objects {
	class Enemy : Entity {

		protected Bullets.BulletObject BObject => EntityManager.Singleton.mainBullletObject;

		public Enemy(float maxHealth) : base(maxHealth) {
			
		}
		
		public override void Init() {
			base.Init();

			EntityManager.Singleton.AddEnemy(this);
		}

		public override void Exit() {
			base.Exit();
			
			EntityManager.Singleton.RemoveEnemy(this);
		}

		public override void LoadContent() {
			base.LoadContent();
		}

		public override void Render() {
			base.Render();
		}

		public override string ToString() {
			return base.ToString();
		}

		public override void Update(float delta) {
			base.Update(delta);
		}

		protected override void OnCollision() {
			base.OnCollision();
		}
	}
}
