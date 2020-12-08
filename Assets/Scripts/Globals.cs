

public static class Globals
{
    public static float volume = 0.7f;
    internal static bool isGazing = false;
    public enum Location
    {
        HALL = 0, IDC_ROOM_1, IDC_ROOM_2, IDC_ROOM_3, IDC_ROOM_4
    }
    public static Location currentLocation;
    public static Location nextLocation;
}
