
public enum ObjectID
{
    SO_POWERUP_MULTIPLY_COIN = 0,
    SO_POWERUP_SHIELD = 1,
    SO_POWERUP_FLIGHT = 2,

    NONE = 100,
}

public interface IIdentificable
{
    public ObjectID ID { get;}
}
