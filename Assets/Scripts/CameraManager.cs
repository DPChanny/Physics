using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Image ImageUI_Fade;

    private void Start()
    {
        Out(5f, null);
    }

    private IEnumerator OutC = null;

    public void Out(float _speed, UnityAction _action)
    {
        if (InC != null)
        {
            StopCoroutine(InC);
        }
        if (OutC != null)
        {
            StopCoroutine(OutC);
        }
        OutC = OutI(_speed, _action);
        StartCoroutine(OutC);
    }

    private IEnumerator OutI(float _speed, UnityAction _action)
    {
        while (true)
        {
            if (Mathf.Abs(ImageUI_Fade.color.a) < 0.01f)
            {
                ImageUI_Fade.color =
                    new Color(
                        ImageUI_Fade.color.r,
                        ImageUI_Fade.color.g,
                        ImageUI_Fade.color.b,
                        0f);
                if (_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            ImageUI_Fade.color =
                new Color(
                    ImageUI_Fade.color.r,
                    ImageUI_Fade.color.g,
                    ImageUI_Fade.color.b,
                    Mathf.Lerp(
                        ImageUI_Fade.color.a,
                        0f,
                        _speed * Time.deltaTime));

            yield return null;
        }
    }

    private IEnumerator InC = null;

    public void In(float _speed, UnityAction _action)
    {
        if (InC != null)
        {
            StopCoroutine(InC);
        }
        if (OutC != null)
        {
            StopCoroutine(OutC);
        }
        InC = InI(_speed, _action);
        StartCoroutine(InC);
    }

    private IEnumerator InI(float _speed, UnityAction _action)
    {
        while (true)
        {
            if (Mathf.Abs(1 - ImageUI_Fade.color.a) < 0.01f)
            {
                ImageUI_Fade.color =
                       new Color(
                           ImageUI_Fade.color.r,
                           ImageUI_Fade.color.g,
                           ImageUI_Fade.color.b,
                           1f);
                if (_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            ImageUI_Fade.color =
                new Color(
                    ImageUI_Fade.color.r,
                    ImageUI_Fade.color.g,
                    ImageUI_Fade.color.b,
                    Mathf.Lerp(
                        ImageUI_Fade.color.a,
                        1f,
                        _speed * Time.deltaTime));

            yield return null;
        }
    }

    private IEnumerator MoveToC = null;

    public void MoveTo(Transform _target, float _speed, UnityAction _action)
    {
        if(MoveToC != null)
        {
            StopCoroutine(MoveToC);
        }
        MoveToC = MoveToI(_target, _speed, _action);
        StartCoroutine(MoveToC);
    }

    private IEnumerator MoveToI(Transform _target, float _speed, UnityAction _action)
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, _target.position) < 0.01f)
            {
                transform.position = _target.position;
                if (_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator ZoomInOutC = null;

    public void ZoomInOut(float _size, float _speed, UnityAction _action)
    {
        if (ZoomInOutC != null)
        {
            StopCoroutine(ZoomInOutC);
        }
        ZoomInOutC = ZoomInOutI(_size, _speed, _action);
        StartCoroutine(ZoomInOutC);
    }

    private IEnumerator ZoomInOutI(float _size, float _speed, UnityAction _action)
    {
        Camera cam = GetComponent<Camera>();
        while (true)
        {
            if (Mathf.Abs(cam.orthographicSize - _size) < 0.01f)
            {
                cam.orthographicSize = _size;
                if (_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, _size, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
