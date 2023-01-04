using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Info;
    
    public static TextManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
    public void TryTextInfoInput(string _txt_Info)
    {
        StartCoroutine(TextInfoInput(_txt_Info));
    }
    
    private IEnumerator TextInfoInput(string _txt_Info)
    {
        txt_Info.text = _txt_Info;
        yield return new WaitForSeconds(2f);
        txt_Info.text = null;
    }
}
