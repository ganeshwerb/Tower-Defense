using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GoldManager
{
    public static int gold = 200;
    public static void KilledEnemy()
    {
        gold+= 30;
    }

    public static void SpendGold(int amount)
    {
        gold -= amount;
    }
}
