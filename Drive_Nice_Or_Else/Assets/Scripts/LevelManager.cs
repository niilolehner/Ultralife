using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    Dictionary<int, LevelDesign> Levels = new Dictionary<int, LevelDesign>();
    public int LevelId = 0;
    public Dictionary<string, GameObject> GamepPlayGameObject = new Dictionary<string, GameObject>();


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

    public Dictionary<int, LevelDesign> GetLevels()
    {
        return Levels;
    }

    public bool IsLevelDeath() { 
        return LevelId > Levels.Count-2;
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

    public List<string> GetGamePlayAllowedUntilActualLevel() 
    {
        List<string> gamePlayName = new List<string>();
        for (int i = 0; i <= LevelId; i++)
        {
            foreach (Texture2D sprite in Levels[i].GamePlaySprites) 
            {
                if (sprite.name != "BonusMalus" && sprite.name != "Question") {
                    gamePlayName.Add(sprite.name);
                }
            }
        }
        return gamePlayName;
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
        Levels.Add(levelId + 1 , new LevelDesign(new List<Sprite>(), new List<Texture2D>()));

        Levels[0].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/BonusMalus"));
        Levels[0].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Question"));
        Levels[1].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Pedestrian"));
        Levels[2].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Traficlight"));
        Levels[Levels.Count-1].GamePlaySprites.Add(Resources.Load<Texture2D>("GamePlayInstruction/Death"));

        GamepPlayGameObject.Add("Pedestrian", Resources.Load<GameObject>("Prefabs/Pedestrian"));
        GamepPlayGameObject.Add("Traficlight", Resources.Load<GameObject>("Prefabs/Traficlight"));
    }

    public string GetNameSignSprite(Sprite spriteItem) 
    {
        return name = Regex.Replace(spriteItem.name.Split("_")[0], @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
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