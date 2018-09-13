using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { Simple, Ridge };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public Simple simpleNoiseSettings;

    [ConditionalHide("filterType", 1)]
    public Ridge ridgeNoiseSettings;

    [System.Serializable]
    public class Simple
    {

        public float strength = 1;
        [Range(1, 8)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = 0.5f;
        public Vector3 center;
        public float minValue;
    }

    [System.Serializable]
    public class Ridge : Simple
    {
        public float weightMultiplier = 0.5f;
    }
}
