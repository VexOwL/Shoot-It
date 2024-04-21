using UnityEngine;

public class OptionsUI : MonoBehaviour
{
    public void Fullscreen_Toggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Options_Exit()
    {
        gameObject.SetActive(false);
    }

    public void Delete_PlayerPrefs()
    {
        //PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Levels_Finished", 4);
    }
}
