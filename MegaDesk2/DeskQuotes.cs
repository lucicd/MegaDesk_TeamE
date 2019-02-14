using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MegaDesk
{

    public class DeskQuotes
    {
        private List<Desk> quotes;
        private const string fileName = @"quotes.txt";

        public DeskQuotes()
        {
            quotes = new List<Desk>();
            LoadFromFile();
        }

        public List<Desk> GetAll()
        {
            return quotes;
        }

        public List<Desk> GetFiltered(string materialDescr)
        {
            if (materialDescr.Equals("All"))
            {
                return quotes;
            }
            else
            {
                return quotes.FindAll
                (
                    delegate (Desk quote)
                    {
                        return quote.SurfaceMaterial.ToString() == materialDescr;
                    }
                );
            }
        }

        public void Add(Desk quote)
        {
            quotes.Add(quote);
            string[] lines = quotes.ConvertAll(x => x.MakeFileRecord()).ToArray();
            try
            {
                File.WriteAllLines(fileName, lines);
                AddJson();
            }
            catch (System.Exception e)
            {
                throw new System.Exception("Can't write to " + fileName + ". " + e.Message);
            }
            
        }

        public void AddJson()
        {
            using (StreamWriter file = File.CreateText(@"quotes.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, quotes);
            }
        }

        private void LoadFromFile()
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    quotes.Add(new Desk(line));
                }
            }
        }
    }
}
