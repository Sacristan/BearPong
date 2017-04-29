[System.Serializable]
internal struct MinMax
{
    public float min;
    public float max;

    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float RandomFromRange()
    {
        return UnityEngine.Random.Range(min, max);
    }
}
