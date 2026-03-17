
public class PlayerManager : GenericSingleton<PlayerManager>
{
    public PlayerMotor CurrentPlayer { get; private set; }

    public void SetPlayer(PlayerMotor player)
    {
        CurrentPlayer = player;
    }
}
