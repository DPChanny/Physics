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
}

//���� ����
public class Setting
{
    //�⺻ ���� ����
    public readonly static Setting defaultSetting = new Setting(
        CentripetalForceSetting.defaultCentripetalForceDifficulty, 
        NetForceSetting.defaultNetForceSetting, 
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

    //�շ� ���� ����
    public readonly NetForceSetting netForceSetting;
    //�� ���� ����
    public readonly LightSetting lightSetting;

    //������ ��ġ ����
    public readonly int positionCount;

    public Setting(Difficulty _centripetalForceDifficulty, NetForceSetting _netForceSetting, LightSetting _lightSetting, int _positionCount)
    {
        centripetalForceDifficulty = _centripetalForceDifficulty;
        netForceSetting = _netForceSetting;
        lightSetting = _lightSetting;
        positionCount = _positionCount;
    }
}

//���ɷ� ���� ����
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
    public static CentripetalForceSetting[] centripetalForceSettings = { easyCentripetalForceSetting, normalCentripetalForceSetting, hardCentripetalForceSetting, customCentripetalForceSetting };

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
}

//�շ� ���� ����
public class NetForceSetting
{
    //�⺻ �շ� ���� ����
    public readonly static NetForceSetting defaultNetForceSetting = 
        new NetForceSetting(2.5f, 2.5f, 500f, 250, 250f, true, true, 75f, true, 50f, ForceDirection.None);

    //��ü ����
    public readonly float objectRange;
    //�̵� �ӵ�
    public readonly float speed;
    //�ִ� �� ����
    public readonly float maxForcePower;
    //�ּ� �� ����
    public readonly float minForcePower;
    //�⺻ �� ����
    public readonly float defaultForcePower;
    //�̵� ����
    public readonly float moveRange;
    //�� �̵�
    public readonly bool enemyMove;
    //�� �� ���� ��ȯ
    public readonly bool enemyForceDirectionToggle;
    //�� �� ���� ��ȯ Ȯ��
    public readonly float enemyForceDirectionTogglePercentage;
    //�� �� ���� ��ȯ
    public readonly bool enemyForcePowerToggle;
    //�� �� ���� ��ȯ Ȯ��
    public readonly float enemyForcePowerTogglePercentage;
    //�� �⺻ �� ����
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

//�� ����
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}

//���̵�
public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Custom
}