using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_Switch : MonoBehaviour
{

    [SerializeField]
    GameObject _switchCover;

    Open_Generator_Switch_Cover _coverScript;

    Animator _switchAnim;

    bool _flipped = false;

    [SerializeField] AudioSource audioSourceSwitch;
    [SerializeField] AudioSource audioSourceGenStart;
    [SerializeField] AudioSource audioSourceGenRun;
    // Start is called before the first frame update
    void Start()
    {
        _coverScript = _switchCover.GetComponent<Open_Generator_Switch_Cover>();
        _switchAnim = GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        if(_coverScript.IsOpen() && Input.GetMouseButtonUp(0) && !_flipped)
        {
            _flipped = true;
            Debug.Log("Generator switch flipped");
            _switchAnim.SetTrigger("Flip");

            //Play generator audio
            audioSourceSwitch.Play();           
            audioSourceGenStart.PlayDelayed(1f);
            AudioClip clip = audioSourceGenStart.clip;
            float delay = clip.length;
            audioSourceGenRun.PlayDelayed(delay);
        }
    }

    public bool IsFlipped()
    {
        return _flipped;
    }
}
