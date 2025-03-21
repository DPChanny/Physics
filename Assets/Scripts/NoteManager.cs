using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;

    private const string centripetalForceStringLeft =
@"구심력
구심력은 원운동에서 운동의 중심 방향으로 작용하여 물체의 경로를 바꾸는 힘이다.
힘의 방향은 물체의 순간의 운동방향과 늘 직교하며, 방향은 곡면의 중심이다.

원운동은 운동방향이 늘 바뀌므로 등속도 운동이 아니다. 
원운동 하는 물체의 경우 운동의 방향이 늘 바뀌므로 가속도를 가지고 있다고 할 수 있으며, 뉴턴의 운동 제1법칙에 따라 힘이 작용한다고 볼 수 있는데 이 힘을 구심력이라고 한다. 
구심력은 물체의 속도 벡터에 수직으로 작용하므로, 물체의 속도의 방향만을 변화시키고 속도의 크기는 변화시키지 않는다. 
또한 구심력은 물체와 물체의 운동의 중심을 잇는 선과 그 작용선이 항상 평행하므로, 구심력에 의한 돌림힘은 0이 된다. 따라서 구심력 외에 다른 힘이 작용하지 않는 등속 원운동의 경우, 각운동량 보존 법칙이 성립한다.

원운동을 하는 관찰자는, 구심력과 정반대 방향의 힘이 자신에게 구심력과 같이 작용하여 힘의 평형을 이룬다고 생각한다. 이렇게 구심력에 반대되는 방향으로 작용된다고 생각하는 가상의 힘을 원심력이라고 한다.";

    private const string centripetalForceStringRight = 
@"구심력 게임
게임 목표: 
    완주
게임 조작: 
    마우스 좌 클릭: 회전축 설정
게임 보상:
         쉬움 보통 어려움
    완주 3    6    9
";

    private const string netForceStringLeft =
@"합력
합력은 물체에 작용하고 있는 모든 힘들의 벡터를 합하여 계산한 것이다.
이렇게 계산된 합력은 실제 물체에 작용되는 힘의 크기와 방향을 나타낸다.
물체에 작용하는 힘들 전체를 나타내고 합력을 구하기 위해 자유물체도가 사용되기도 한다.

실제 물체의 운동에는 알짜뿐만 아니라 돌림힘도 관여하므로 물체의 운동에서 관찰되는 최종적인 합력은 다를 수 있다. 
다시 말하면 알짜힘과 돌림힘이 합쳐져 합력이 된다.

힘은 벡터량으로 크기와 방향을 갖는다. 
알짜힘은 하나의 물체에 작용하고 있는 모든 힘들의 벡터합이다. 
만약 힘을 받는 물체가 바닥에 놓인 공과 같이 마찰에 큰 영향을 받지 않는 것이라면, 알짜힘의 크기가 정지한 물체를 움직일 충분한 크기라면 정지해 있는 물체는 알짜힘이 작용하는 방향으로 운동할 것이다.

힘의 크기는 언제나 0 또는 양의 값을 갖지만, 알짜힘의 계산해서 방향이 반대인 두 힘은 그 중 하나를 음수로 나타내어 계산할 수도 있다.";

    private const string netForceStringRight =
@"합력 게임
게임 목표: 
    최고 점수
게임 조작: 
    마우스 좌 클릭: 인력
    마우스 우 클릭: 척력
    W: 위로 이동
    S: 아래로 이동
    마우스 스크롤: 힘 세기 전환
게임 보상:
         쉬움 보통 어려움
    1등  3    4    6
    2등  2    3    5
    3등  1    2    4";

    private const string lightStringLeft =
@"빛의 반사
빛의 반사는 매질의 경계면에서 굴절과 함께 일어난다.

빛이 반사할 때는 다음과 같은 반사의 법칙을 따른다:
1. 법선에 대해 입사각과 반사각의 크기는 같다.
2. 법선을 기준으로 입사파와 반사파는 서로 반대편에 있다.
3. 입사파, 법선, 반사파는 서로 동일 평면상에 위치한다.
즉, 반사면의 각도를 통해 반사파의 각도를 조절할 수 있다.
반사의 종류는 다음과 같다:
    정반사
        매끄러운 면에서 일정한 방향(나란하게)으로 입사한 빛이 나란하게 반사하는 것
    정반사의 특징
        빛이 한 방향으로만 반사된다
        반사광선의 방향에서만 물체를 볼 수 있다.
        반사의 법칙이 성립된다.
    난반사
        울퉁불퉁한 면에서 여러 방향으로 빛이 흩어져서 반사하는 것
    난반사의 특징
        빛이 사방으로 흩어져 반사
        반사의 법칙 성립
";

    private const string lightStringRight =
@"빛 게임
게임 목표: 
    최소 거울 개수, 최소 시간
게임 조작: 
    마우스 좌 클릭: 거울 설치
    마우스 우 클릭: 거울 제거
    마우스 스크롤: 거울 각도 전환
게임 보상:
    1등  6    4등  3
    2등  5    5등  2
    3등  4    6등  1";

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
