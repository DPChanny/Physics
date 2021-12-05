using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (Mathf.Abs(spriteRenderer.color.a) < 0.01f)
            {
                spriteRenderer.color = 
                    new Color(
                        spriteRenderer.color.r, 
                        spriteRenderer.color.g, 
                        spriteRenderer.color.b, 
                        0f);
                if(_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            spriteRenderer.color = 
                new Color(
                    spriteRenderer.color.r, 
                    spriteRenderer.color.g, 
                    spriteRenderer.color.b, 
                    Mathf.Lerp(
                        spriteRenderer.color.a, 
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
            if (Mathf.Abs(1 - spriteRenderer.color.a) < 0.01f)
            {
                spriteRenderer.color =
                       new Color(
                           spriteRenderer.color.r,
                           spriteRenderer.color.g,
                           spriteRenderer.color.b,
                           1f);
                if (_action != null)
                {
                    _action.Invoke();
                }
                yield break;
            }

            spriteRenderer.color =
                new Color(
                    spriteRenderer.color.r,
                    spriteRenderer.color.g,
                    spriteRenderer.color.b,
                    Mathf.Lerp(
                        spriteRenderer.color.a,
                        1f,
                        _speed * Time.deltaTime));

            yield return null;
        }
    }
}
