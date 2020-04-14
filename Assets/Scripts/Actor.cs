using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public enum Effect { Bubbled };

    // status
    protected HpSystem hpSystem;
    protected Image hpBar;

    // bubbled debuff
    protected GameObject bubblePrefab;
    protected bool bubbled = false;
    protected GameObject bubble;

    protected virtual void Start()
    {
        bubblePrefab = Resources.Load<GameObject>("EnemyInBubble");
    }

    protected int GetHp()
    {
        return hpSystem.GetHp();
    }

    public virtual void TakeDamage(int value)
    {
        hpSystem.TakeDamage(value);
        hpBar.fillAmount = hpSystem.CurrentLifePercentage();
    }

    IEnumerator Bubbled(float duration)
    {
        bubbled = true;
        Destroy(bubble);
        bubble = Instantiate(bubblePrefab, transform);
        yield return new WaitForSeconds(duration);
        bubbled = false;
        Destroy(bubble);
    }

    public void ApplyEffect(Effect effect, float duration)
    {
        StopCoroutine(effect.ToString());
        StartCoroutine(effect.ToString(), duration);
    }
}
