using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;

	private Transform _playerTransform;

    private IEnumerator Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        yield return new WaitForSeconds(2f);

        // StartCoroutine(AttackPlayer());
    }

    private IEnumerator AttackPlayer()
    {
        GetComponent<Animator>().SetBool("IsAttacking", true);

        while(true)
        {
            Vector3 direction = _playerTransform.position - transform.position;
            direction.Normalize();

            Vector3 deltaMovementVector = direction * Time.deltaTime * speed;

            transform.position += deltaMovementVector;
            transform.LookAt(_playerTransform);

            yield return null;
        }
    }

}
