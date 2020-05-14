using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNova : Spell
{
    protected GameObject prefab;
    protected float duration;
    public static int Damage = 10;
    public static string UpgradeDes = "Decrease cooldown by 1 second!";
    public static string NewDes = "Cast a fire nova that will damage all nearby enemies!";

    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/FireNova");
        prefab = Resources.Load<GameObject>("FireNova");
        duration = 5f;
        cooldown = 3f;
    }

    public override void Upgrade()
    {
        if (cooldown == 1f)
        {
            UpgradeDes = "Increase the damage by 5!";
            Damage += 5;
        }
        else
            cooldown -= 1f;
    }

    public static string GetNewDescription()
    {
        return NewDes;
    }

    public static string GetUpgradeDescription()
    {
        return UpgradeDes;
    }

    public override void Cast()
    {
        base.Cast();
        Instantiate();
    }

    protected virtual void Instantiate()
    {
        GameObject fireNova = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y-0.7f,transform.position.z), Quaternion.identity);
        fireNova.transform.parent = GameManager.GetLocalPlayer().transform;
        Destroy(fireNova, 0.7f);
    }
}
