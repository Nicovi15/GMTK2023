using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countDownText;
    [SerializeField]
    float countDownSpeed = 1.0f;

    [SerializeField]
    public GameObject HidePanel;

    [SerializeField]
    public Image ImageHidePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountDown(int duration)
    {
        StartCoroutine(CountDown(duration));
    }

    IEnumerator CountDown(int duration)
    {
        countDownText.gameObject.SetActive(true);
        for(int i = duration; i > 0; i--)
        {
            float t = 0;
            countDownText.text = i.ToString();
            while (t < 1)
            {
                t += Time.deltaTime * countDownSpeed;
                countDownText.gameObject.transform.localScale = new Vector3(1.0f - t, 1.0f - t, 1.0f - t);
                yield return null;
            }
        }
        countDownText.gameObject.SetActive(false);
        GameManager.Instance.StartLoadLevel();
    }

    public void ChangeHideColor(Color color)
    {
        ImageHidePanel.color = color;
    }

    public IEnumerator FadeOutHide(float duration)
    {
        ImageHidePanel.color = new Color(ImageHidePanel.color.r, ImageHidePanel.color.g, ImageHidePanel.color.b, 1.0f);
        yield return null;
        float t = 0;
        while(t < duration)
        {
            ImageHidePanel.color = new Color(ImageHidePanel.color.r, ImageHidePanel.color.g, ImageHidePanel.color.b, 1 - (t / duration));
            t += Time.deltaTime;
            yield return null;
        }
        ImageHidePanel.color = new Color(ImageHidePanel.color.r, ImageHidePanel.color.g, ImageHidePanel.color.b, 0.0f);
    }

}
