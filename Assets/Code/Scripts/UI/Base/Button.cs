using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Button : MonoBehaviour
{
    [Header("On Press")]
    public UnityEvent OnPressed;

    [Header("Sprites")]
    [SerializeField] protected Sprite[] _sprites;
    [Header("Pressed Delay")]
    [SerializeField] protected float _pressedDuration = 0.1f;

    private bool _isEnabled = true;

    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _collider;

    protected bool _isMouseOver = false;
    protected bool _isPressed = false;
    
    // controls whether the button can be clicked or selected
    public void Enable(bool isEnabled)
    {
        _isEnabled = isEnabled;

        // ensure button appears neutral
        if (!isEnabled)
        {
            _isMouseOver = false;
            _isPressed = false;
            _spriteRenderer.sprite = _sprites[0];
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = _sprites[0];
    }

    // show selected
    private void OnMouseEnter()
    {
        if (_isEnabled)
        {
            _isMouseOver = true;
            if (!_isPressed)
            {
                _spriteRenderer.sprite = _sprites[1];
            }
        }
    }

    // show neutral
    private void OnMouseExit()
    {
        _isMouseOver = false;
        if (!_isPressed)
        {
            _spriteRenderer.sprite = _sprites[0];
        }
    }

    // show pressed and perform ButtonPress()
    private void OnMouseDown()
    {
        if (_isEnabled)
        {
            _isPressed = true;
            _spriteRenderer.sprite = _sprites[2];
        }
    }

    // on mouse up, perform actions
    private void OnMouseUp()
    {
        if (_isEnabled)
        {
            StartCoroutine(ButtonPressedCR());
        }
    }

    // Do stuff
    protected void ButtonPress()
    {
        OnPressed.Invoke();
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
            _spriteRenderer.sprite = _sprites[1];
        }
        else
        {
            _spriteRenderer.sprite = _sprites[0];
        }
    }
}
