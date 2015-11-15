using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("GameCollection")]
public class GameContainer
{
    [XmlArray("Games"), XmlArrayItem("GameSave")]
    public List<GameSave> gameSaves = new List<GameSave>();
    

    GameSave kid = new GameSave("kid", 14, 33, 4);
    GameSave mom = new GameSave("mom", 32, 33, 4);
    GameSave dad = new GameSave("FATHER", 53, 33, 4);

    public GameContainer()
    {

    }
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GameContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static GameContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GameContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as GameContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static GameContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(GameContainer));
        return serializer.Deserialize(new StringReader(text)) as GameContainer;
    }
}


public class GameSave
{
    // Note: all variables must be public
    // Identifier for User Saved Game Data
    [XmlAttribute("UserName")]
    public string Name = "BillyBob";

    // Game information
    public int VolumeLevel;
    public int MusicLevel;
    public int CoinAmount;
    

    // needs default constructor for w.e. reason
    public GameSave()
    { }
    public GameSave(string name, int vollvl, int musiclvl, int coinAmt)
    {
        Name = name;
        VolumeLevel = vollvl;
        MusicLevel = musiclvl;
        CoinAmount = coinAmt;
    }
}
