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

    List<Question> TrafficSignQuestions = new List<Question>();
    Question TrafficQuestionSelected;



    private void Awake()
    {
        Instance = this;
        initializeQuestions();
        initializeTrafficSignQuestions();
        //test
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void initializeTrafficSignQuestions()
    {
        TrafficSignQuestions.Add(new Question("BusLane(Clone)", "Bus Stop", false));
        TrafficSignQuestions.Add(new Question("BusStop(Clone)", "Bus lane", false));
        TrafficSignQuestions.Add(new Question("DirectionToBeFollowed(Clone)", "Direction to be followed", true));
        TrafficSignQuestions.Add(new Question("FallenRocks(Clone)", "Fallen rocks", true));
        TrafficSignQuestions.Add(new Question("GiveWay(Clone)", "You have to dodge cross traffic", true));
        TrafficSignQuestions.Add(new Question("GiveWayForCyckes(Clone)", "Give way for cycles", true));
        TrafficSignQuestions.Add(new Question("LevelCrossingWithGates(Clone)", "Level-Crossing with gates", true));
        TrafficSignQuestions.Add(new Question("LightSignal(Clone)", "Light signal", true));
        TrafficSignQuestions.Add(new Question("MinimumSpeed(Clone)", "Max speed limit", false));
        TrafficSignQuestions.Add(new Question("Motorway(Clone)", "Motorway", true));
        TrafficSignQuestions.Add(new Question("NoEntry(Clone)", "There is crossing roads", false));
        TrafficSignQuestions.Add(new Question("NoLeftTurn(Clone)", "No left turn", true));
        TrafficSignQuestions.Add(new Question("NoParking(Clone)", "No standing", false));
        TrafficSignQuestions.Add(new Question("NoPowerDrivenVehicles(Clone)", "Not allowed use car or motorcycle", false));
        TrafficSignQuestions.Add(new Question("NoStandingOrParking(Clone)", "No parking", false));
        TrafficSignQuestions.Add(new Question("OneWayRoad(Clone)", "One way road", true));
        TrafficSignQuestions.Add(new Question("Pedestrian(Clone)", "Pedestrian", true));
        TrafficSignQuestions.Add(new Question("PedestrianPath(Clone)", "Pedestrian path", true));
        TrafficSignQuestions.Add(new Question("PriorityRoad(Clone)", "Priority road", true));
        TrafficSignQuestions.Add(new Question("ResidentialZone(Clone)", "People zone", false));
        TrafficSignQuestions.Add(new Question("RoadNumberPrimaryRoad(Clone)", "Secondary road sign", false));
        TrafficSignQuestions.Add(new Question("RoadNumberSecondaryRoad(Clone)", "Primary road sign", false));
        TrafficSignQuestions.Add(new Question("RoadWork(Clone)", "There is trash in the road", false));
        TrafficSignQuestions.Add(new Question("SingAppliesMon-Fri(Clone)", "Sign applies parking at Monday to Friday", true));
        TrafficSignQuestions.Add(new Question("SpeedLimit(Clone)", "Minimum speed limit", false));
        TrafficSignQuestions.Add(new Question("SpeedLimitZone(Clone)", "Speed limit zone", true));
        TrafficSignQuestions.Add(new Question("Stop(Clone)", "You can continue without stopping", false));
    }

    public Question GetTrafficSignQuestion(string questionId)
    {
        for (int i = 0; i < TrafficSignQuestions.Count; i++)
        {
            bool isExist = TrafficSignQuestions[i].id.Equals(questionId);

            if (isExist)
            {
                TrafficQuestionSelected = TrafficSignQuestions[i];
                break;
            }
            else
            {
                TrafficQuestionSelected = null;
            }
        }
        return TrafficQuestionSelected;
    }

    public bool IsPlayerAnswerTrafficQuestionCorrect(bool playerAnswer)
    {
        if (TrafficQuestionSelected == null)
        {
            return false;
        }
        else
        {
            TrafficQuestionSelected.playerAnswer = playerAnswer;
            //QuestionsAnswered.Add(TrafficQuestionSelected);
            return playerAnswer == TrafficQuestionSelected.answer;
        }
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

    public List<Question> GetQuestionsAnswered() 
    { 
        return QuestionsAnswered;
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
        if (QuestionSelected == null)
        {
            return false;
        }
        else
        {
            QuestionSelected.playerAnswer = playerAnswer;
            QuestionsAnswered.Add(QuestionSelected);
            return playerAnswer == QuestionSelected.answer;
        }
    }
}

public class Question 
{
    public string question;
    public Sprite sprite;
    public bool answer;
    public bool playerAnswer;
    public string id;

    public Question(string question, bool answer, Sprite sprite)
    {
        this.question = question;
        this.answer = answer;
        this.sprite = sprite;
    }

    public Question(string id, string question, bool answer)
    {
        this.question = question;
        this.answer = answer;
        this.id = id;
    }
}