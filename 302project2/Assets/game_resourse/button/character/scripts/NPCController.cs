using System.Collections;
using Fungus;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public string BlockName;
    public string HasPurchasedHP = "HasPurchasedHP";

    private Flowchart flowChart;
    private GameObject knight;
    private HPPotCountController potCountController;

    private void Awake()
    {
        flowChart = FindObjectOfType<Flowchart>();
		knight = FindObjectOfType<Knight>().gameObject;
        potCountController = FindObjectOfType<HPPotCountController>();
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

        while (!flowChart.GetBooleanVariable(HasPurchasedHP))
            yield return null;

        gamectrl.gamecontrl.IncrementPotCount();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other == knight.GetComponent<Collider2D>())
		{
            StopAllCoroutines();
		}
	}
}
