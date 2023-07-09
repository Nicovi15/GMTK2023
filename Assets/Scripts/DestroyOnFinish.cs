using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinish : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndBind());
    }

    IEnumerator WaitAndBind()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.Instance.CurrentLevel.finalTarget.OnReachTarget += DestroyWhenFinish;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyWhenFinish()
    {
        Debug.Log("hey");
        Destroy(this.gameObject);
    }
}
