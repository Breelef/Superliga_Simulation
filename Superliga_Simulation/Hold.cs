namespace Superliga_Simulation
{
    public class Hold
    {
        private string navn;
        private string forkortelse;
        private int kampeSpillet;
        private int vundet;
        private int uafgjort;
        private int tabt;
        private int målimod;
        private int målfor;
        private int point;

        public Hold(string navn, string forkortelse, int kampeSpillet, int vundet, 
            int uafgjort, int tabt, int målimod, int målfor, int point)
        {
            this.navn = navn;
            this.forkortelse = forkortelse;
            this.kampeSpillet = kampeSpillet;
            this.vundet = vundet;
            this.uafgjort = uafgjort;
            this.tabt = tabt;
            this.målimod = målimod;
            this.målfor = målfor;
            this.point = point;
        }

        public Hold(string navn, string forkortelse)
        {
            this.navn = navn;
            this.forkortelse = forkortelse;
        }

        public Hold(string forkortelse)
        {
            this.forkortelse = forkortelse;
        }

        public Hold()
        {
            
        }
        public string Navn
        {
            get { return navn; }
            set { navn = value; }
        }

        public string Forkortelse
        {
            get { return forkortelse; }
            set { forkortelse = value; }
        }

        public int KampeSpillet
        {
            get { return kampeSpillet; }
            set { kampeSpillet = value; }
        }

        public int Vundet
        {
            get { return vundet; }
            set { vundet = value; }
        }

        public int Uafgjort
        {
            get { return uafgjort; }
            set { uafgjort = value; }
        }

        public int Tabt
        {
            get { return tabt; }
            set { tabt = value; }
        }

        public int Målimod
        {
            get { return målimod; }
            set { målimod = value; }
        }

        public int Målfor
        {
            get { return målfor; }
            set { målfor = value; }
        }

        public int Point
        {
            get { return point; }
            set { point = value; }
        }
        public List<Hold> ReadTeams(List<Hold> holdList)
        {
            using (StreamReader reader = new StreamReader("C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/setup.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string[] data = reader.ReadLine().Split(",");
                    holdList.Add(new Hold(
                        data[0],
                        data[1],
                        int.Parse(data[2]),
                        int.Parse(data[3]),
                        int.Parse(data[4]),
                        int.Parse(data[5]),
                        int.Parse(data[6]),
                        int.Parse(data[7]),
                        int.Parse(data[8])
                    ));
                }
            }
            return holdList;
        }

        public void UpdateTabel(List<Hold> tabel)
        {
            tabel = tabel
                .OrderByDescending(x => x.Point)
                .ThenByDescending(x => x.Målfor - x.Målimod)
                .ThenByDescending(x => x.Målfor)
                .ThenBy(x => x.Målimod)
                .ThenBy(x => x.Navn)
                .ToList();
            using StreamWriter sw = new StreamWriter("C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/setup.csv");
            sw.WriteLine("navn,forkortelse,kampeSpillet,vundet,uafgjort,tabt,målimod,målfor,point");
            foreach (Hold hold in tabel)
            {
                sw.WriteLine($"{hold.Navn},{hold.Forkortelse},{hold.KampeSpillet},{hold.Vundet},{hold.Uafgjort}," +
                             $"{hold.Tabt},{hold.Målimod},{hold.Målfor},{hold.Point}");
            }
        }

        public void printTable()
        {
            List<Hold> holdList = new List<Hold>();
            int placement = 1;
            holdList = ReadTeams(holdList);
            holdList = holdList
                .OrderByDescending(x => x.Point)
                .ThenByDescending(x => x.Målfor - x.Målimod)
                .ThenByDescending(x => x.Målfor)
                .ThenBy(x => x.Målimod)
                .ThenBy(x => x.Navn)
                .ToList();
            foreach (var hold in holdList)
            {
                Console.WriteLine(placement + ". " + hold.ToString());
                placement++;
            }
        }

        public void DivideTable()
        {
            List<Hold> tabel = new List<Hold>();
            tabel = ReadTeams(tabel);
            int half = tabel.Count / 2;
            List<Hold> topTeams = tabel.GetRange(0, half);
            List<Hold> bottomTeams = tabel.GetRange(half, tabel.Count - half);
            using StreamWriter sw =
                new StreamWriter(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/Mesterskabsspil.csv");
            sw.WriteLine("navn,forkortelse,kampeSpillet,vundet,uafgjort,tabt,målimod,målfor,point");
            foreach (var hold in topTeams)
            {
                sw.WriteLine($"{hold.Navn},{hold.Forkortelse},{hold.KampeSpillet},{hold.Vundet},{hold.Uafgjort}," +
                             $"{hold.Tabt},{hold.Målimod},{hold.Målfor},{hold.Point}");
            }
            sw.Close();
            using StreamWriter sw2 =
                new StreamWriter(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/Nedrykningsspil.csv");
            sw2.WriteLine("navn,forkortelse,kampeSpillet,vundet,uafgjort,tabt,målimod,målfor,point");
            foreach (var hold in bottomTeams)
            {
                sw2.WriteLine($"{hold.Navn},{hold.Forkortelse},{hold.KampeSpillet},{hold.Vundet},{hold.Uafgjort}," +
                             $"{hold.Tabt},{hold.Målimod},{hold.Målfor},{hold.Point}");
            }
        }

        public override string ToString()
        {
            return $"{navn} | {forkortelse} | {kampeSpillet} | {vundet} | {uafgjort} | {tabt} | {målfor} | " +
                   $"{målimod} | {målfor-målimod} | {point}";
        }
    }
}

