using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject explosionDetector;
    void Start()
    {
        animator = GetComponent<Animator>();
        explosionDetector.SetActive(false);
    }

    private void Update()
    {
        StartCoroutine(Explosion());
        
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("ReadyExplosion", true);
        explosionDetector.SetActive(true);
        Destroy(gameObject, .8f);
    }
  
}
