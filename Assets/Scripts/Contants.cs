using UnityEngine;

//조작키
public static class Key
{
    public const KeyCode START = KeyCode.Space;
    public const KeyCode EXIT = KeyCode.Escape;
    public const int MOUSE_LEFT = 0;
}

//레이어 이름
public static class LayerName
{
    public const string TRACK = "Track";
}

//태그
public static class Tag
{
    public const string GAME_MANAGER = "Game Manager";
    public const string FINISH_POINT = "Finish Point";
    public const string TRACK = "Track";
    public const string OBJECT = "Object";
}

//장면 이름
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
}