using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text goldText = null;

    public static GoldUpdate Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
    {
        goldText.text = LevelController.Instance.goldAmount.ToString();
    }

	public void UpdateGoldText(float goldAmount)
	{
		goldText.text = goldAmount.ToString();
	}

   
}
