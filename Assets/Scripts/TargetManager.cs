using UnityEngine;

public class TargetManager : MonoBehaviour
{
    Vector3 firstStagePosition = new Vector3(-0.49f, 3.84f, -5.81f);
    Vector3 secondStagePosition = new Vector3(-0.49f, 5.636f, -7.74f);
    Vector3 thirdStagePosition = new Vector3(-0.49f, 7.347f, -9.66f);
    GameObject target;

    void Start()
    {
        target = (GameObject)Resources.Load("Target");
        Instantiate(target, firstStagePosition, target.transform.rotation);
        Instantiate(target, secondStagePosition, target.transform.rotation);
        Instantiate(target, thirdStagePosition, target.transform.rotation);
    }

    void Update()
    {
        
    }

    

    }
