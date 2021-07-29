namespace VkMusicDownloader
{
    public static class SomeHelper
    {
        // TODO move out
        private const int AudiosPerBlock = 20;
        
        public static void CalcIndices(int index, out int index1, out int index2)
        {
            index1 = index / AudiosPerBlock + 1;
            index2 = index % AudiosPerBlock + 1;
        }
    }
}