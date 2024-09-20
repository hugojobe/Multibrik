using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorCorrectionManager : MonoBehaviour
{

    [SerializeField]
    private Volume imageCorrectionVolume;
    private VolumeProfile imageCorrectionProfile;

    private void Start(){
        imageCorrectionProfile = imageCorrectionVolume.profile;
    }

    public void SetBrightness(float value){
        imageCorrectionProfile.TryGet(out LiftGammaGain gammaGain);
        gammaGain.lift.value = new Vector4(gammaGain.lift.value.x, gammaGain.lift.value.y, gammaGain.lift.value.z, value);
    }
    public void SetContrast(float value){
        imageCorrectionProfile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.contrast.value = value;
    }
    public void SetSaturation(float value){
        imageCorrectionProfile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.saturation.value = value;
    } 
    public void SetGamma(float value){
        imageCorrectionProfile.TryGet(out LiftGammaGain gammaGain);
        gammaGain.gamma.value = new Vector4(gammaGain.gamma.value.x, gammaGain.gamma.value.y, gammaGain.gamma.value.z, value);
    }
}
