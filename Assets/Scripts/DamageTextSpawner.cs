using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab = null;



    public void Spawn(float damageAmount)
    {
        DamageText damageTextInstance = Instantiate(damageTextPrefab, transform);
        damageTextInstance.UpdateDamageTextValue(damageAmount);
        Destroy(damageTextInstance, 2f);

    }
}