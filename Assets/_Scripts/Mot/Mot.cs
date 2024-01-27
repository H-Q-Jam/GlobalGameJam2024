using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mot : MonoBehaviour
{
    [SerializeField] private TeamWhoCanGrab equipe;
    [SerializeField] private List<Lettre> _lettres;
    
    [SerializeField] private bool isComplete;
    public event Action OnMotComplete;
    public event Action OnMotUncomplete;

    public TeamWhoCanGrab Equipe => equipe;
    public List<Lettre> Lettres => _lettres;

    private void OnEnable()
    {
        foreach (Lettre lettre in _lettres)
        {
            lettre.OnLetterComplete += LetterCompleted;
            lettre.OnLetterUncomplete += LetterUnCompleted;
        }
    }

    private void OnDisable()
    {
        foreach (Lettre lettre in _lettres)
        {
            lettre.OnLetterComplete -= LetterCompleted;
            lettre.OnLetterUncomplete -= LetterUnCompleted;
        }
    }
    [SerializeField] private int nbLetterCompleted = 0;
    private void LetterCompleted()
    {
        nbLetterCompleted = Mathf.Clamp(nbLetterCompleted+1,0,_lettres.Count);
        if (nbLetterCompleted >= _lettres.Count && !isComplete)
        {
            isComplete = true;
            OnMotComplete?.Invoke();
        }
    }
    private void LetterUnCompleted()
    {
        nbLetterCompleted = Mathf.Clamp(nbLetterCompleted-1,0,_lettres.Count);
        if (nbLetterCompleted < _lettres.Count && isComplete)
        {
            isComplete = false;
            OnMotUncomplete?.Invoke();
        }
    }

}
