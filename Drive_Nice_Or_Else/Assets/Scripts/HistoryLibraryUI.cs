using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoryLibraryUI : MonoBehaviour
{
    public static HistoryLibraryUI Instance;

    [Header("HistoryLibraryPanel")]
    [SerializeField]
    private GameObject historyLibraryPanel;
    [SerializeField]
    private TextMeshProUGUI HistoryLibraryText;
    [SerializeField]
    private GameObject ScrowPanelContent;
    [SerializeField]
    private GameObject PrefabRowHistory;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HistoryButtonOnClick()
    {
        HistoryLibraryText.text = "HISTORY";

        List<QuestionModel> questions = QuestionManager.Instance.GetQuestionsAnswered();

        if (questions.Count > 0)
        {
            RectTransform rect = PrefabRowHistory.GetComponent<RectTransform>();
            float offset = 20f;
            float width = rect.sizeDelta.x;
            float height = rect.sizeDelta.y;

            RectTransform ScrowPanelContentRec = ScrowPanelContent.GetComponent<RectTransform>();
            ScrowPanelContentRec.sizeDelta = new Vector2(ScrowPanelContentRec.sizeDelta.x, (questions.Count * height));

            Vector3 positionRow = new Vector3(ScrowPanelContent.transform.position.x + width / 2 + offset, ScrowPanelContent.transform.position.y - height / 2 - offset, 0);

            GameObject row = InstantiateRowHistory(positionRow, questions[0]);

            for (int i = 1; i < questions.Count; i++)
            {
                Vector3 positionNextRow = new Vector3(row.transform.position.x, row.transform.position.y - height - offset, 0);
                GameObject nextRow = InstantiateRowHistory(positionNextRow, questions[i]);
                row = nextRow;
            }
        }
    }

    public GameObject InstantiateRowHistory(Vector3 position, QuestionModel question)
    {
        GameObject row = Instantiate(PrefabRowHistory, position, Quaternion.identity);
        FillRowHistory(row, question);
        row.transform.parent = ScrowPanelContent.transform;
        return row;
    }

    public void FillRowHistory(GameObject row, QuestionModel question)
    {
        row.GetComponent<RowQuestion>().image.sprite = question.SpriteItem;
        row.GetComponent<RowQuestion>().question.text = question.IsCorrectAnswerDisplay ? question.Answer + " ?" : question.WrongAnswer + " ?";
        row.GetComponent<RowQuestion>().answer.text = question.PlayerAnswer ? "Yes" : "No";

        row.GetComponent<RowQuestion>().answerCorrection.text = (question.IsCorrectAnswerDisplay == question.PlayerAnswer) ? " - Correct" : " - False";
        row.GetComponent<RowQuestion>().answerCorrection.color = (question.IsCorrectAnswerDisplay == question.PlayerAnswer) ? Color.green : Color.red;
    }

    public void LibraryButtonOnClick()
    {
        HistoryLibraryText.text = "lIBRARY";

        List<QuestionModel> questions = QuestionManager.Instance.GetQuestionsAnswered();

        if (questions.Count > 0)
        {
            RectTransform rect = PrefabRowHistory.GetComponent<RectTransform>();
            float offset = 20f;
            float width = rect.sizeDelta.x;
            float height = rect.sizeDelta.y;

            RectTransform ScrowPanelContentRec = ScrowPanelContent.GetComponent<RectTransform>();
            ScrowPanelContentRec.sizeDelta = new Vector2(ScrowPanelContentRec.sizeDelta.x, (questions.Count * height));

            Vector3 positionRow = new Vector3(ScrowPanelContent.transform.position.x + width / 2 + offset, ScrowPanelContent.transform.position.y - height / 2 - offset, 0);

            GameObject row = InstantiateRowHistory(positionRow, questions[0]);

            for (int i = 1; i < questions.Count; i++)
            {
                Vector3 positionNextRow = new Vector3(row.transform.position.x, row.transform.position.y - height - offset, 0);
                GameObject nextRow = InstantiateRowHistory(positionNextRow, questions[i]);
                row = nextRow;
            }
        }
    }

    
}
