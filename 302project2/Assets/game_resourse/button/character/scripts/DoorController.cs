using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

    public GameObject Knights;
    public Animator animator;
    public string ScaleAnimationName = "Scale";
    public string GoToLevel;

    private MonsterCountController monsterCountController;

    private void Awake()
    {
        monsterCountController = FindObjectOfType<MonsterCountController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Knights.GetComponent<Collider2D>())
        {
            StartCoroutine(Coroutine());
        }
    }

    private IEnumerator Coroutine()
    {
		animator.Play(ScaleAnimationName);
        while (Knight.knights.isgrounded)
            yield return null;

        if (monsterCountController.IsAllMonsterDead())
            SceneManager.LoadScene(GoToLevel);

	}

    private void OnTriggerExit2D(Collider2D other)
    {
		if (other == Knights.GetComponent<Collider2D>())
		{
            StopAllCoroutines();
		}
    }
}
