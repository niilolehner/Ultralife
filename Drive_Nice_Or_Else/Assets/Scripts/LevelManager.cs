using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    Dictionary<int, LevelDesign> Levels = new Dictionary<int, LevelDesign>();
    public int LevelId = 0;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initializelevels();
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public LevelDesign GetLevelDesginlById(int index) 
    {
        return Levels[index];
    }

    public List<LevelDesign> GetLevelDesignUntilActualLevel() 
    {
        List<LevelDesign> levels = new List<LevelDesign>();
        for (int i = 0; i <= LevelId; i++) 
        {
            levels.Add(Levels[i]);
        }
        return levels;
    }

    public LevelDesign GetActualLevelDesign()
    {
        return Levels[LevelId];
    }

    private List<Sprite> fillLevels(List<Sprite> allSpritesSigns, int numberOfSprite) 
    {
        List<Sprite> spriteForLevel = new List<Sprite>();
        for (int i = 0; i < numberOfSprite; i++)
        {
            Sprite sprite = allSpritesSigns.OrderBy(e => Random.value).First();
            spriteForLevel.Add(sprite);
            allSpritesSigns.Remove(sprite);
        }

        return spriteForLevel;
    }

    private void Initializelevels()
    {
        List<Sprite> allSpritesSigns = Resources.LoadAll<Sprite>("Signs").ToList();

        int stopCount = allSpritesSigns.Count % 4;
        int levelId = 0;

        while (allSpritesSigns.Count > stopCount)
        {
            List<Sprite> spriteForLevel = fillLevels(allSpritesSigns, 4);
            Levels.Add(levelId, new LevelDesign(spriteForLevel, new List<Texture2D>()));
            levelId++;
        }

        List<Sprite> spriteForLastLevel = fillLevels(allSpritesSigns, stopCount);
        Levels.Add(levelId, new LevelDesign(spriteForLastLevel, new List<Texture2D>()));

        Levels[0].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/BonusMalus"));
        Levels[0].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Question"));
        Levels[1].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Pedestrian"));
    }
}

public class LevelDesign
{
    public List<Sprite> SignSprites;
    public List<Texture2D> GamePlaySprites;

    public LevelDesign(List<Sprite> SignSprites, List<Texture2D> GamePlaySprites)
    {
        this.SignSprites = SignSprites;
        this.GamePlaySprites = GamePlaySprites;
    }
}