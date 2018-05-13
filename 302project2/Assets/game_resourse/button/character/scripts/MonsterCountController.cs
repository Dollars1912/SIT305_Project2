using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCountController : MonoBehaviour {
 
    private Rigidbody2D[] Monsters;

    private void Awake()
    {
        Monsters = gameObject.GetComponentsInChildren<Rigidbody2D>();
    }

    public bool IsAllMonsterDead()
    {
        foreach (var monster in Monsters)
            if (monster != null)
                return false;

        return true;
    }
}
