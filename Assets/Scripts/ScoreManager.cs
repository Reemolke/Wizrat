
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager scoreManager;
    public static int totalScore;
    int score = 0;
    public Text scoreText;
    void Start(){
        scoreManager = this;
    }
    public void raiseScore(int points){
        score += points;
        scoreText.text = score.ToString();
    }
    public void sumScore(){
        totalScore +=score;

    }
}
