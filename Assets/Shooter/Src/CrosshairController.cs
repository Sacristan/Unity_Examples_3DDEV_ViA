using UnityEngine;
using System.Collections;

public class CrosshairController: MonoBehaviour
{
	[SerializeField]
	private float pixelWidth = 4;

	[SerializeField]
	private float pixelHeight = 11;

	[SerializeField]
	private int minSpread = 11;

	[SerializeField]
	private int maxSpread = 50;

	[SerializeField]
	private RectTransform topRectTransform;

	[SerializeField]
	private RectTransform bottomRectTransform;

	[SerializeField]
	private RectTransform leftRectTransform;

	[SerializeField]
	private RectTransform rightRectTransform;

	private float spreadDecreaseTime = 1.5f;
	private float currentSpreadPerc = 0f;

	#region Mono

	private void Start()
	{
		Vector2 sizeDelta = new Vector2(pixelWidth, pixelHeight);

		leftRectTransform.sizeDelta = sizeDelta;
		rightRectTransform.sizeDelta = sizeDelta;
		topRectTransform.sizeDelta = sizeDelta;
		bottomRectTransform.sizeDelta = sizeDelta;

		EnableCrossHairGraphics(true);
	}

	private void Update()
	{
		currentSpreadPerc = Mathf.Clamp01(currentSpreadPerc - Time.deltaTime / spreadDecreaseTime);

		float currentSpread = Mathf.Lerp(minSpread, maxSpread, currentSpreadPerc);

		bottomRectTransform.anchoredPosition = -Vector3.up * currentSpread;
		topRectTransform.anchoredPosition = Vector3.up * currentSpread;

		rightRectTransform.anchoredPosition = -Vector3.right * currentSpread;
		leftRectTransform.anchoredPosition = Vector3.right * currentSpread;
	}

	#endregion

	public void TriggerFX()
	{
		currentSpreadPerc += 0.1f;
	}

	private void EnableCrossHairGraphics(bool flag)
	{
		if (leftRectTransform.gameObject.activeSelf != flag)
		{
			leftRectTransform.gameObject.SetActive(flag);
			rightRectTransform.gameObject.SetActive(flag);
			topRectTransform.gameObject.SetActive(flag);
			bottomRectTransform.gameObject.SetActive(flag);
		}
	}

}