using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadScene("Boss");
    }

    
    public void OpenSettings()
    {
        
    }

    // ฟังก์ชันสำหรับปุ่ม Exit
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}