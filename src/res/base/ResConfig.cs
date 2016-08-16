namespace KangEngine.Res.Base
{


    class ResConfig
    {
        private const string MODEL_ROOT_PATH = "Models/";
        private const string PREFAB_ROOT_PATH = "Prefabs/";
        private const string ANIMATION_ROOT_PATH = "Animations/";

        public static string GetFullPath(ResType type, string path)
        {
            string dirc = "";
            switch (type)
            {
                case ResType.RT_MODEL:
                    dirc = MODEL_ROOT_PATH;
                    break;
                case ResType.RT_PREFAB:
                    dirc = PREFAB_ROOT_PATH;
                    break;
                case ResType.RT_ANIMATION:
                    dirc = ANIMATION_ROOT_PATH;
                    break;
                default:
                    break;
            }

            return dirc + path;
        }
    }

    public enum ResType
    {
        RT_MODEL,
        RT_PREFAB,
        RT_ANIMATION,
    }
}
