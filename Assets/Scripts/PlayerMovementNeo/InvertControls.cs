using System.Collections;
using UnityEngine;

public class InvertControls : MonoBehaviour
{
    private Coroutine invertCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            invertCoroutine = StartCoroutine(InvertControlsAfterDelay(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            if (invertCoroutine != null)
            {
                StopCoroutine(invertCoroutine);
                invertCoroutine = null;
            }

            MovementControllerTest movement = other.GetComponent<MovementControllerTest>();
            if (movement != null)
            {
                movement.InvertControls(false);
            }
        }
    }

    private IEnumerator InvertControlsAfterDelay(Collider player)
    {
        yield return new WaitForSeconds(1f);

        MovementControllerTest movement = player.GetComponent<MovementControllerTest>();
        if (movement != null)
        {
            movement.InvertControls(true);
        }
    }
}

