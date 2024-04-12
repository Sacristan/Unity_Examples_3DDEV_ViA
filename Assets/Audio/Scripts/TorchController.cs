using UnityEngine;

public class TorchController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] flashLightSounds;

    [SerializeField]
    private Light flashLight;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashLight();
        }
    }


    private void ToggleFlashLight()
    {
        int audioClipIndex = Random.Range(0, flashLightSounds.Length);

        AudioClip flashLightSFX = flashLightSounds[audioClipIndex];

        if (flashLightSFX != null)
        {
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(flashLightSFX);
        }

        flashLight.enabled = !flashLight.enabled;
    }

}
