using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject powerUp;
    public int totalCollectibles = 10;
    public int Collected { get; private set; }
    public bool Started { get; private set; }
    public bool Powered { get; private set; }
    public bool Won { get; private set; }
    public bool WallBroken { get; private set; }
    public string notice = "Collect all 10 cubes to reveal the power-up.";

    void Awake() { powerUp.SetActive(false); }
    public void StartGame()
    {
        if (Started) return;
        Started = true;
        player.Begin();
    }
    public void Collect()
    {
        if (!Started || Won) return;
        Collected++;
        if (Collected == totalCollectibles)
        {
            powerUp.SetActive(true);
            notice = "All cubes collected! Pick up the pink capsule.";
        }
    }
    public void ActivatePower()
    {
        if (Collected != totalCollectibles || Powered) return;
        Powered = true;
        powerUp.SetActive(false);
        player.PowerUp();
        notice = "Power-up active! Break the brown wall, then jump the gap.";
    }
    public void BreakWall(GameObject wall)
    {
        if (!Powered || Won) return;
        WallBroken = true;
        wall.SetActive(false);
        notice = "Wall broken! Jump from the edge to the blue goal.";
    }
    public void Win()
    {
        if (!Powered || !WallBroken || Won) return;
        Won = true;
        player.gameObject.SetActive(false);
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 310, 155), "Roll a Ball");
        GUI.Label(new Rect(20, 35, 290, 25), "Cubes: " + Collected + " / " + totalCollectibles);
        GUI.Label(new Rect(20, 60, 290, 25), "WASD/arrows: move. Space: jump. R: reset.");
        if (!Started)
        {
            if (GUI.Button(new Rect(20, 95, 100, 35), "Start")) StartGame();
        }
        else if (Won)
        {
            GUI.Label(new Rect(20, 90, 200, 25), "You win!");
            if (GUI.Button(new Rect(20, 120, 100, 30), "Restart")) Restart();
        }
        else if (Powered)
            GUI.Label(new Rect(20, 90, 290, 25), "Power-up active! Break the wall and jump.");
        else if (Collected == totalCollectibles)
            GUI.Label(new Rect(20, 90, 290, 25), "Collect the capsule to power up.");
    }
    public static void Restart() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
