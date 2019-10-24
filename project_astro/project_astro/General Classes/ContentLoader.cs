
namespace Astro {
	static class ContentLoader {
		public static T Load<T>(string assetName) => Game1.singleton.Content.Load<T>(assetName);
	}
}
