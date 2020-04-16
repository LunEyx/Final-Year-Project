using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Spell
{
    protected GameObject prefab;
    protected float duration;
    protected float projectileSpeed;
    private int meteorCounter = 0;
    public const int MeteorNum = 20;
    public const float Radius = 8;

    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_22");
        prefab = Resources.Load<GameObject>("Fireball");
        duration = 5f;
        cooldown = 3f;
    }

    public override void Cast()
    {
        base.Cast();
        InvokeRepeating("Instantiate", 0, 0.1f);
    }

    protected virtual void Instantiate()
    {
        float randPosX = Random.Range(0 - Radius, Radius);
        float randPosZ = Random.Range(0 - Radius, Radius);
        GameObject meteor = Instantiate(
            prefab,
            new Vector3(transform.position.x + randPosX, transform.position.y + 10f, transform.position.z + randPosZ),
            Quaternion.Euler(90, 0, 0));
        meteor.GetComponent<Rigidbody>().velocity = transform.up * -20;
        meteorCounter++;

        if (meteorCounter >= MeteorNum)
        {
            CancelInvoke();
            meteorCounter = 0;
        }

        Destroy(meteor, duration);
    }
}
