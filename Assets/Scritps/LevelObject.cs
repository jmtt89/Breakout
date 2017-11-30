using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObject : MonoBehaviour {
    private Material _powerBombMatA;
    private Material _powerBombMatB;
    private Material _powerBombMatC;
    private UnityEngine.UI.Text _score;

	// Use this for initialization
	void Start () {
        _powerBombMatA = GameObject.Find("PowerBombA").GetComponent<Renderer>().material;
        _powerBombMatB = GameObject.Find("PowerBombB").GetComponent<Renderer>().material;
        _powerBombMatC = GameObject.Find("PowerBombC").GetComponent<Renderer>().material;
        _score = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
    }
	
	void LateUpdate () {
        var idx = SceneManager.GetActiveScene().buildIndex;
        if (idx != 0)
        {
            UpdatePowerBombMetter();
            UpdateScoreMetter();
            if (LevelManager.numInitialBlocks == 0)
            {

                if (idx > 0 && idx < 3)
                {
                    // Proximo nivel
                    ChangeLevel(idx + 1);
                }
                else if (idx == 3)
                {
                    // Fin del juego a Main
                    ChangeLevel(0);
                }
            }
        }
    }

    public void GoToLevel1()
    {
        LevelManager.Points = 0;
        LevelManager.UsePowerBomb();
        LevelManager.UsePowerBomb();
        LevelManager.UsePowerBomb();
        ChangeLevel("Level1");
    }

    private void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void ChangeLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    private void UpdateScoreMetter()
    {
        _score.text = LevelManager.Points.ToString("0000000");
    }

    private void UpdatePowerBombMetter()
    {
        switch (LevelManager.PowerBomb)
        {
            case 3:
                _powerBombMatA.color = Color.red;
                _powerBombMatB.color = Color.red;
                _powerBombMatC.color = Color.red;
                break;
            case 2:
                _powerBombMatA.color = Color.red;
                _powerBombMatB.color = Color.red;
                _powerBombMatC.color = Color.black;
                break;
            case 1:
                _powerBombMatA.color = Color.red;
                _powerBombMatB.color = Color.black;
                _powerBombMatC.color = Color.black;
                break;
            default:
                _powerBombMatA.color = Color.black;
                _powerBombMatB.color = Color.black;
                _powerBombMatC.color = Color.black;
                break;
        }
    }
}
