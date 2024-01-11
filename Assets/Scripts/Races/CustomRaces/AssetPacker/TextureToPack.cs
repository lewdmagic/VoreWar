namespace DaVikingCode.AssetPacker
{
    internal class TextureToPack
    {
        public string File;
        public string Id;

        public TextureToPack(string file, string id)
        {
            File = file;
            Id = id;
        }
    }
}