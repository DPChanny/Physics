using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;

    private const string centripetalForceStringLeft =
@"���ɷ�
���ɷ��� ������� ��� �߽� �������� �ۿ��Ͽ� ��ü�� ��θ� �ٲٴ� ���̴�.
���� ������ ��ü�� ������ ������ �� �����ϸ�, ������ ����� �߽��̴�.

����� ������� �� �ٲ�Ƿ� ��ӵ� ��� �ƴϴ�. 
��� �ϴ� ��ü�� ��� ��� ������ �� �ٲ�Ƿ� ���ӵ��� ������ �ִٰ� �� �� ������, ������ � ��1��Ģ�� ���� ���� �ۿ��Ѵٰ� �� �� �ִµ� �� ���� ���ɷ��̶�� �Ѵ�. 
���ɷ��� ��ü�� �ӵ� ���Ϳ� �������� �ۿ��ϹǷ�, ��ü�� �ӵ��� ���⸸�� ��ȭ��Ű�� �ӵ��� ũ��� ��ȭ��Ű�� �ʴ´�. 
���� ���ɷ��� ��ü�� ��ü�� ��� �߽��� �մ� ���� �� �ۿ뼱�� �׻� �����ϹǷ�, ���ɷ¿� ���� �������� 0�� �ȴ�. ���� ���ɷ� �ܿ� �ٸ� ���� �ۿ����� �ʴ� ��� ����� ���, ����� ���� ��Ģ�� �����Ѵ�.

����� �ϴ� �����ڴ�, ���ɷ°� ���ݴ� ������ ���� �ڽſ��� ���ɷ°� ���� �ۿ��Ͽ� ���� ������ �̷�ٰ� �����Ѵ�. �̷��� ���ɷ¿� �ݴ�Ǵ� �������� �ۿ�ȴٰ� �����ϴ� ������ ���� ���ɷ��̶�� �Ѵ�.";

    private const string centripetalForceStringRight = 
@"���ɷ� ����
���� ��ǥ: 
    ����
���� ����: 
    ���콺 �� Ŭ��: ȸ���� ����
���� ����: 
    �̵� �ӵ�: �÷��̾� �̵� �ӵ�
    Ʈ�� ����: Ʈ�� ��Ż ��� ����
���� ����:
         ���� ���� �����
    ���� 3    6    9
";

    private const string netForceStringLeft =
@"�շ�
�շ��� ��ü�� �ۿ��ϰ� �ִ� ��� ������ ���͸� ���Ͽ� ����� ���̴�.
�̷��� ���� �շ��� ���� ��ü�� �ۿ�Ǵ� ���� ũ��� ������ ��Ÿ����.
��ü�� �ۿ��ϴ� ���� ��ü�� ��Ÿ���� �շ��� ���ϱ� ���� ������ü���� ���Ǳ⵵ �Ѵ�.

���� ��ü�� ����� ��¥�Ӹ� �ƴ϶� �������� �����ϹǷ� ��ü�� ����� �����Ǵ� �������� �շ��� �ٸ� �� �ִ�. 
�ٽ� ���ϸ� ��¥���� �������� ������ �շ��� �ȴ�.

���� ���ͷ����� ũ��� ������ ���´�. 
��¥���� �ϳ��� ��ü�� �ۿ��ϰ� �ִ� ��� ������ �������̴�. 
���� ���� �޴� ��ü�� �ٴڿ� ���� ���� ���� ������ ū ������ ���� �ʴ� ���̶��, ��¥���� ũ�Ⱑ ������ ��ü�� ������ ����� ũ���� ������ �ִ� ��ü�� ��¥���� �ۿ��ϴ� �������� ��� ���̴�.

���� ũ��� ������ 0 �Ǵ� ���� ���� ������, ��¥���� ����ؼ� ������ �ݴ��� �� ���� �� �� �ϳ��� ������ ��Ÿ���� ����� ���� �ִ�.";

    private const string netForceStringRight =
@"�շ� ����
���� ��ǥ: 
    �ְ� ����
���� ����: 
    ���콺 �� Ŭ��: �η�
    ���콺 �� Ŭ��: ô��
    W: ���� �̵�
    S: �Ʒ��� �̵�
    ���콺 ��ũ��: �� ���� ��ȯ
���� ����: 
    �̵� �ӵ�: �÷��̾� �̵� �ӵ�
    ��ü ����: ��ü ��Ż ��� ����
    �ִ� �� ����: �ִ� �� ����
    �ּ� �� ����: �ּ� �� ����
    �� �̵�: �� �̵� ����
    �� �� ���� ��ȯ: �� �� ���� ��ȯ ����
    �� �� ���� ��ȯ: �� �� ���� ��ȯ ����
���� ����:
         ���� ���� �����
    1��  3    4    6
    2��  2    3    5
    3��  1    2    4";

    private const string lightStringLeft =
@"";

    private const string lightStringRight =
@"";

    private readonly string[] text = new string[] { 
        centripetalForceStringLeft, centripetalForceStringRight,
        netForceStringLeft, netForceStringRight,
        lightStringLeft, lightStringRight};

    [SerializeField]
    private TextMeshProUGUI TextUI_left;

    [SerializeField]
    private TextMeshProUGUI TextUI_right;

    [SerializeField]
    private Image ImageUI_image;

    private void Start()
    {
        ImageUI_image.sprite = images[(int)Public.game];
        TextUI_left.text = text[(int)Public.game * 2];
        TextUI_right.text = text[(int)Public.game * 2 + 1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.MAIN);
        }
    }

    public void OnStart()
    {
        if(Public.game == Game.NetForce)
        {
            Public.LoadScene(SceneName.NET_FORCE);
        }
        if (Public.game == Game.CentripetalForce)
        {
            Public.LoadScene(SceneName.CENTRIPETAL_FORCE);
        }
        if (Public.game == Game.Light)
        {
            Public.LoadScene(SceneName.LIGHT);
        }
    }
}
