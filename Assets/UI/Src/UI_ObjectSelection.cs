using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ObjectSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Renderer _renderer;

    Color selectedColor = Color.green;
    Color unselectedColor = Color.white;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material.color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material.color = unselectedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.Rotate(Vector3.up * 45);
    }

}
