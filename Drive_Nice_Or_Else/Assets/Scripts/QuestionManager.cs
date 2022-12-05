using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;
    public Sprite[] sprites;
    List<Question> Questions = new List<Question>();
    List<Question> QuestionsAnswered = new List<Question>();
    Question QuestionSelected;


    private void Awake()
    {
        Instance = this;
        initializeQuestions();
        //test
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void initializeQuestions()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                int randomSpriteIndex = 0;
                do
                {
                    randomSpriteIndex = Random.Range(0, sprites.Length);
                } while (randomSpriteIndex == i);

                Questions.Add(new Question(sprites[randomSpriteIndex].name.Replace("_", " "), false, sprites[i]));
            }
            else
            {
                Questions.Add(new Question(sprites[i].name.Replace("_", " "), true, sprites[i]));
            }
        }   
        /*
        Questions.Add(new Question("the penalties for driving over the speed limit could be a fines only", false));
        Questions.Add(new Question("In wet weather when it becomes hard for you to see, you should: Flash your headlights to warn other drivers.", false));
        Questions.Add(new Question("Bicycle riders:  Must obey the road rules.", true));
        Questions.Add(new Question("When driving near pedestrian crossings you should always Move into the left lane.", false));
        Questions.Add(new Question("When driving near pedestrian crossings you should always Move into the left lane.", false));
        Questions.Add(new Question("If you see a sign indicating road repairs are going on, you should:  Stop immediately and wait for instructions.", false));
        Questions.Add(new Question("If you see a sign indicating road repairs are going on, you should:. Maintain the same speed.", false));
        Questions.Add(new Question("At night you should : Leave a longer gap behind the vehicle in front.", true));
        Questions.Add(new Question("If you are caught cheating on the knowledge test: You will not be allowed to take another test for 6 weeks", true));
        Questions.Add(new Question("When there are three lanes on a freeway: The right lane is reserved for overtaking.", true));
        Questions.Add(new Question("If you see a sign indicating road repairs are going on, you should: Slow down and watch for traffic controllers and instructions.", true));*/
    }

    public void reInitializeQuestionManager() 
    {
        Questions = new List<Question>();
        QuestionsAnswered = new List<Question>();
        initializeQuestions();
    }

    public int questionsListCount() { 
        return Questions.Count;
    }
        
    public Question GetRandomQuestion() 
    {
        if (Questions.Count > 0) {
            Question question = Questions.OrderBy(e => Random.value).First();
            Questions.Remove(question);
            QuestionSelected = question;
            return QuestionSelected;
        }
        return null;
    }

    public bool IsPlayerAnswerCorrect(bool playerAnswer) 
    {
        QuestionSelected.playerAnswer = playerAnswer;
        QuestionsAnswered.Add(QuestionSelected);
        return playerAnswer == QuestionSelected.answer;
    }
}

public class Question 
{
    public string question;
    public Sprite sprite;
    public bool answer;
    public bool playerAnswer;

    public Question(string question, bool answer, Sprite sprite)
    {
        this.question = question;
        this.answer = answer;
        this.sprite = sprite;
    }
}