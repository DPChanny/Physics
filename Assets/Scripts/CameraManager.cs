using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    private Coroutine MoveToC = null;

    public void MoveTo(Transform _target, float _speed, UnityAction _action)
    {
        if(MoveToC != null)
        {
            StopCoroutine(MoveToC);
        }
        MoveToC = StartCoroutine(MoveToI(_target, _speed, _action));
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

    private Coroutine ZoomInOutC = null;

    public void ZoomInOut(float _size, float _speed, UnityAction _action)
    {
        if (ZoomInOutC != null)
        {
            StopCoroutine(ZoomInOutC);
        }
        ZoomInOutC = StartCoroutine(ZoomInOutI(_size, _speed, _action));
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
