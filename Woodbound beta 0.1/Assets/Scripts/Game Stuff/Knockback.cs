using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust;
    public float knockTime = 1f;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference * 2, ForceMode2D.Impulse);
                Debug.Log("adding force to " + other.name + " force = " + difference * 2);
                //StartCoroutine(stopKnockBackCo(hit, difference));
                if (other.gameObject.CompareTag("enemy") && other.isTrigger && 
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentState == PlayerState.attack)
                {
                    //Debug.Log("hittt enemy");
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        //Debug.Log("hittt player");
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage); 
                    }
                }
            }
        }
    }

    private IEnumerator stopKnockBackCo(Rigidbody2D hit, Vector2 difference)
    {
        yield return new WaitForSeconds(1f);
        hit.AddForce(-difference, ForceMode2D.Impulse);
        hit.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
    }

}
