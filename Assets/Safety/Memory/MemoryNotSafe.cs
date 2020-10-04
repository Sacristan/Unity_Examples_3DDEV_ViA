using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryNotSafe : MonoBehaviour
{
    int score = 0;

    private IEnumerator Start()
    {
        Text text = GetComponent<Text>();

        YieldInstruction wait = new WaitForSeconds(10f);

        while (true)
        {
            score = Random.Range(0, 1000);
            text.text = score.ToString();
            yield return wait;
        }
    }

}
