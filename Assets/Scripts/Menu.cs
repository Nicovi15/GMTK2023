using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countDownText;
    [SerializeField]
    float countDownSpeed = 1.0f;

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
        this.gameObject.SetActive(false);
        GameManager.Instance.StartLevel();
    }


}
