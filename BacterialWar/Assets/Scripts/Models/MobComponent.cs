using UnityEngine;
using System.Collections;

public class MobComponent : MonoBehaviour
{
    //todo make setters private
    public float Health = 100;
    public int RiachRange = 1;
    public float Damage = 10;

    /// <summary>
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns>Returns if mob still alive.</returns>
    public bool Attack(MobComponent enemy)
    {
        Health -= enemy.Damage;      

        return Health > 0;
    }
}
