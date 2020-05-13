using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : EnemyFireball
{
    protected override void Instantiate()
    {
        for (int i = 0; i < 36; i++){
            float angle = 10;
            float curretnRotation = angle * i;
            GameObject fireball = Instantiate(prefab, transform.position, transform.rotation);
            fireball.transform.Rotate(new Vector3(0, curretnRotation, 0));
            fireball.transform.Translate(new Vector3(10, 1, 0));
            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 40f;
            Destroy(fireball, duration);
        }
    }
}
