using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    public static void LoadScene(string sceneName)
    {
        //if (sceneName.Equals("Main Menu"))
        //{
        //    Game.players[Game.GetHumanPlayer()].Avatar.Health = 0;
        //}
        SceneManager.LoadScene(sceneName);
    }
}
