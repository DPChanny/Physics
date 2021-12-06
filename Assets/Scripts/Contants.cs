using System.Collections.Generic;
using UnityEngine;

//조작키
public static class Key
{
    public const KeyCode START = KeyCode.Space;
    public const KeyCode EXIT = KeyCode.Escape;
    public const KeyCode DOWN = KeyCode.S;
    public const KeyCode UP = KeyCode.W;
    public const KeyCode DIFFICULTY_MANAGER = KeyCode.BackQuote;
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
    public const string DIFFICULTY_MANAGER = "DifficultyManager";
}

//화면 이름
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
    public const string LIGHT = "Light";
    public const string NOTE = "Note";
}

//게임 설정
public class Setting
{
    //기본 게임 설정
    public readonly static Setting defaultSetting = new Setting(
        CentripetalForceSetting.defaultCentripetalForceDifficulty, 
        NetForceSetting.defaultNetForceDifficulty, 
        LightSetting.defaultLightSetting, 
        200);

    //구심력 게임 난이도
    public Difficulty centripetalForceDifficulty;
    //구심력 게임 설정
    public ref CentripetalForceSetting centripetalForceSetting
    {
        get
        {
            return ref CentripetalForceSetting.centripetalForceSettings[(int)centripetalForceDifficulty];
        }
    }

    //합력 게임 난이도
    public Difficulty netForceDifficulty;
    //합력 게임 설정
    public ref NetForceSetting netForceSetting
    {
        get
        {
            return ref NetForceSetting.netForceSettings[(int)netForceDifficulty];
        }
    }

    //빛 게임 설정
    public readonly LightSetting lightSetting;

    //렌더러 위치 개수
    public readonly int positionCount;

    public Setting(Difficulty _centripetalForceDifficulty, Difficulty _netForceDfficulty, LightSetting _lightSetting, int _positionCount)
    {
        centripetalForceDifficulty = _centripetalForceDifficulty;
        netForceDifficulty = _netForceDfficulty;
        lightSetting = _lightSetting;
        positionCount = _positionCount;
    }
}

//구심력 게임 설정
[System.Serializable]
public class CentripetalForceSetting
{
    //쉬움 구심력 게임 설정
    private readonly static CentripetalForceSetting easyCentripetalForceSetting = new CentripetalForceSetting(10f, 3f);
    //보통 구심력 게임 설정
    private readonly static CentripetalForceSetting normalCentripetalForceSetting = new CentripetalForceSetting(15f, 2.5f);
    //어려움 구심력 게임 설정
    private readonly static CentripetalForceSetting hardCentripetalForceSetting = new CentripetalForceSetting(20f, 2f);
    //사용자 설정 구심력 게임 설정
    private readonly static CentripetalForceSetting customCentripetalForceSetting = normalCentripetalForceSetting;

    //구심력 게임 설정 리스트
    public static CentripetalForceSetting[] centripetalForceSettings = {
        new CentripetalForceSetting(easyCentripetalForceSetting),
        new CentripetalForceSetting(normalCentripetalForceSetting),
        new CentripetalForceSetting(hardCentripetalForceSetting),
        new CentripetalForceSetting(customCentripetalForceSetting) };

    //기본 구심력 게임 난이도
    public readonly static Difficulty defaultCentripetalForceDifficulty = Difficulty.Normal;

    //이동 속도
    public float speed;
    //트랙 범위
    public float trackRange;

    public CentripetalForceSetting(float _speed, float _trackRange)
    {
        speed = _speed;
        trackRange = _trackRange;
    }

    public CentripetalForceSetting(CentripetalForceSetting _setting)
    {
        trackRange = _setting.trackRange;
        speed = _setting.speed;
    }
}

[System.Serializable]
//합력 게임 설정
public class NetForceSetting
{
    //쉬움 합력 게임 설정
    private readonly static NetForceSetting easyNetForceSetting =
        new NetForceSetting(2.5f, 3f, 500f, 250f, true, true, 25f, false, 0f, ForceDirection.None);
    //보통 합력 게임 설정
    private readonly static NetForceSetting normalNetForceSetting =
        new NetForceSetting(3f, 2.5f, 500f, 250f,true, true, 50f, true, 50f, ForceDirection.None);
    //어려움 합력 게임 설정
    private readonly static NetForceSetting hardNetForceSetting = 
        new NetForceSetting(3.5f, 2f, 1000f, 500f, true, true, 75f, true, 75f, ForceDirection.None);
    //사용자 설정 합력 게임 설정
    private readonly static NetForceSetting customNetForceSetting = normalNetForceSetting;

    //구심력 게임 설정 리스트
    public static NetForceSetting[] netForceSettings = {
        new NetForceSetting(easyNetForceSetting),
        new NetForceSetting(normalNetForceSetting),
        new NetForceSetting(hardNetForceSetting),
        new NetForceSetting(customNetForceSetting)};

    //기본 합력 게임 난이도
    public readonly static Difficulty defaultNetForceDifficulty = Difficulty.Normal;

    //물체 범위
    public float objectRange;
    //이동 속도
    public float speed;
    //최대 힘 세기
    public float maxForcePower;
    //최소 힘 세기
    public float minForcePower;
    //이동 범위
    public readonly float moveRange;
    //적 이동
    public bool enemyMove;
    //적 힘 방향 전환
    public bool enemyForceDirectionToggle;
    //적 힘 방향 전환 확률
    public readonly float enemyForceDirectionTogglePercentage;
    //적 힘 세기 전환
    public bool enemyForcePowerToggle;
    //적 힘 세기 전환 확률
    public readonly float enemyForcePowerTogglePercentage;
    //적 기본 힘 방향
    public readonly ForceDirection enemyDefaultForceDirection;

    public NetForceSetting(
        float _speed, 
        float _objectRange, 
        float _maxForcePower, 
        float _minForcePower, 
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
        moveRange = 7.5f;
        enemyMove = _enemyMove;
        enemyForceDirectionToggle = _enemyForceDirectionToggle;
        enemyForceDirectionTogglePercentage = _enemyForceDirectionTogglePercentage;
        enemyForcePowerToggle = _enemyForcePowerToggle;
        enemyForcePowerTogglePercentage = _enemyForcePowerTogglePercentage;
        enemyDefaultForceDirection = _enemyDefaultForceDirection;
    }

    public NetForceSetting(NetForceSetting _netForceSetting)
    {
        speed = _netForceSetting.speed;
        objectRange = _netForceSetting.objectRange;
        maxForcePower = _netForceSetting.maxForcePower;
        minForcePower = _netForceSetting.minForcePower;
        moveRange = 7.5f;
        enemyMove = _netForceSetting.enemyMove;
        enemyForceDirectionToggle = _netForceSetting.enemyForceDirectionToggle;
        enemyForceDirectionTogglePercentage = _netForceSetting.enemyForceDirectionTogglePercentage;
        enemyForcePowerToggle = _netForceSetting.enemyForcePowerToggle;
        enemyForcePowerTogglePercentage = _netForceSetting.enemyForcePowerTogglePercentage;
        enemyDefaultForceDirection = _netForceSetting.enemyDefaultForceDirection;
    }
}

//빛 게임 설정
public class LightSetting
{
    //기본 빛 게임 설정
    public readonly static LightSetting defaultLightSetting = new LightSetting(10f);

    //빛 이동 속도
    public readonly float speed;

    public LightSetting(float _speed)
    {
        speed = _speed;
    }
}

//구심력 게임 기록
[System.Serializable]
public class CentripetalForceRecord{
    
    //구심력 게임 난이도
    public Difficulty difficulty;

    //구심력 게임 설정
    public CentripetalForceSetting setting;

    //완주 여부
    public bool succeed;

    public CentripetalForceRecord(
        Difficulty _difficulty, 
        CentripetalForceSetting _setting,
        bool _succeed)
    {
        setting = new CentripetalForceSetting(_setting);
        difficulty = _difficulty;
        succeed = _succeed;
    }
}

//합력 게임 기록
[System.Serializable]
public class NetForceRecord
{
    //합력 게임 난이도
    public Difficulty difficulty;

    //합력 게임 설정
    public NetForceSetting setting;

    //점수
    public float score;
    
    public NetForceRecord(Difficulty _difficulty, NetForceSetting _setting, float _score)
    {
        difficulty = _difficulty;
        score = _score;
        setting = new NetForceSetting(_setting);
    }
}

//기록
public class Record
{
    //구심력 게임 기록
    public List<CentripetalForceRecord> centripetalForceRecords;
    //합력 게임 기록
    public List<NetForceRecord> netForceRecords;

    public Record(
        List<CentripetalForceRecord> _centripetalForceRecords, 
        List<NetForceRecord> _netForceRecords)
    {
        centripetalForceRecords = _centripetalForceRecords;
        netForceRecords = _netForceRecords;
    }

    public Record()
    {
        centripetalForceRecords = new List<CentripetalForceRecord>();
        netForceRecords = new List<NetForceRecord>();
    }

    public RecordWrapper Wrap()
    {
        return new RecordWrapper(
            centripetalForceRecords.ToArray(),
            netForceRecords.ToArray());
    }
}

[System.Serializable]
public class RecordWrapper
{
    public CentripetalForceRecord[] centripetalForceRecords;
    public NetForceRecord[] netForceRecords;

    public RecordWrapper(
        CentripetalForceRecord[] _centripetalForceRecords, 
        NetForceRecord[] _netForceRecord)
    {
        centripetalForceRecords = _centripetalForceRecords;
        netForceRecords = _netForceRecord;
    }

    public Record Unwrap()
    {
        return new Record(
            new List<CentripetalForceRecord>(centripetalForceRecords),
            new List<NetForceRecord>(netForceRecords));
    }
}

//힘 방향
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}

//난이도
[System.Serializable]
public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Custom
}

//카메라 상태
public enum CameraStatus
{
    Main,
    Middle,
    Window,
    Bookshelf,
    Desk
}

public enum Game
{
    CentripetalForce,
    NetForce,
    Light
}