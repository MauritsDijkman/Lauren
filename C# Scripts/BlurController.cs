using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class BlurController : MonoBehaviour
{
    private DepthOfField dofComponent;

    private void Start()
    {
        Volume volume = gameObject.GetComponent<Volume>();
        DepthOfField tmp;

        if (volume.profile.TryGet<DepthOfField>(out tmp))
            dofComponent = tmp;
    }

    public void TurnBlurOn()
    {
        // Sets the focus distance to near
        dofComponent.focusDistance.value = 0.1f;
    }

    public void TurnBlurOff()
    {
        // Sets the focus distance to far
        dofComponent.focusDistance.value = 30f;
    }
}
