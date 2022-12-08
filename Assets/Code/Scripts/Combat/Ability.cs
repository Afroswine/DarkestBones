using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ability : MonoBehaviour
{
    public List<Sprite> ButtonSprites => _buttonSprites;
    public Character Caster;

    [Header("Ability")]
    [SerializeField] private List<Sprite> _buttonSprites;
    [SerializeField] TargetType _targetType;
    [Space]
    [SerializeField] protected bool _selfTarget = false;
    [SerializeField] protected bool _multiTarget = false;
    [SerializeField] protected List<bool> _targetablePositions = new(4);

    enum TargetType { foe, ally, self, none }
    protected Character _target;
    protected List<Character> _targets;

    private void OnValidate()
    {
        if (_targetablePositions.Count != 4)
        {
            Debug.LogWarning("No more than " + 4 + " targetable positions!");
            _targetablePositions.Resize(4);
        }
    }

    [Tooltip("Set the target for this ability.")]
    public virtual void SetTarget(Character character)
    {
        _target = character;
    }
    public virtual void SetTarget(List<Character> characters)
    {
        _targets = new List<Character>(characters);
    }

    // Cast the ability on the target
    public virtual void Cast()
    {

    }

    // Highlight potential targets
    public virtual void HighlightTargets()
    {
        //Debug.Log("HighlightTargets()");
        if (_targetType == TargetType.foe)
        {
            //Debug.Log("Is Foe");
            List<Character> foes = new(Caster.OpposingParty.PartyMembers);
            for(int i = 0; i < foes.Count; i++)
            {
                if (_targetablePositions[i])
                {
                    //foes[i].InvokeTargeted();
                    foes[i].TargetSelect.IsDisplaying = true;
                    Debug.Log(foes[i].name + " InvokeTargeted()!");
                }
            }
        }

        if(_targetType == TargetType.self)
        {
            Caster.InvokeTargeted();
            Caster.TargetSelect.IsDisplaying = true;
            Debug.Log(Caster.name + " InvokeTargeted()!");
        }
    }

    #region Editor
    #if UNITY_EDITOR
    /*
    [CustomEditor(typeof(Ability))]
    public class AbilityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Ability ability = (Ability)target;


            List<bool> targetablePositions = ability._targetablePositions;

            if (ability._targetType == TargetType.ally)
            {
                ability._selfTarget = GUILayout.Toggle(ability._selfTarget, "Self Target");

                if (!ability._selfTarget)
                {
                    ability._multiTarget = GUILayout.Toggle(ability._multiTarget, "Multi-Target");
                    //ability._targetablePositions = EditorGUILayout.Foldout(ability)
                    for(int i = 0; i < targetablePositions.Count; i++)
                    {
                        targetablePositions[i] = GUILayout.Toggle(targetablePositions[i], "Position " + i);
                    }
                }
            }

            if (ability._targetType == TargetType.foe)
            {
                ability._multiTarget = GUILayout.Toggle(ability._multiTarget, "Multi-Target");

            }
            
        }
    }
    */
    #endif
    #endregion
}
