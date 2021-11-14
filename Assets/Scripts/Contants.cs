using UnityEngine;

//조작키
public static class Key
{
    public const KeyCode START = KeyCode.Space;
    public const KeyCode EXIT = KeyCode.Escape;
    public const KeyCode DOWN = KeyCode.S;
    public const KeyCode UP = KeyCode.W;
    public const int MOUSE_LEFT = 0;
    public const int MOUSE_RIGHT = 1;
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

//화면 이름
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
}

//게임 설정
public class Setting
{
    //기본 게임 설정
    public static Setting defaultSetting = new Setting(CentripetalForceSetting.defaultCentripetalForceSetting, NetForceSetting.defaultNetForceSetting, 200);

    //구심력 게임 설정
    public readonly CentripetalForceSetting centripetalForceSetting;
    public readonly NetForceSetting netForceSetting;

    //렌더러 위치 개수
    public readonly int positionCount;

    public Setting(CentripetalForceSetting _centripetalForceSetting, NetForceSetting _netForceSetting,int _positionCount)
    {
        centripetalForceSetting = _centripetalForceSetting;
        netForceSetting = _netForceSetting;
        positionCount = _positionCount;
    }
}

//구심력 게임 설정
public class CentripetalForceSetting
{
    //기본 구심력 게임 설정
    public static CentripetalForceSetting defaultCentripetalForceSetting = new CentripetalForceSetting(10f, 2.5f);

    //플레이어 이동 속도
    public readonly float speed;
    //트랙 범위
    public readonly float trackRange;

    public CentripetalForceSetting(float _speed, float _trackRange)
    {
        speed = _speed;
        trackRange = _trackRange;
    }
}

//합력 게임 설정
public class NetForceSetting
{
    //기본 합력 게임 설정
    public static NetForceSetting defaultNetForceSetting = new NetForceSetting(2.5f, 2f, 500f, 250, 250f, true, true, 50f, true, 25f, ForceDirection.None);

    //물체 범위
    public readonly float objectRange;
    //이동 속도
    public readonly float speed;
    //최대 힘 세기
    public readonly float maxForcePower;
    //최소 힘 세기
    public readonly float minForcePower;
    //기본 힘 세기
    public readonly float defaultForcePower;
    //이동 범위
    public readonly float moveRange;
    //적 이동
    public readonly bool enemyMove;
    //적 힘 방향 전환
    public readonly bool enemyForceDirectionToggle;
    //적 힘 방향 전환 확률
    public readonly float enemyForceDirectionTogglePercentage;
    //적 힘 세기 전환
    public readonly bool enemyForcePowerToggle;
    //적 힘 세기 전환 확률
    public readonly float enemyForcePowerTogglePercentage;
    //적 기본 힘 방향
    public readonly ForceDirection enemyDefaultForceDirection;

    public NetForceSetting(
        float _speed, 
        float _objectRange, 
        float _maxForcePower, 
        float _minForcePower, 
        float _defaultForcePower, 
        bool _enemyMove, 
        bool _enemyForceDirectionToggle,
        float _enemyForceDirectionTogglePercentage,
        bool _enemyForcePowerToggle,
        float _enemyForcePowerTogglePercentage,
        ForceDirection _enemyDefaultForceDirection)
    {
        speed = _speed;
        objectRange = _objectRange;
        maxForcePower = _maxForcePower;
        minForcePower = _minForcePower;
        defaultForcePower = _defaultForcePower;
        moveRange = 5f;
        enemyMove = _enemyMove;
        enemyForceDirectionToggle = _enemyForceDirectionToggle;
        enemyForceDirectionTogglePercentage = _enemyForceDirectionTogglePercentage;
        enemyForcePowerToggle = _enemyForcePowerToggle;
        enemyForcePowerTogglePercentage = _enemyForcePowerTogglePercentage;
        enemyDefaultForceDirection = _enemyDefaultForceDirection;
    }
}

//힘 방향
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}
