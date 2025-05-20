using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitM;
    private Material originalM;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalM = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitM;
        yield return new WaitForSeconds(.2f) ;
        sr.material = originalM;
    }

    private void RedColorBlink()
    {
        if(sr.color != Color.white)        
            sr.color = Color.white;       
        else 
            sr.color = Color.red;
    }
    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
