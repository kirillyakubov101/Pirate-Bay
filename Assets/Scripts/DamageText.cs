using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
   [SerializeField] TMP_Text damageText = null;
  

    public void UpdateDamageTextValue(float amount)
	{
		damageText.text = amount.ToString();
	}



}
