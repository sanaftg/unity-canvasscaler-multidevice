using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiDeviceCanvasScaler : CanvasScaler
{
    private float currentRatio = 16f/9f;
    [SerializeField] private bool isHard;

    // Update is called once per frame
    protected  void Update()
    {
        float ratio = GetCurrentRatio();
        if(currentRatio != ratio){
            currentRatio = ratio;
            UpdateScale();
		}
    }

    private float GetCurrentRatio(){
        float ratio = 1f * Screen.width / Screen.height;
#if UNITY_EDITOR
        var sizes = UnityEditor.UnityStats.screenRes.Split('x');
        var w = float.Parse(sizes[0]);
        var h = float.Parse(sizes[1]);
        ratio = w / h;
#endif
        return ratio;
    }
    private void UpdateScale(){
        // ~4:3
        if(currentRatio < 1.3f){
           m_MatchWidthOrHeight = 0f;
            return;
		}
        // 4:3 ~ 16:11
        if(currentRatio < 1.45){
            m_MatchWidthOrHeight = 0.3f;
            return;
		}
        // 16:11 ~16:9
        if(currentRatio < 1.76){
            m_MatchWidthOrHeight = 0.5f;
            return;
		}
        // 16:9より横長
        m_MatchWidthOrHeight = 1f;
        Handle();
    }
}
