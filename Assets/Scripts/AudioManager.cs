// AudioManager.cs
// February 2nd, 2020
// Completely revised and documented by Matheus Vilano

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple audio player for physics objects (to be used in the Game Audio classes).
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Single instance.
    /// </summary>
    public static AudioManager instance;
    /// <summary>
    /// Wwise SoundBank name.
    /// </summary>
    public string soundBank = default;

    public string wwiseSwitchGroup = default;

    /// <summary>
    /// Defines the medium strength threshold.
    /// </summary>
    [SerializeField]
    private float fMediumThreshold = 5.0f;
    /// <summary>
    /// Defines the heavy strength threshold.
    /// </summary>
    [SerializeField]
    private float fHeavyThreshold = 15.0f;
    /// <summary>
    /// User interface text. Logs current audio events on the screen.
    /// </summary>
    [SerializeField]
    private Text onScreenText = default;

    /// <summary>
    /// Instantiates the singleton and loads the Wwise SoundBank.
    /// </summary>
    private void Awake()
    {
        // Load SoundBank
        AkBankManager.LoadBank(soundBank, false, false);

        // Instantiate if null
        if (instance == null)
        {
            instance = this;
        }
        // Destroy duplicate if not null
        else
        {
            Debug.Log("Destroying duplicate instance.");
            Destroy(this);
        }
    }

    /// <summary>
    /// Sets the appropriate switch and plays sound by posting a Wwise Event.
    /// </summary>
    /// <param name="ObjectName"></param>
    /// <param name="intensity"></param>
    /// <param name="gameObject"></param>
    public void PlayPhysicsObjectImpact(string ObjectName, float intensity, GameObject gameObject)
    {
        // Null check and intensity check
        if (gameObject != null && intensity >= 0f)
        {
            // Local variables
            string SwitchValue = GetIntensityRange(intensity);
           

            // Set Switch and Post Event if Switch Group is valid
            if (wwiseSwitchGroup != "" && wwiseSwitchGroup != null)
            {
                AkSoundEngine.SetSwitch(wwiseSwitchGroup, SwitchValue, this.gameObject);
                
                AkSoundEngine.PostEvent($"{ObjectName}", this.gameObject);
            }

            // Change on-screen text
            onScreenText.text = $"Posted Wwise Event: {ObjectName} at {SwitchValue} switch intensity. \nHit Magnitude of: {intensity}.";
        }
    }

    /// <summary>
    /// Returns intensity as a string: "Light", "Medium", or "Hard"
    /// </summary>
    /// <param name="intensity"></param>
    /// <returns></returns>
    private string GetIntensityRange(float intensity)
    {
        // Log intensity to the Console
        Debug.Log(intensity);

        // Choose intensity range based on float value input by user ("intensity")
        if (intensity < fMediumThreshold)
        {
            return "light"; 
        }
        else if ((intensity >= fMediumThreshold) && (intensity < fHeavyThreshold))
        {
            return "medium";
        }
        else if (intensity >= fHeavyThreshold)
        {
            return "hard";
        }
        else
        {
            return "light"; // Default value, in case none of the conditions above evaluate to true
        }
    }

    /// <summary>
    /// Identifies a physics object as "PhysObj_1" or "PhysObj_2", depending on the game object name.
    /// </summary>
    /// <param name="ObjectName"></param>
    /// <returns></returns>
    private string GetSwitchGroup(string ObjectName)
    {
        // Use the String class built-in function "EndsWith" as the algorithm to identify the physics object.
        if (ObjectName.EndsWith("1"))
        {
            return "PhysObj_1";
        }
        else if (ObjectName.EndsWith("2"))
        {
            return "PhysObj_2";
        }
        else
        {
            Debug.LogError("Please check your object naming. You have named something incorrectly");
            return ""; // Return empty string as an indicator that this function failed
        }
    }
}
