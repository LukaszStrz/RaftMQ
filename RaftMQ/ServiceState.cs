namespace RaftMQ
{
    public enum ServiceState
    {
        UNREGISTERED,
        FOLLOWER,
        CANDIDATE,
        LEADER
    }
}
