using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] Image healthBar = null;
	[SerializeField] TMP_Text levelText = null;
	[SerializeField] LivingEntity self = null;

	

	private void Start()
	{
		healthBar.rectTransform.localScale = new Vector3(self.GetHealthPercentage(), 1f, 1f);
		levelText.text = self.level.ToString();
	}

	private void Update()
	{
		healthBar.rectTransform.localScale = new Vector3(self.GetHealthPercentage(), 1f, 1f);
		levelText.text = self.level.ToString();
	}
}
