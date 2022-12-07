using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    public QuestionModel QuestionModelSelected;
    List<QuestionModel> QuestionModels = new List<QuestionModel>();
    List<QuestionModel> QuestionsAnswered = new List<QuestionModel>();

    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        initializeQuestionsBis();
    }

    public void initializeQuestionsBis()
    {
        List<LevelDesign> levelDesigns = LevelManager.instance.GetLevelDesignUntilActualLevel();

        List<QuestionModel> questionModels = new List<QuestionModel>();
        foreach (LevelDesign levelDesign in levelDesigns)
        {
            foreach (Sprite spriteItem in levelDesign.SignSprites)
            {
                string[] answers = spriteItem.name.Split("_");
                string goodAnswer = Regex.Replace(answers[0], @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
                string wrongAnswer = Regex.Replace(answers[1], @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
                questionModels.Add(new QuestionModel(spriteItem, goodAnswer, wrongAnswer));
            }
        }
        QuestionModels = questionModels;
    }

    public QuestionModel GetRandomQuestionModel()
    {
        QuestionModelSelected = QuestionModel.Clone(QuestionModels.OrderBy(e => Random.value).First());
        return QuestionModelSelected;
    }


    public List<QuestionModel> GetQuestionsAnswered()
    {
        return QuestionsAnswered;
    }

    public void UserAnswer(bool IsYesSelected)
    {
        QuestionModelSelected.PlayerAnswer = IsYesSelected;

        if ((IsYesSelected && QuestionModelSelected.IsCorrectAnswerDisplay) || (!IsYesSelected && !QuestionModelSelected.IsCorrectAnswerDisplay))
        {
            ScoreManager.Instance.AddScore();
            UI_Manager.Instance.ShowFeedback(true);
        }
        else
        {
            ScoreManager.Instance.MinusScore();
            LifeManager.Instance.MinusLife();
            UI_Manager.Instance.ShowFeedback(false);
        }
        UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.GetScore());
        QuestionsAnswered.Add(QuestionModelSelected);
        Game_Manager.Instance.CheckGameStatus();
    }
}

public class QuestionModel
{
    public Sprite SpriteItem;
    public string Answer;
    public string WrongAnswer;
    public bool IsCorrectAnswerDisplay;
    public bool PlayerAnswer;

    public QuestionModel(Sprite sprite, string answer, string wrongAnswer)
    {
        SpriteItem = sprite;
        Answer = answer;
        WrongAnswer = wrongAnswer;
    }

    public static QuestionModel Clone(QuestionModel questionRef) 
    {
        return new QuestionModel(questionRef.SpriteItem, questionRef.Answer, questionRef.WrongAnswer);
    }
}