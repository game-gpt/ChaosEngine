namespace Chaos.Networking.Messages;

public abstract record NetworkMessage(MessageType Type, DateTime Timestamp)
{
    public abstract byte[] Serialize();
    public static NetworkMessage? Deserialize(byte[] data) => null;
}
