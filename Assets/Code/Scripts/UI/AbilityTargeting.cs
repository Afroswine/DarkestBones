using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AbilityTargeting : MonoBehaviour
{
    public Character Character => _character;

    [Tooltip("Is this object displaying and interactable?")]
    public bool IsDisplaying = false;

    [Header("Target Select")]
    [Space]
    [Header("Sprite Positioning")]
    [SerializeField] private Vector3 _positionOffset = new Vector3(0, -6, -1);
    //[SerializeField] private int _yOffset = -6;
    [Header("Attack Targeting Sprites")]
    [SerializeField] Sprite _attackSelect;
    [SerializeField] Sprite _attackOption;
    [Header("Ally Targeting Sprites")]
    [SerializeField] Sprite _allySelect;
    [SerializeField] Sprite _allyOption;
    [Header("Swap Targeting Sprites")]
    [SerializeField] Sprite _swapSelect;
    [SerializeField] Sprite _swapOption;

    //private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Character _character;
    

    private delegate void DisplaySelect();
    private delegate void DisplayOption();
    DisplaySelect _selectHandler;
    DisplayOption _optionHandler;

    private const int _collisionWidth = 32;
    private const int _collisionHeight = 48;
    private Vector2 _collisionSize = new Vector2(_collisionWidth, _collisionHeight);

    private void Awake()
    {
        //_collider = GetComponent<BoxCollider2D>();
        // get component references
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _character = GetComponentInParent<Character>();

        // default handler values
        _selectHandler = DisplayAttackSelect;
        _optionHandler = DisplayAttackOption;

        IsDisplaying = false;
    }

    private void OnEnable()
    {
        _character.Targeted += ActivateTargeting;
        //Debug.Log("OnEnable: " + _character.name);
    }

    private void OnDisable()
    {
        _character.Targeted -= ActivateTargeting;
    }

    private void Start()
    {
        //_collider = GetComponent<BoxCollider2D>();
        // get component references
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _character = GetComponentInParent<Character>();

        // default handler values
        _selectHandler = DisplayAttackSelect;
        _optionHandler = DisplayAttackOption;

        IsDisplaying = false;

        transform.position = _character.transform.position + _positionOffset;
        DetermineDisplay();
    }

    private void ActivateTargeting()
    {
        IsDisplaying = true;
        _optionHandler();
        Debug.Log("activate targeting");
    }

    private void DetermineDisplay()
    {
        if (!IsDisplaying)
        {
            _spriteRenderer.sprite = null;
            return;
        }

        _optionHandler();
    }

    private void OnMouseOver()
    {
        if (!IsDisplaying)
            return;

        _selectHandler();
    }

    private void OnMouseExit()
    {
        if (!IsDisplaying)
            return;

        DetermineDisplay();
    }

    private void DisplayAttackSelect()
    {
        _spriteRenderer.sprite = _attackSelect;
    }
    private void DisplayAttackOption()
    {
        _spriteRenderer.sprite = _attackOption;
    }

    private void DisplayAllySelect()
    {
        _spriteRenderer.sprite = _allySelect;
    }
    private void DisplayAllyOption()
    {
        _spriteRenderer.sprite = _allyOption;
    }

    private void DisplaySwapSelect()
    {
        _spriteRenderer.sprite = _swapSelect;
    }
    private void DisplaySwapOption()
    {
        _spriteRenderer.sprite = _swapOption;
    }
}
