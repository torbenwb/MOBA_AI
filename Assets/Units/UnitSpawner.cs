using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public float startDelay = 10f;
    public float waveRate = 30f;
    public float spawnRate = 1f;
    public MinionWave_SO minionWave;
    public NavPath path;
    public bool reversePath;

    private void Start() => StartCoroutine(Waves());

    IEnumerator Waves(){
        yield return new WaitForSeconds(startDelay);
        while(true){
            StartCoroutine(SpawnWave(minionWave));
            yield return new WaitForSeconds(waveRate);
        }
    }

    IEnumerator SpawnWave(MinionWave_SO minionWave){
        Vector3 spawnPoint = reversePath ? path.pathPoints[path.pathPoints.Count - 1] : path.pathPoints[0];

        for(int i = 0; i < minionWave.Minions.Count; i++){
            GameObject newUnit = Instantiate(minionWave.Minions[i], spawnPoint, Quaternion.identity, transform);
            newUnit.layer = gameObject.layer;
            newUnit.GetComponent<FollowPath>().path = path;
            newUnit.GetComponent<FollowPath>().followReverse = reversePath;
            newUnit.GetComponent<FollowPath>().pathIndex = reversePath ? path.pathPoints.Count - 1 : 0;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
