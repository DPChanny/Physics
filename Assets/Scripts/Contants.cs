using System.Collections.Generic;
using UnityEngine;

//����Ű
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

//�±�
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

//ȭ�� �̸�
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
    public const string LIGHT = "Light";
    public const string NOTE = "Note";
}

//���� ����
public class Setting
{
    //�⺻ ���� ����
    public readonly static Setting defaultSetting = new Setting(
        CentripetalForceSetting.defaultCentripetalForceDifficulty, 
        NetForceSetting.defaultNetForceDifficulty, 
        LightSetting.defaultLightSetting, 
        200);

    //���ɷ� ���� ���̵�
    public Difficulty centripetalForceDifficulty;
    //���ɷ� ���� ����
    public ref CentripetalForceSetting centripetalForceSetting
    {
        get
        {
            return ref CentripetalForceSetting.centripetalForceSettings[(int)centripetalForceDifficulty];
        }
    }

    //�շ� ���� ���̵�
    public Difficulty netForceDifficulty;
    //�շ� ���� ����
    public ref NetForceSetting netForceSetting
    {
        get
        {
            return ref NetForceSetting.netForceSettings[(int)netForceDifficulty];
        }
    }

    //�� ���� ����
    public readonly LightSetting lightSetting;

    //������ ��ġ ����
    public readonly int positionCount;

    public Setting(Difficulty _centripetalForceDifficulty, Difficulty _netForceDfficulty, LightSetting _lightSetting, int _positionCount)
    {
        centripetalForceDifficulty = _centripetalForceDifficulty;
        netForceDifficulty = _netForceDfficulty;
        lightSetting = _lightSetting;
        positionCount = _positionCount;
    }
}

//���ɷ� ���� ����
[System.Serializable]
public class CentripetalForceSetting
{
    //���� ���ɷ� ���� ����
    private readonly static CentripetalForceSetting easyCentripetalForceSetting = new CentripetalForceSetting(10f, 3f);
    //���� ���ɷ� ���� ����
    private readonly static CentripetalForceSetting normalCentripetalForceSetting = new CentripetalForceSetting(15f, 2.5f);
    //����� ���ɷ� ���� ����
    private readonly static CentripetalForceSetting hardCentripetalForceSetting = new CentripetalForceSetting(20f, 2f);
    //����� ���� ���ɷ� ���� ����
    private readonly static CentripetalForceSetting customCentripetalForceSetting = normalCentripetalForceSetting;

    //���ɷ� ���� ���� ����Ʈ
    public static CentripetalForceSetting[] centripetalForceSettings = {
        new CentripetalForceSetting(easyCentripetalForceSetting),
        new CentripetalForceSetting(normalCentripetalForceSetting),
        new CentripetalForceSetting(hardCentripetalForceSetting),
        new CentripetalForceSetting(customCentripetalForceSetting) };

    //�⺻ ���ɷ� ���� ���̵�
    public readonly static Difficulty defaultCentripetalForceDifficulty = Difficulty.Normal;

    //�̵� �ӵ�
    public float speed;
    //Ʈ�� ����
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
//�շ� ���� ����
public class NetForceSetting
{
    //���� �շ� ���� ����
    private readonly static NetForceSetting easyNetForceSetting =
        new NetForceSetting(2.5f, 3f, 500f, 250f, true, true, 25f, false, 0f, ForceDirection.None);
    //���� �շ� ���� ����
    private readonly static NetForceSetting normalNetForceSetting =
        new NetForceSetting(3f, 2.5f, 500f, 250f,true, true, 50f, true, 50f, ForceDirection.None);
    //����� �շ� ���� ����
    private readonly static NetForceSetting hardNetForceSetting = 
        new NetForceSetting(3.5f, 2f, 1000f, 500f, true, true, 75f, true, 75f, ForceDirection.None);
    //����� ���� �շ� ���� ����
    private readonly static NetForceSetting customNetForceSetting = normalNetForceSetting;

    //���ɷ� ���� ���� ����Ʈ
    public static NetForceSetting[] netForceSettings = {
        new NetForceSetting(easyNetForceSetting),
        new NetForceSetting(normalNetForceSetting),
        new NetForceSetting(hardNetForceSetting),
        new NetForceSetting(customNetForceSetting)};

    //�⺻ �շ� ���� ���̵�
    public readonly static Difficulty defaultNetForceDifficulty = Difficulty.Normal;

    //��ü ����
    public float objectRange;
    //�̵� �ӵ�
    public float speed;
    //�ִ� �� ����
    public float maxForcePower;
    //�ּ� �� ����
    public float minForcePower;
    //�̵� ����
    public readonly float moveRange;
    //�� �̵�
    public bool enemyMove;
    //�� �� ���� ��ȯ
    public bool enemyForceDirectionToggle;
    //�� �� ���� ��ȯ Ȯ��
    public readonly float enemyForceDirectionTogglePercentage;
    //�� �� ���� ��ȯ
    public bool enemyForcePowerToggle;
    //�� �� ���� ��ȯ Ȯ��
    public readonly float enemyForcePowerTogglePercentage;
    //�� �⺻ �� ����
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

//�� ���� ����
public class LightSetting
{
    //�⺻ �� ���� ����
    public readonly static LightSetting defaultLightSetting = new LightSetting(10f);

    //�� �̵� �ӵ�
    public readonly float speed;

    public LightSetting(float _speed)
    {
        speed = _speed;
    }
}

//���ɷ� ���� ���
[System.Serializable]
public class CentripetalForceRecord{
    
    //���ɷ� ���� ���̵�
    public Difficulty difficulty;

    //���ɷ� ���� ����
    public CentripetalForceSetting setting;

    //���� ����
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

//�շ� ���� ���
[System.Serializable]
public class NetForceRecord
{
    //�շ� ���� ���̵�
    public Difficulty difficulty;

    //�շ� ���� ����
    public NetForceSetting setting;

    //����
    public float score;
    
    public NetForceRecord(Difficulty _difficulty, NetForceSetting _setting, float _score)
    {
        difficulty = _difficulty;
        score = _score;
        setting = new NetForceSetting(_setting);
    }
}

//���
public class Record
{
    //���ɷ� ���� ���
    public List<CentripetalForceRecord> centripetalForceRecords;
    //�շ� ���� ���
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

//�� ����
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}

//���̵�
[System.Serializable]
public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Custom
}

//ī�޶� ����
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