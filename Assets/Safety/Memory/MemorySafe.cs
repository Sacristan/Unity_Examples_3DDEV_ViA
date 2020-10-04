using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MemorySafe : MonoBehaviour
{
    Safe.Int score = new Safe.Int();

    private IEnumerator Start()
    {
        Text text = GetComponent<Text>();

        YieldInstruction wait = new WaitForSeconds(10f);

        while (true)
        {
            score = new Safe.Int(Random.Range(0, 1000));
            text.text = score.ToString();
            yield return wait;
        }
    }

}
