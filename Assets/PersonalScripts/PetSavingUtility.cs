using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("AnimalCollection")]
public class AnimalContainer


{
    [XmlArray("Animals"), XmlArrayItem("Animal")]
    public List<Animal> animals = new List<Animal>();

    Animal kid = new Animal("kid");
    Animal mom = new Animal("mom");
    Animal Zap = new Animal("Zappy");

    public AnimalContainer()
    {
        //animals.Add(kid);
        //animals.Add(mom);
        //animals.Add(Zap);
    }
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(AnimalContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static AnimalContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(AnimalContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as AnimalContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static AnimalContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(AnimalContainer));
        return serializer.Deserialize(new StringReader(text)) as AnimalContainer;
    }
}

 
 public class Animal
 { 
     // Note: all variables must be public
     // Identify animals by species, but nicknames are just cool
 	[XmlAttribute("name")]
    public string Name = "Nickname";

    public PlayableCharacters species;

 	

    // stats
     public LocationData _playerLoci;
     public StatusInformation _playerStats;
     public OutfitAccessoryData _playerFit;

     
     public struct LocationData
     {
         public float xpos;
         public float ypos;
         public float zpos;
     }

     public struct StatusInformation
     {
         public int Hunger;
         public int Thirst;
         public int Happiness;
         public int Fatigue;
         public int BladderCapacity;
         public int Boredom;
         public int Health;
     }

     public struct OutfitAccessoryData
     {
         public int OutFitIndex;
         public int EyeIndex;
     }

     // needs default constructor for w.e. reason
     public Animal()
    {}
     public Animal(string name)
     {
         species = PlayableCharacters.Cat;
         Name = name;
         _playerLoci.xpos = 4;
         _playerLoci.ypos = 12;
         _playerLoci.zpos = 87;
     }
 }