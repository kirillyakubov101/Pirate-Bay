using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public static Shop Instance { get; private set; }

	[SerializeField] GameObject shopObjecet = null;
	[Header("DAMAGE")]
	[SerializeField] int damagePrice = 50;
	[SerializeField] float bonusDamage = 20;
	[SerializeField] TMP_Text damagePriceText = null;
	[Header("HEALTH")]
	[SerializeField] int healthPrice = 50;
	[SerializeField] float bonushealth = 50;
	[SerializeField] TMP_Text healthPriceText = null;
	[Header("FIX")]
	[SerializeField] int fixPrice = 50;
	[SerializeField] TMP_Text fixPriceText = null;


	PlayerMovement player;



	[HideInInspector] public bool isShopOpen = false;

	private void Awake()
	{
		
		if (Instance == null)
		{
			player = FindObjectOfType<PlayerMovement>();
			Instance = this;
			
		}
		else
		{
			Destroy(gameObject);
		}
		
	}

	private void Start()
	{
		damagePriceText.text = damagePrice.ToString();
		healthPriceText.text = healthPrice.ToString();
		fixPriceText.text = fixPrice.ToString();
	}

	public void ManageShop()
	{
		shopObjecet.SetActive(isShopOpen);
	}

	public void CloseShop()
	{
		isShopOpen = false;
		ManageShop();
	}

	public void Buy_Damage()
	{
		if (LevelController.Instance.goldAmount >= damagePrice)
		{
			LevelController.Instance.Purchase(damagePrice);
			player.totalDamage += bonusDamage;
			bonusDamage += 15;
			damagePrice += 50;
			damagePriceText.text = damagePrice.ToString();
		}
	}

	public void Buy_Health()
	{
		if (LevelController.Instance.goldAmount >= healthPrice)
		{
			LevelController.Instance.Purchase(healthPrice);
			player.hitpoints += bonushealth;
			player.maxHitpoints = player.hitpoints;
			bonushealth += 25;
			healthPrice += 75;
			healthPriceText.text = healthPrice.ToString();
		}
	}

	public void Heal()
	{
		if (LevelController.Instance.goldAmount >= fixPrice)
		{
			LevelController.Instance.Purchase(fixPrice);
			player.hitpoints = player.maxHitpoints;
			fixPriceText.text = fixPrice.ToString();
		}
		
	}



}
