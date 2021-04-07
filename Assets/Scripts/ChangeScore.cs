using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScore : MonoBehaviour
{
    private TargetBehavior targetBehavior;
    // Start is called before the first frame update
    void Start()
    {
        targetBehavior = GetComponent<TargetBehavior>();

        StartCoroutine(DoEveryTwoSeconds());
    }

    IEnumerator DoEveryTwoSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            IncreaseScore();
        }
    }

    // happens every 0.5 seconds
    void IncreaseScore()
    {
        targetBehavior.scoreAmount += 1;
    }

}
