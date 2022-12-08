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

    [Header("Button")]
    [SerializeField] protected List<Sprite> _sprites = new(3);
    [SerializeField] protected float _pressedDuration = 0.1f;
    [SerializeField] AudioClip _hoverAudio;
    [SerializeField] AudioClip _pressDownAudio;
    [SerializeField] AudioClip _pressUpAudio;

    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _collider;
    private AudioSource _audioSource;

    protected bool _isEnabled = true;
    private bool _isMouseOver = false;
    private bool _isPressed = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.sprite = _sprites[0];
    }

    // controls whether the button can be clicked or selected
    public void Enable(bool isEnabled)
    {
        _isEnabled = isEnabled;

        // ensure button appears neutral
        if (isEnabled)
            return;

        _isMouseOver = false;
        _isPressed = false;
        _spriteRenderer.sprite = _sprites[0];
    }

    public virtual void Set(Button button)
    {
        if (button == null)
            return;

        _sprites = new List<Sprite>(button._sprites);
        _pressedDuration = button._pressedDuration;
        _spriteRenderer.sprite = _sprites[0];
        OnPressed = button.OnPressed;
    }
    public virtual void Set(Button button, List<Sprite> sprites)
    {
        if (button == null)
            return;

        _sprites = new List<Sprite>(sprites);
        _pressedDuration = button._pressedDuration;
        _spriteRenderer.sprite = _sprites[0];
        OnPressed = button.OnPressed;
    }

    #region Mouse Interactions
    // show neutral
    protected void OnMouseExit()
    {
        if (!_isEnabled) return;
        _isMouseOver = false;
        _isPressed = false;
        _spriteRenderer.sprite = _sprites[0];

        if (_audioSource == null) return;
        _audioSource.Stop();
    }

    // show pressed and perform ButtonPress()
    protected void OnMouseDown()
    {
        if (!_isEnabled) return;
        _isPressed = true;
        _spriteRenderer.sprite = _sprites[2];

        if (_pressDownAudio == null) return;
        _audioSource = AudioHelper.PlayClip2D(_pressDownAudio, 1f);
    }

    // show selected 
    protected void OnMouseOver()
    {
        if (!_isEnabled) return;
        _isMouseOver = true;
        if (_isPressed) return;
        _spriteRenderer.sprite = _sprites[1];
    }

    protected void OnMouseEnter()
    {
        if (!_isEnabled) return;
        if (_hoverAudio == null) return;
        //_audioSource = AudioHelper.PlayClip2D(_hoverAudio, 1f);
    }

    // on mouse up, perform actions
    private void OnMouseUp()
    {
        if (!_isEnabled) return;
        if (!_isPressed) return;
        StartCoroutine(ButtonPressedCR());
    }
    #endregion Mouse Interactions END

    // Do stuff
    protected virtual void ButtonPress()
    {
        Debug.Log("Button pressed");
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
            _spriteRenderer.sprite = _sprites[1];
        else
            _spriteRenderer.sprite = _sprites[0];

        if (_pressUpAudio != null)
            _audioSource = AudioHelper.PlayClip2D(_pressUpAudio, 1f);
    }
}
