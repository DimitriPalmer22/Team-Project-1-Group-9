public static class GameSettings
{
    private static bool _isHardcore;
    private static bool _isInfiniteHealth;

    public static bool IsHardcore => _isHardcore;
    public static bool IsInfiniteHealth => _isInfiniteHealth;

    public static void SetHardcore(bool value) => _isHardcore = value;
    public static void SetInfiniteHealth(bool value) => _isInfiniteHealth = value;
}

public enum GameSettingType
{
    Hardcore,
    InfiniteHealth
}