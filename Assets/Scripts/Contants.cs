using UnityEngine;

//����Ű
public static class Key
{
    public const KeyCode START = KeyCode.Space;
    public const KeyCode EXIT = KeyCode.Escape;
    public const KeyCode DOWN = KeyCode.S;
    public const KeyCode UP = KeyCode.W;
    public const int MOUSE_LEFT = 0;
    public const int MOUSE_RIGHT = 1;
}

//���̾� �̸�
public static class LayerName
{
    public const string TRACK = "Track";
}

//�±�
public static class Tag
{
    public const string GAME_MANAGER = "Game Manager";
    public const string FINISH_POINT = "Finish Point";
    public const string TRACK = "Track";
    public const string OBJECT = "Object";
}

//ȭ�� �̸�
public static class SceneName
{
    public const string CENTRIPETAL_FORCE= "Centripetal Force";
    public const string NET_FORCE = "Net Force";
    public const string MAIN = "Main";
}

//���� ����
public class Setting
{
    //�⺻ ���� ����
    public static Setting defaultSetting = new Setting(CentripetalForceSetting.defaultCentripetalForceSetting, NetForceSetting.defaultNetForceSetting, 200);

    //���ɷ� ���� ����
    public readonly CentripetalForceSetting centripetalForceSetting;
    public readonly NetForceSetting netForceSetting;

    //������ ��ġ ����
    public readonly int positionCount;

    public Setting(CentripetalForceSetting _centripetalForceSetting, NetForceSetting _netForceSetting,int _positionCount)
    {
        centripetalForceSetting = _centripetalForceSetting;
        netForceSetting = _netForceSetting;
        positionCount = _positionCount;
    }
}

//���ɷ� ���� ����
public class CentripetalForceSetting
{
    //�⺻ ���ɷ� ���� ����
    public static CentripetalForceSetting defaultCentripetalForceSetting = new CentripetalForceSetting(10f, 2.5f);

    //�÷��̾� �̵� �ӵ�
    public readonly float speed;
    //Ʈ�� ����
    public readonly float trackRange;

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
    public static NetForceSetting defaultNetForceSetting = new NetForceSetting(2.5f, 2f, 500f, 250, 250f, true, true, 50f, true, 25f, ForceDirection.None);

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
        moveRange = 5f;
        enemyMove = _enemyMove;
        enemyForceDirectionToggle = _enemyForceDirectionToggle;
        enemyForceDirectionTogglePercentage = _enemyForceDirectionTogglePercentage;
        enemyForcePowerToggle = _enemyForcePowerToggle;
        enemyForcePowerTogglePercentage = _enemyForcePowerTogglePercentage;
        enemyDefaultForceDirection = _enemyDefaultForceDirection;
    }
}

//�� ����
public enum ForceDirection
{
    Push = -1,
    None = 0,
    Pull = 1,
}
