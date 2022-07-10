using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    private TMP_Text objectiveText;

    private void Awake()
    {
        objectiveText = GameObject.Find("/Canvas/ObjectiveSystem/Objective_text").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (objectiveText == null)
            throw new System.Exception("Add an Objective_text to the ObjectiveSystem on the Canvas");

        SetObjective("No objective at the moment");
    }

    public void SetObjective(string pObjective)
    {
        objectiveText.text = pObjective;
    }
}
