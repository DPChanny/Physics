using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NoteManager : MonoBehaviour
{
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
@"";

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

    private void Start()
    {
        TextUI_left.text = text[(int)Public.game * 2];
        TextUI_right.text = text[(int)Public.game * 2 + 1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            SceneManager.LoadScene(SceneName.MAIN);
        }
    }
}
