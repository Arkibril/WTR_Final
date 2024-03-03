using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VHS;

public class PlayerManager : MonoBehaviour
{
    public Image fillImage;
    public Image fillImageBG;

    public float sprint = 100f;
    public float maxSprint = 100f;

    private FirstPersonController _firstPersonController;

    private const string VerticalAxis = "Vertical";
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode SprintKey = KeyCode.LeftShift;

    public float RunLevel = 20.0f;
    public float WalkLevel = 5.0f;

    public bool m_sprint;
    public bool m_run;
    public bool m_walk;

    private Coroutine fadeCoroutine;
    private float fadeDuration = 0.5f;
    private float targetAlpha = 0f;

    private void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        SprintOrRun();
        Run();
        RegenSprintBar();

        UpdateFillImage();
    }

    private void UpdateFillImage()
    {
        float fillValue = sprint / maxSprint;
        fillImage.fillAmount = fillValue;

        float halfWidth = fillImage.rectTransform.rect.width * 1f;
        float centeredPosition = (0.5f - fillValue * 0.5f) * halfWidth;
        fillImage.rectTransform.localPosition = new Vector3(centeredPosition, 0, 0);

        bool shouldShowFill = fillValue > 0f && (Input.GetButton(VerticalAxis) && Input.GetKey(SprintKey) || Input.GetButton(HorizontalAxis) && Input.GetKey(SprintKey));

        if (shouldShowFill)
        {
            targetAlpha = 1f;
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeFillImage(true));
        }
        else
        {
            targetAlpha = 0f;
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeFillImage(false));
        }
    }

    private IEnumerator FadeFillImage(bool fadeIn)
    {
        Color currentColor = fillImage.color;
        float startAlpha = currentColor.a;
        float timer = 0.3f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fillImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            fillImageBG.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        fillImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

    private void Run()
    {
        if (sprint > 0 && !_firstPersonController.movementInputData.IsCrouching)
        {
            if (Input.GetButton(VerticalAxis) && Input.GetKey(SprintKey) || Input.GetButton(HorizontalAxis) && Input.GetKey(SprintKey))
            {
                sprint -= 10 * Time.deltaTime;
            }
        }
    }

    private void SprintOrRun()
    {
        if (sprint > RunLevel)
        {
            m_sprint = true;
            m_run = false;
            m_walk = false;
        }
        else if (sprint < RunLevel && sprint > WalkLevel)
        {
            m_sprint = false;
            m_run = true;
            m_walk = false;
        }
        else if (sprint <= WalkLevel)
        {
            m_sprint = false;
            m_run = false;
            m_walk = true;
        }
    }

    private void RegenSprintBar()
    {
        if (m_walk || (sprint != maxSprint && _firstPersonController.m_currentSpeed <= _firstPersonController.walkSpeed) || (sprint != maxSprint && _firstPersonController.m_currentSpeed == 0))
        {
            StartCoroutine(Regeneration());
        }
        else if (sprint > RunLevel)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Regeneration()
    {
        yield return new WaitForSeconds(2f);
        if (sprint <= maxSprint)
        {
            sprint += 20 * Time.deltaTime;
        }
    }
}
