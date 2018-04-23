using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

    public GameObject Knight;
    public Animator animator;
    public string ScaleAnimationName = "Scale";
    public string GoToLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Knight.GetComponent<Collider2D>())
        {
            StartCoroutine(Coroutine());
        }
    }

    private IEnumerator Coroutine()
    {
		animator.Play(ScaleAnimationName);

        //yield return new WaitForSeconds(3);

        while (!Input.GetButton("Jump"))
            yield return null;

        SceneManager.LoadScene(GoToLevel);
	}

    private void OnTriggerExit2D(Collider2D other)
    {
		if (other == Knight.GetComponent<Collider2D>())
		{
            StopAllCoroutines();
		}
    }
}
