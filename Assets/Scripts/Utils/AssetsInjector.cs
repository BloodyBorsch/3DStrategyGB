using System;
using System.Reflection;


namespace Utils
{
    public static class AssetsInjector
    {
        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            var fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach(var field in fields)
            {
                var injectAssetAttribute = field.GetCustomAttribute(typeof(InjectAssetAttributes)) as InjectAssetAttributes;                

                if (injectAssetAttribute != null)
                {
                    var prefab = context.GetAsset(injectAssetAttribute.AssetName);                    
                    field.SetValue(target, prefab);
                }
            }

            return target;
        }
    }
}
