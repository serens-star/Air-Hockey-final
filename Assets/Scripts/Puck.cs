
using UnityEngine;
using UnityEngine.UI;

public class Puck : MonoBehaviour
{
    [SerializeField] private GameObject winPage;
    [SerializeField] private GameObject losePage;
    [SerializeField] private GameObject gamePlayPage;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Portal;
    [SerializeField] private GameObject AI;

    public Text AI_scorTxt;
    public int AI_scorePoints;
    public Text Player_scorTxt;
    public int Playr_scorePoints;
    public bool AI_can_Attack = false;
    public float resetPos_X;
    
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Portal = GameObject.FindGameObjectWithTag("Portal");
    }
    void Update()
    {
        Player_scorTxt.text = Playr_scorePoints.ToString();
        AI_scorTxt.text = AI_scorePoints.ToString();

        if(Playr_scorePoints == 5)//if the user scores 5 points, announce that they've won, end the game, reset the points and give user the option to play again
        {
            Playr_scorePoints = 0;
            winPage.SetActive(true);
            losePage.SetActive(false);
            gamePlayPage.SetActive(false);
            Portal.SetActive(false);
        }
        else if(AI_scorePoints == 5)//if the AI scores 5 points, announce that they've lost, end the game, reset the points and give user the option to play again
        {
            Playr_scorePoints = 0;
            losePage.SetActive(true);
            winPage.SetActive(false);
            gamePlayPage.SetActive(false);
            Portal.SetActive(false);
        }


    }

    public void resetScore()
    {
        AI_scorePoints = 0;
        Playr_scorePoints = 0;
        Portal.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AI_field"))//should the puck enter the AI's field, give the AI permission to go aftr the puck
        {
            AI_can_Attack = true;
            print("AI");
        }

        if (collision.gameObject.CompareTag("AI_GoalPost"))
        {
            AI_scorePoints++;
            transform.position = new Vector2(resetPos_X, 0f);//reset the position of the puck when we score
            rb.velocity = new Vector2(0, 0);
        }
        else if (collision.gameObject.CompareTag("Player_GoalPost"))
        {
            Playr_scorePoints++;
            transform.position = new Vector2(resetPos_X, 0f);
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AI_field"))//when the puck leaves th AI's field, the AI is no longr allowed to áttack
        {
            AI_can_Attack = false;
            print("left");
        }
    }

 



}
