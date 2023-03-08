using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IIntListener
{
    static GameManager instance;
    
    public Unit blueNexus;
    public Unit redNexus;
    public GameObject blueVictory;
    public GameObject redVictory;

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        blueNexus.Health.listeners.Add(gameObject);
        redNexus.Health.listeners.Add(gameObject);
    }

    public void IntUpdate(IntWrapper i){
        if (i == blueNexus.Health) BlueNexusUpdate();
        else if (i == redNexus.Health) RedNexusUpdate();
    }

    public void BlueNexusUpdate(){
        if (blueNexus.Health <= 0){
            redVictory.SetActive(true);
            DisableAI();
            StartCoroutine(SlowDown());
        }
    }

    public void RedNexusUpdate(){
        if (redNexus.Health <= 0){
            blueVictory.SetActive(true);
            DisableAI();
            StartCoroutine(SlowDown());
        }
    }

    public void DisableAI()
    {
        AI[] ai = FindObjectsOfType<AI>();
        foreach(AI a in ai)
        {
            a.update = false;
        }
    }

    IEnumerator SlowDown()
    {
        while(Time.timeScale > 0f)
        {
            Time.timeScale -= Time.deltaTime;
            if (Time.timeScale < 0) Time.timeScale = 0f;
            yield return null;
        }
    }
}
