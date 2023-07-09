using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    GameObject TimeObject;
    [SerializeField]
    GameObject DeathObject;
    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI DeathText;
    [SerializeField]
    Image BorderTop;
    [SerializeField]
    Image BorderBot;

    public Action OnEndLevelComplete;

    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowLevelComplete(int time, int death, Color color)
    {
        BorderBot.gameObject.SetActive(true);
        BorderTop.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        TimeObject.SetActive(false);
        DeathObject.SetActive(false);

        RectTransform rtBot = BorderBot.GetComponent<RectTransform>();
        RectTransform rtTop = BorderTop.GetComponent<RectTransform>();

        float t = 0;
        while(t < 1)
        {
            rtBot.anchoredPosition = new Vector2(0, -810 + t * 270.0f);
            rtTop.anchoredPosition = new Vector2(0, 810 - t * 270.0f);
            title.gameObject.transform.localScale = new Vector3(t * 1.1f, t * 1.1f, t * 1.1f);
            t += Time.deltaTime * speed;
            yield return null;
        }

        TimeObject.SetActive(true);
        DeathObject.SetActive(true);
        TimeText.text = time.ToString();
        DeathText.text = death.ToString();

        yield return new WaitForSeconds(2.5f);

        t = 0;
        while (t < 1)
        {
            rtBot.anchoredPosition = new Vector2(0, -540 + t * 270.0f);
            rtTop.anchoredPosition = new Vector2(0, 540 - t * 270.0f);
            t += Time.deltaTime * speed;
            yield return null;
        }

        OnEndLevelComplete?.Invoke();
        this.gameObject.SetActive(false);
    }
}
