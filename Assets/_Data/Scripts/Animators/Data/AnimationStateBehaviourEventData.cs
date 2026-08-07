public readonly struct StateExitedData
{
    public int InstanceID { get; }
    public int ShortNameHash { get; }

    public StateExitedData(int instanceID, int shortNameHash)
    {
        InstanceID = instanceID;
        ShortNameHash = shortNameHash;
    }
}