
using UnityEngine;

public class ShieldVisual : MonoBehaviour
{
    private Shield shield;

    [SerializeField] private SpriteRenderer EyeVisual;
    [SerializeField] private SpriteRenderer WingVisual;
    private const string OUTLINE_MATERIAL_TAG = "_OutlineAlpha";
    public void ShowShieldVisual()
    {
        EyeVisual.material.SetFloat(OUTLINE_MATERIAL_TAG, 1f);
        WingVisual.material.SetFloat(OUTLINE_MATERIAL_TAG, 1f);
    }
    public void CloseShieldVisual()
    {
        EyeVisual.material.SetFloat(OUTLINE_MATERIAL_TAG, 0f);
        WingVisual.material.SetFloat(OUTLINE_MATERIAL_TAG, 0f);
    }

}
