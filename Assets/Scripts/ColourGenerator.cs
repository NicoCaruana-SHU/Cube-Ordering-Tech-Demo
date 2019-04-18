using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assigns a random Colour to the attached Material, and optional ParticleSystem.
/// </summary>
public class ColourGenerator : MonoBehaviour
{
    /// <summary>
    /// Identification value of the colour space. Can be used to sort.
    /// </summary>
    public float Hue { get { return hue_; } }
    private float hue_;

    void Awake()
    {
        Renderer renderer_ = GetComponent<Renderer>();
        ParticleSystem ps_ = GetComponent<ParticleSystem>();

        Color rgbColour_ = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); // Generate a new random colour.
        float saturation;   // Temp variable - used only to run the RGBtoHSV function to get hue value
        float value;        // Temp variable - used only to run the RGBtoHSV function to get hue value

        renderer_.material.color = rgbColour_; // Set the material to be the random colour.
        Color.RGBToHSV(rgbColour_, out hue_, out saturation, out value); // After testing, sorting using just the hue value provides the most appealing visual colour banding.

        if (ps_ != null)
        {
            var main = ps_.main;
            main.startColor = rgbColour_; // If theres a particle system attached, set the particle colour to match the material colour.
        }
    }

}
