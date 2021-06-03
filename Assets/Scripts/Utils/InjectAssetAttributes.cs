namespace System
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttributes : Attribute
    {
        public readonly string AssetName;

        public InjectAssetAttributes(string assetName = null)
        {
            AssetName = assetName;
        }
    }
}
