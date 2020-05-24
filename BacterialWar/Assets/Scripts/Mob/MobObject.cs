using UnityEngine;
using System.Collections;

public class MobObject : HexContent
{
    public float Health = 100;
    public bool IsFrozen = false;


    //public int RiachRange = 1;
    //public float Damage = 10;

    /*
    //todo move it to some MobBatte
    //todo one will attack, another will be attacked
    //todo make interface for this method
    /// <summary>
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns>Returns if attacked mob still alive.</returns>
    public bool Attacked(MobObject enemy)
    {
        enemy.Health -= Damage;
        return enemy.Health > 0;
    }*/
}
