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

    public void ClearContent() {
        foreach (Transform child in ScrowPanelContent.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void HistoryButtonOnClick()
    {
        historyLibraryPanel.gameObject.SetActive(true);
        ClearContent();
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
        historyLibraryPanel.gameObject.SetActive(true);
        ClearContent();
        HistoryLibraryText.text = "LIBRARY";
        List<LevelDesign> leveldesigns = LevelManager.instance.GetLevelDesignUntilActualLevel();

        RectTransform rect = PrefabRowHistory.GetComponent<RectTransform>();
        float offset = 20f;
        float width = rect.sizeDelta.x;
        float height = rect.sizeDelta.y;

        RectTransform ScrowPanelContentRec = ScrowPanelContent.GetComponent<RectTransform>();
        ScrowPanelContentRec.sizeDelta = new Vector2(ScrowPanelContentRec.sizeDelta.x, (leveldesigns.Count * height * 4));

        Vector3 positionRow = new Vector3(ScrowPanelContent.transform.position.x + width / 2 + offset, ScrowPanelContent.transform.position.y - height / 2 - offset, 0);
        GameObject row = InstantiateRowLibrary(positionRow, leveldesigns[0].SignSprites[0]);

        foreach (LevelDesign lvl in leveldesigns)
        {
            foreach (Sprite sprite in lvl.SignSprites )
            {
                Vector3 positionNextRow = new Vector3(row.transform.position.x, row.transform.position.y - height - offset, 0);
                GameObject nextRow = InstantiateRowLibrary(positionNextRow, sprite);
                row = nextRow; 
            }
        }


        for (int i = 0; i < leveldesigns.Count; i++)
        {
            List<Sprite> sprites = leveldesigns[i].SignSprites;
            for (int j = 0; j < sprites.Count; j++)
            {
                if (i !=  0 && j!=0 )  {
                    Vector3 positionNextRow = new Vector3(row.transform.position.x, row.transform.position.y - height - offset, 0);
                    GameObject nextRow = InstantiateRowLibrary(positionNextRow, sprites[j]);
                    row = nextRow;
                }  
            }
        }
    }

    public GameObject InstantiateRowLibrary(Vector3 position, Sprite sprite)
    {
        GameObject row = Instantiate(PrefabRowHistory, position, Quaternion.identity);
        FillRowLibrary(row, sprite);
        row.transform.parent = ScrowPanelContent.transform;
        return row;
    }

    public void FillRowLibrary(GameObject row, Sprite sprite)
    {
        row.GetComponent<RowQuestion>().image.sprite = sprite;
        row.GetComponent<RowQuestion>().question.text = LevelManager.instance.GetNameSignSprite(sprite);
    }

    public void HistoryExitButtonOnClick()
    {
        historyLibraryPanel.gameObject.SetActive(false);
    }

}
