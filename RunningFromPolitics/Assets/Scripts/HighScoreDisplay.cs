using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    ScoreManager ScoreMan;

    [SerializeField] Text Highscore;
    // Start is called before the first frame update
    void Start()
    {
        ScoreMan = ScoreManager.Instance;
        Highscore.text = ScoreMan.MaxScore.ToString();
        print(ScoreMan.MaxScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
