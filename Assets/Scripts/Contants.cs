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

//태그
public static class Tag
{
    public const string GAME_MANAGER = "GameManager";
    public const string FINISH_POINT = "FinishPoint";
    public const string RANGE = "Range";
    public const string OBJECT = "Object";
    public const string MIRROR = "Mirror";
    public const string OBSTACLE = "Obstacle";
}

//화면 이름
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
    public const string LIGHT = "Light";
}

//게임 설정
public class Setting
{
    //기본 게임 설정
    public static Setting defaultSetting = new Setting(
        CentripetalForceSetting.defaultCentripetalForceSetting, 
        NetForceSetting.defaultNetForceSetting, 
        LightSetting.defaultLightSetting, 
        200);

    //구심력 게임 설정
    public readonly CentripetalForceSetting centripetalForceSetting;
    //합력 게임 설정
    public readonly NetForceSetting netForceSetting;
    //빛 게임 설정
    public readonly LightSetting lightSetting;

    //렌더러 위치 개수
    public readonly int positionCount;

    public Setting(CentripetalForceSetting _centripetalForceSetting, NetForceSetting _netForceSetting, LightSetting _lightSetting, int _positionCount)
    {
        centripetalForceSetting = _centripetalForceSetting;
        netForceSetting = _netForceSetting;
        lightSetting = _lightSetting;
        positionCount = _positionCount;
    }
}

//구심력 게임 설정
public class CentripetalForceSetting
{
    //쉬움 구심력 게임 설정
    public static CentripetalForceSetting easyCentripetalForceSetting = new CentripetalForceSetting(10f, 3f);
    //보통 구심력 게임 설정
    public static CentripetalForceSetting normalCentripetalForceSetting = new CentripetalForceSetting(12.5f, 2.5f);
    //어려움 구심력 게임 설정
    public static CentripetalForceSetting hardCentripetalForceSetting = new CentripetalForceSetting(15f, 2f);

    //기본 구심력 게임 설정
    public static CentripetalForceSetting defaultCentripetalForceSetting = normalCentripetalForceSetting;

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
    public static NetForceSetting defaultNetForceSetting = new NetForceSetting(2.5f, 2.5f, 500f, 250, 250f, true, true, 75f, true, 50f, ForceDirection.None);

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
        moveRange = 7.5f;
        enemyMove = _enemyMove;
        enemyForceDirectionToggle = _enemyForceDirectionToggle;
        enemyForceDirectionTogglePercentage = _enemyForceDirectionTogglePercentage;
        enemyForcePowerToggle = _enemyForcePowerToggle;
        enemyForcePowerTogglePercentage = _enemyForcePowerTogglePercentage;
        enemyDefaultForceDirection = _enemyDefaultForceDirection;
    }
}

//빛 게임 설정
public class LightSetting
{
    //기본 빛 게임 설정
    public static LightSetting defaultLightSetting = new LightSetting(10f);

    //빛 이동 속도
    public readonly float speed;

    public LightSetting(float _speed)
    {
        speed = _speed;
    }
}

//힘 방향
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}
