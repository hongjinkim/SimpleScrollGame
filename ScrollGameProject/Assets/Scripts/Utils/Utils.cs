using UnityEngine;
public static class Utils
{
    public static bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
