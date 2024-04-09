using System;

public class GlobalEvents : Singleton<GlobalEvents>
{
    public static event Action<Player> onPlayerEnabled = delegate{};
    public void PlayerEnabled(Player player) => onPlayerEnabled?.Invoke(player);

    public static event Action<bool> onEnablePlayerMovement = delegate{};
    public void EnablePlayerMovement(bool enable) => onEnablePlayerMovement?.Invoke(enable);
}
