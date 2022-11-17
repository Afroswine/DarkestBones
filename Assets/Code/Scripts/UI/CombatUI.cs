using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [Header("CombatUI")]
    [SerializeField] SpriteRenderer _portraitRenderer;
    [Header("Abilities")]
    [SerializeField] Button _abilityButton1;
    [SerializeField] Button _abilityButton2;
    [SerializeField] Button _abilityButton3;
    [SerializeField] Button _abilityButton4;
    [SerializeField] Button _swapButton;
    [SerializeField] Button _skipButton;

    private Button[] _buttons = new Button[6];
    //private Button[] _abilityButtons = new Button[4];

    private void Start()
    {
        _buttons[0] = _abilityButton1;
        _buttons[1] = _abilityButton2;
        _buttons[2] = _abilityButton3;
        _buttons[3] = _abilityButton4;
        _buttons[4] = _swapButton;
        _buttons[5] = _skipButton;

        //_abilityButtons[0] = _abilityButton1;
        //_abilityButtons[1] = _abilityButton2;
        //_abilityButtons[2] = _abilityButton3;
        //_abilityButtons[3] = _abilityButton4;
    }

    public void EnableButtons(bool isEnabled)
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Enable(isEnabled);
        }
    }

    public void SetAbilities(Button[] abilityButtons)
    {
        if(abilityButtons[0] != null)
        {
            //_abilityButton1.gameObject.SetActive(false);
            _abilityButton1 = abilityButtons[0];
            GameObject gameObject = Instantiate(abilityButtons[0].gameObject, _abilityButton1.transform.position, _abilityButton1.transform.rotation);
            _abilityButton1 = gameObject.GetComponent<Button>();
        }
        else
        {
            _abilityButton1.Enable(false);
        }
        if (abilityButtons[1] != null)
        {
            _abilityButton2 = abilityButtons[1];
            GameObject gameObject = Instantiate(_abilityButton2.gameObject, _abilityButton2.transform.position, _abilityButton2.transform.rotation);
            _abilityButton2 = gameObject.GetComponent<Button>();
        }
        else
        {
            _abilityButton2.Enable(false);
        }
        if (abilityButtons[2] != null)
        {
            _abilityButton3 = abilityButtons[2];
            GameObject gameObject = Instantiate(_abilityButton3.gameObject, _abilityButton3.transform.position, _abilityButton3.transform.rotation);
            _abilityButton3 = gameObject.GetComponent<Button>();
        }
        else
        {
            _abilityButton3.Enable(false);
        }
        if (abilityButtons[3] != null)
        {
            _abilityButton4 = abilityButtons[3];
            GameObject gameObject = Instantiate(_abilityButton4.gameObject, _abilityButton4.transform.position, _abilityButton4.transform.rotation);
            _abilityButton4 = gameObject.GetComponent<Button>();
        }
        else
        {
            _abilityButton4.Enable(false);
        }

    }

    public void SetHero(Hero hero)
    {
        // set the ability buttons based on the given hero
        // set the sprite of the spriteRenderer
        _portraitRenderer.sprite = hero.Portrait;
        SetAbilities(hero.AbilityButtons);
    }
}
