using System.Collections;
using Fungus;
using UnityEngine;
/// <summary>
/// this class is controlling the behavier of the NPC, the dialog are controlling by the external assets fungus. the class is determine the event and call the block in fungus
/// </summary>
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
    /// <summary>
    ///determine whether user press attack to the  NPC
    ///it will called when user hit the NPC  exp(StartCoroutine(Coroutine());)
    ///th method will returned a flowchart block to do the dialog
    /// </summary>
    /// <returns></returns>
	private IEnumerator Coroutine()
	{
		while (!Input.GetButton("Fire1"))
			yield return null;

        flowChart.ExecuteBlock(BlockName);

        while (!flowChart.GetBooleanVariable(HasPurchasedHP))
            yield return null;

        gamectrl.gamecontrl.IncrementPotCount();
	}
    //stop the event when player doensn't collide with NPC
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other == knight.GetComponent<Collider2D>())
		{
            StopAllCoroutines();
		}
	}
}
