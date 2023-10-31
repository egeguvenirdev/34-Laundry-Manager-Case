using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class VibrationManager : MonoSingleton<VibrationManager>
{
    public void PlayVibration(HapticPatterns.PresetType vibrationType)
    {
        HapticPatterns.PlayPreset(vibrationType);
    }

    public void SelectionVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
    }

    public void SuccessVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
    }

    public void WarningVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
    }

    public void FailureVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
    }

    public void RigidVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
    }

    public void SoftVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
    }

    public void LightVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
    }

    public void MediumVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
    }

    public void HeavyVibration()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
    }
}
