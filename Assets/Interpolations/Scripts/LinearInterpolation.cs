using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    [SerializeField]
    private Transform startTransform;

    [SerializeField]
    private Transform endTransform;

    [SerializeField]
    private float movementTimeInSeconds = 5f;

    private IEnumerator Start()
    {
        while(true){
            MyUpdate();
            yield return null;
            MyLateUpdate();

        }
    }

private void MyUpdate(){

}

private void MyLateUpdate(){
    
}

    private IEnumerator MoveRoutine(){
        float t = 0f;

        while(true)
        {
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, t);
            t += Time.deltaTime / movementTimeInSeconds;
            yield return new WaitForSeconds(50f);
        }
    }

}
