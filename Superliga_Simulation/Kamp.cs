using Superliga_Simulation;
namespace Superliga_Simulation
{
    public class Kamp
    {
        private string kampNavn;
        private Hold hjemmehold;
        private Hold udehold;
        private int målhjemme;
        private int målude;

        public Kamp(string kampNavn, Hold hjemmehold, Hold udehold, int målhjemme, int målude)
        {
            this.kampNavn = kampNavn;
            this.hjemmehold = hjemmehold;
            this.udehold = udehold;
            this.målhjemme = målhjemme;
            this.målude = målude;
        }

        public Kamp(Hold hjemmehold, Hold udehold)
        {
            this.hjemmehold = hjemmehold;
            this.udehold = udehold;
        }

        public Kamp(string kampNavn)
        {
            this.kampNavn = kampNavn;
        }

        public Kamp()
        {
            
        }

        public string KampNavn
        {
            get { return kampNavn; }
            set { kampNavn = value; }
        }
        public Hold Hjemmehold
        {
            get { return hjemmehold; }
            set { hjemmehold = value; }
        }
        public Hold Udehold
        {
            get { return udehold; }
            set { udehold = value; }
        }
        public int Målhjemme
        {
            get { return målhjemme; }
            set { målhjemme = value; }
        }
        public int Målude
        {
            get { return målude; }
            set { målude = value; }
        }

        public List<Kamp> readGames()
        {
            List<Kamp> gamesMade = new List<Kamp>();
            String[] files;
            try
            {
                files = Directory.GetFiles(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/runder");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            for (int i = 1; i < files.Length+1; i++)
            {
                
                using StreamReader reader = new StreamReader("C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/runder/runde-" +i +".csv");
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string[] data = reader.ReadLine().Split();
                    string makeMatch = data[0];
                    gamesMade.Add(new Kamp(makeMatch));
                }
            }
            return gamesMade;
        }
        
        public override string ToString()
        {
            return $"{hjemmehold} ({udehold})";
        }
    }
}

