﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_astro {
	static class ContentLoader {
		public static T Load<T>(string assetName) => Game1.singleton.Content.Load<T>(assetName);
	}
}
