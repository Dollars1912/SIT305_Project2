using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCountController : MonoBehaviour {
    /// <summary>
    /// count the number of enemy in the scene,only when user kill all the enemy he can pass the level
    /// the class when called when user destroy enemy   (exp:MonsterCountController.IsAllMonsterDead())
    /// the class will return a boolean value, only when IsAllMonsterDead=true, user can pass the level
    /// </summary>
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
