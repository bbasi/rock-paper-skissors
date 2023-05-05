using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource ASrcMusic;
    [SerializeField] AudioSource ASrcSFX;
    bool active;

    public void Initialize()
    {
        active = false;
        glbl._.Addressables_.GetMusicAsync((audioClip) =>
        {
            ASrcMusic.clip = audioClip;
            //ASrcMusic.Play();
        });
    }

    public void PlaySFX_Click()
    {
        if (!active) return;
        ASrcSFX.Play();
    }

    public void Toggle()
    {
        if(active)
            ASrcMusic.Stop();
        else
            ASrcMusic.Play();
        active = !active;
    }
}
