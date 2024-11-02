using UnityEngine;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    Vector3 firstStagePosition = new Vector3(-0.49f, 3.84f, -5.81f);
    Vector3 secondStagePosition = new Vector3(-0.49f, 5.636f, -7.74f);
    Vector3 thirdStagePosition = new Vector3(-0.49f, 7.347f, -9.66f);
    private const int stageNum = 3;
    Vector3[] stagePositions = new Vector3[stageNum];
    GameObject[] targetPrefabs = new GameObject[2];
    GameObject[] stageTargets = new GameObject[stageNum];
    private int randomNumber;
    

    void Start()
    {
        targetPrefabs = new GameObject[] {(GameObject)Resources.Load("NormalTarget"), (GameObject)Resources.Load("RainbowTarget")};
        stagePositions = new Vector3[] {firstStagePosition, secondStagePosition, thirdStagePosition};
        for(int i = 0; i < stageNum; i++)
        {
            stageTargets[i] = Instantiate(targetPrefabs[0], stagePositions[i], targetPrefabs[0].transform.rotation);
            stageTargets[i].GetComponent<Target>().cycleTime = (float)(i+1)/stageNum;
            Debug.Log(stageTargets[i].GetComponent<Target>().cycleTime);
        }
    }

    void Update()
    {
        for(int i = 0; i < stageNum; i++)
        {
            if(stageTargets[i] == null)
            {
                SpawnTarget(i);
            }
        }
    }
    public void OnGameFinish()
    {
        Target.SetGameFinish();
        for (int i = 0; i < stageNum; i++)
        {
            Destroy(stageTargets[i]);
        }
    }
    // 階数指定は欧米式(0から始まる)
    // TODO いつか的が壊れた位置で新しい的を生成するようにする(Target.csのOnDestroy()時にその位置をどこかで補完して以下の関数でその値を使うようにする)
    private void SpawnTarget(int stage)
    {
        randomNumber = (int)Random.Range(0, 100);
        Debug.Log(randomNumber);
        if(randomNumber < 80)
        {
            stageTargets[stage] = Instantiate(targetPrefabs[0], stagePositions[stage], targetPrefabs[0].transform.rotation);
            stageTargets[stage].GetComponent<Target>().cycleTime = (float)(stage+1)/stageNum;
        }
        else
        {
           stageTargets[stage] = Instantiate(targetPrefabs[1], stagePositions[stage], targetPrefabs[1].transform.rotation);
           stageTargets[stage].GetComponent<Target>().cycleTime = (float)(stage+1)/stageNum;
        }
    }
}