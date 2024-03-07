using UnityEngine;

public class DoorSoundCollection : MonoBehaviour
{
    public static DoorSoundCollection instance;
    
    [field: SerializeField] public AudioClip DoorOpen { get; private set; } 
    [field: SerializeField] public AudioClip DoorClose { get; private set; } 
    [field: SerializeField] public AudioClip DoorLocked { get; private set; } 

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
