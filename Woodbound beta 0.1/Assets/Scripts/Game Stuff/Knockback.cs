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
                hit.AddForce(difference, ForceMode2D.Impulse);
                //StartCoroutine(stopKnockBackCo(hit, difference));
                if (other.gameObject.CompareTag("enemy") && other.isTrigger &&
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentState == PlayerState.attack||
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentState == PlayerState.cast||
                    gameObject.CompareTag("Item"))
                {
                    Debug.Log("hittt enemy");
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (gameObject.CompareTag("playerskills")) return;
                    if (other.GetComponent<PlayerController>().currentState != PlayerState.stagger)
                    {
                        Debug.Log("hittt player");
                        if (hit.GetComponent<PlayerController>().currentState == PlayerState.shield)
                        {
                            Debug.Log("hittt player, but player is shielded!");
                            return;
                        }
                        hit.GetComponent<PlayerController>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerController>().Knock(knockTime, damage); 
                    }
                }
            }
        }
    }

    private IEnumerator stopKnockBackCo(Rigidbody2D hit, Vector2 difference)
    {
        yield return new WaitForSeconds(1f);
        hit.AddForce(-difference, ForceMode2D.Impulse);
        hit.GetComponent<PlayerController>().currentState = PlayerState.idle;
    }

}
