using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Button : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] protected Sprite _spriteNeutral;
    [SerializeField] protected Sprite _spriteSelected;
    [SerializeField] protected Sprite _spritePressed;
    [Header("Pressed Delay")]
    [SerializeField] protected float _pressedDuration = 0.1f;

    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _collider;

    protected bool _isMouseOver = false;
    protected bool _isPressed = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = _spriteNeutral;
    }

    // show selected
    private void OnMouseEnter()
    {
        _isMouseOver = true;
        if (!_isPressed)
        {
            _spriteRenderer.sprite = _spriteSelected;
        }
    }

    // show neutral
    private void OnMouseExit()
    {
        _isMouseOver = false;
        if (!_isPressed)
        {
            _spriteRenderer.sprite = _spriteNeutral;
        }
    }

    // show pressed and perform ButtonPress()
    private void OnMouseDown()
    {
        _isPressed = true;
        _spriteRenderer.sprite = _spritePressed;
    }

    private void OnMouseUp()
    {
        StartCoroutine(ButtonPressedCR());
    }

    // Do stuff
    protected virtual void ButtonPress()
    {
        Debug.Log("Button Pressed");
    }

    // show pressed for a delay, and then display the proper sprite
    private IEnumerator ButtonPressedCR()
    {
        yield return new WaitForSeconds(_pressedDuration / 2);
        ButtonPress();
        yield return new WaitForSeconds(_pressedDuration / 2);
        _isPressed = false;

        if (_isMouseOver)
        {
            _spriteRenderer.sprite = _spriteSelected;
        }
        else
        {
            _spriteRenderer.sprite = _spriteNeutral;
        }
    }
}
