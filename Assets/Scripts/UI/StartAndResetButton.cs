using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAndResetButton : MonoBehaviour
{
    [Header("Button Variables")]
    public PathfindingController PathfindingController;

    [Header("Visible Variables")]
    [SerializeField] private Button Button;
    [SerializeField] private Text ButtonText;

    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();
        ButtonText = Button.GetComponentInChildren<Text>();

        InitializeStartButton();
    }

    void InitializeStartButton()
    {
        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(PathfindingController.StartPathfinding);
        Button.onClick.AddListener(InitializeResetButton);
        ButtonText.text = "Pathfind";
    }

    void InitializeResetButton()
    {
        Button.onClick.RemoveAllListeners();

        Button.onClick.AddListener(PathfindingController.ResetPathfinding);
        Button.onClick.AddListener(InitializeStartButton);
        ButtonText.text = "Reset";
    }
    
}
