
namespace Astro {
	static class ContentLoader {
		public static T Load<T>(string assetName) => Game1.Singleton.Content.Load<T>(assetName);
	}
}
