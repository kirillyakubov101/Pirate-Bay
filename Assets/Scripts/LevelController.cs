using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

	public Texture2D cursorMain = null;
	public Texture2D cursorAttack = null;
	public Texture2D cursorShop = null;
	public ParticleSystem cursorMove = null;

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

	private void Start()
	{
		SetNormalCursor();
		
	}

	public int goldAmount = 100;

	public void Purchase(int cost)
	{
		goldAmount -= cost;
		GoldUpdate.Instance.UpdateGoldText(goldAmount);
	}

	public void GainGold(int amount)
	{
		goldAmount += amount;
		GoldUpdate.Instance.UpdateGoldText(goldAmount);
	}

	public void SetNormalCursor()
	{
		Cursor.SetCursor(cursorMain, Vector3.zero, CursorMode.Auto);
	}

	public void SetAttackCursor()
	{
		Cursor.SetCursor(cursorAttack, Vector3.zero, CursorMode.Auto);
	}

	public void SetShopCursor()
	{
		Cursor.SetCursor(cursorShop, Vector3.zero, CursorMode.Auto);
	}
}
