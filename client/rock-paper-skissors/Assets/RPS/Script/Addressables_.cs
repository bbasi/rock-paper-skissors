using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Addressables_ : MonoBehaviour
{
    public void GetMusicAsync(Action<AudioClip> cb)
    {
        Addressables.LoadAssetAsync<AudioClip>("gameMusic").Completed += handle =>
        {
            cb(handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null);
        };
    }
}
