using System.Collections;
using Fungus;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public string BlockName;
    private Flowchart flowChart;
    private GameObject knight;

    private void Awake()
    {
        flowChart = FindObjectOfType<Flowchart>();
		knight = FindObjectOfType<Knight>().gameObject;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other == knight.GetComponent<Collider2D>())
		{
			StartCoroutine(Coroutine());
		}
	}

	private IEnumerator Coroutine()
	{
		while (!Input.GetButton("Fire1"))
			yield return null;

        flowChart.ExecuteBlock(BlockName);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other == knight.GetComponent<Collider2D>())
		{
            StopAllCoroutines();
		}
	}
}
