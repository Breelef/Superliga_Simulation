using Superliga_Simulation;
using System.IO;
using System.Text;

namespace Superliga_Simulation
{

    public class Runde
    {
        private List<Kamp> runder = new List<Kamp>();
        Hold hold = new Hold();
        public Runde(List<Kamp> runder)
        {
            this.runder = runder;
        }

        public List<Kamp> Runder
        {
            get { return runder; }
            set { runder = value; }
        }
        public Runde()
        {
            
        }

    public Runde GenerateRound(List<Kamp> gamesPlanned)
    {
    List<Hold> holdListe = new List<Hold>();
    using (StreamReader reader = new StreamReader("C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/teams.csv"))
    {
        reader.ReadLine();
        while (!reader.EndOfStream)
        {
            string[] data = reader.ReadLine().Split(",");
            holdListe.Add(new Hold(data[0], data[1]));
        }
    }

    Runde nyRunde = new Runde();
    Random rnd = new Random();
    int numberOfTeams = holdListe.Count;
    List<Kamp> kampeIRunde = new List<Kamp>();
    List<Hold> shuffledHoldListe = holdListe.OrderBy(x => rnd.Next()).ToList();

    for (int i = 0; i < 12; i++)
    {
        List<int> unavailableTeams = new List<int>(); // Teams that have already played a match in this round
        foreach (Kamp kamp in kampeIRunde.Concat(gamesPlanned))
        {
            string[] hold = kamp.KampNavn.Split("-");
            int hjemmeIndex = shuffledHoldListe.FindIndex(hold[0].Equals);
            int udeIndex = shuffledHoldListe.FindIndex(hold[1].Equals);
            unavailableTeams.Add(hjemmeIndex);
            unavailableTeams.Add(udeIndex);
        }
        unavailableTeams = unavailableTeams.Distinct().ToList(); // Remove duplicates

        int hjemmeHold = -1;
        int udehold = -1;
        while (hjemmeHold == -1 || udehold == -1 || hjemmeHold == udehold ||
               unavailableTeams.Contains(hjemmeHold) || unavailableTeams.Contains(udehold))
        {
            hjemmeHold = i % numberOfTeams;
            udehold = (i + 3) % numberOfTeams;
            i++;

            if (i >= 6 * numberOfTeams)
            {
                throw new Exception("Cannot generate unique schedule");
            }
        }
        Hold hjemme = shuffledHoldListe[hjemmeHold];
        Hold ude = shuffledHoldListe[udehold];
        Kamp nyKamp = new Kamp(hjemme.Forkortelse + "-" + ude.Forkortelse);
        kampeIRunde.Add(nyKamp);
        gamesPlanned.Add(nyKamp); // Add the new game to the played games list
    }
    nyRunde.Runder = kampeIRunde;
    return nyRunde;
}

    public List<Runde> generatePlayOffRounds(List<Kamp> gamesPlannedChampion, List<Kamp> gamesPlannedRelegation)
    {
        List<Hold> tabel = new List<Hold>();
        tabel = hold.ReadTeams(tabel);
        int half = tabel.Count / 2;
        List<Hold> topTeams = tabel.GetRange(0, half);
        List<Hold> bottomTeams = tabel.GetRange(half, tabel.Count - half);
        Runde championRound = new Runde();
        Runde relegationRound = new Runde();
        championRound = GenerateChampionPlayOffs(topTeams, gamesPlannedChampion);
        relegationRound = GenerateRelegationRound(bottomTeams, gamesPlannedRelegation);
        List<Runde> rounds = new List<Runde>() { championRound, relegationRound };
        return rounds;
    }

    public Runde GenerateChampionPlayOffs(List<Hold> topTeams, List<Kamp> gamesPlanned)
    {
        Runde nyRunde = new Runde();
        Random rnd = new Random();
        int numberOfTeams = topTeams.Count;
        List<Kamp> kampeIRunde = new List<Kamp>();
        List<Hold> shuffledHoldListe = topTeams.OrderBy(x => rnd.Next()).ToList();
        for (int i = 0; i < 6; i++)
        {
            List<int> unavailableTeams = new List<int>(); // Teams that have already played a match in this round
            foreach (Kamp kamp in kampeIRunde.Concat(gamesPlanned))
            {
                string[] hold = kamp.KampNavn.Split("-");
                int hjemmeIndex = shuffledHoldListe.FindIndex(hold[0].Equals);
                int udeIndex = shuffledHoldListe.FindIndex(hold[1].Equals);
                unavailableTeams.Add(hjemmeIndex);
                unavailableTeams.Add(udeIndex);
            }
            unavailableTeams = unavailableTeams.Distinct().ToList(); // Remove duplicates

            int hjemmeHold = -1;
            int udehold = -1;
            while (hjemmeHold == -1 || udehold == -1 || hjemmeHold == udehold ||
                   unavailableTeams.Contains(hjemmeHold) || unavailableTeams.Contains(udehold))
            {
                hjemmeHold = i % numberOfTeams;
                udehold = (i + 3) % numberOfTeams;
                i++;

                if (i >= 6 * numberOfTeams)
                {
                    throw new Exception("Cannot generate unique schedule");
                }
            }
            Hold hjemme = shuffledHoldListe[hjemmeHold];
            Hold ude = shuffledHoldListe[udehold];
            Kamp nyKamp = new Kamp(hjemme.Forkortelse + "-" + ude.Forkortelse);
            kampeIRunde.Add(nyKamp);
            gamesPlanned.Add(nyKamp); // Add the new game to the played games list
        }
        nyRunde.Runder = kampeIRunde;
        return nyRunde;
    }

    public Runde GenerateRelegationRound(List<Hold> bottomTeams, List<Kamp> gamesPlanned)
    {
        Runde nyRunde = new Runde();
        Random rnd = new Random();
        int numberOfTeams = bottomTeams.Count;
        List<Kamp> kampeIRunde = new List<Kamp>();
        List<Hold> shuffledHoldListe = bottomTeams.OrderBy(x => rnd.Next()).ToList();
        for (int i = 0; i < 6; i++)
        {
            List<int> unavailableTeams = new List<int>(); // Teams that have already played a match in this round
            foreach (Kamp kamp in kampeIRunde.Concat(gamesPlanned))
            {
                string[] hold = kamp.KampNavn.Split("-");
                int hjemmeIndex = shuffledHoldListe.FindIndex(hold[0].Equals);
                int udeIndex = shuffledHoldListe.FindIndex(hold[1].Equals);
                unavailableTeams.Add(hjemmeIndex);
                unavailableTeams.Add(udeIndex);
            }
            unavailableTeams = unavailableTeams.Distinct().ToList(); // Remove duplicates

            int hjemmeHold = -1;
            int udehold = -1;
            while (hjemmeHold == -1 || udehold == -1 || hjemmeHold == udehold ||
                   unavailableTeams.Contains(hjemmeHold) || unavailableTeams.Contains(udehold))
            {
                hjemmeHold = i % numberOfTeams;
                udehold = (i + 3) % numberOfTeams;
                i++;

                if (i >= 6 * numberOfTeams)
                {
                    throw new Exception("Cannot generate unique schedule");
                }
            }
            Hold hjemme = shuffledHoldListe[hjemmeHold];
            Hold ude = shuffledHoldListe[udehold];
            Kamp nyKamp = new Kamp(hjemme.Forkortelse + "-" + ude.Forkortelse);
            kampeIRunde.Add(nyKamp);
            gamesPlanned.Add(nyKamp); // Add the new game to the played games list
        }
        nyRunde.Runder = kampeIRunde;
        return nyRunde;
    }
    


    public void SaveRound(Runde nyRunde, int counter)
        {
            if (nyRunde == null)
            {
                throw new ArgumentNullException(nameof(nyRunde), "Runde object cannot be null");
            }
            using StreamWriter sw =
                new StreamWriter(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/runder/runde-" +
                    counter +".csv");
            sw.WriteLine("Runde " + counter);
            foreach (Kamp kamp in nyRunde.Runder)
            {
                sw.WriteLine(kamp.KampNavn);
            }
        }

    public void savePlayOffRounds(List<Runde> playOffRound, int counter)
    {
        using StreamWriter sw = new StreamWriter(
            "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/ChampionsPath/runde-" +
            counter + ".csv");
        sw.WriteLine("Runde " + counter + " Chamionspath");
        foreach (var kamp in playOffRound[0].Runder)
        {
            sw.WriteLine(kamp.KampNavn);
        }
        sw.Close();
        using StreamWriter sw2 = new StreamWriter(
            "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/RelegationPath/runde-" +
            counter + ".csv");
        sw2.WriteLine("Runde " + counter + " Chamionspath");
        foreach (var kamp in playOffRound[1].Runder)
        {
            sw2.WriteLine(kamp.KampNavn);
        }
    }
        
        public List<Hold> PlayRound(int roundNumber)
        {
            Random rnd = new Random();
            List<Hold> tabel = new List<Hold>();
            List<Kamp> tempGames = new List<Kamp>();
            tabel = hold.ReadTeams(tabel);
            using StreamReader reader =
                new StreamReader(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/runder/runde-" +
                    roundNumber + ".csv");
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string[] data = reader.ReadLine().Split("-");
                tempGames.Add(new Kamp(new Hold(data[0]), new Hold(data[1])));
                
            }
            reader.Close();
            using StreamWriter sw = new StreamWriter(
                "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/runder/runde-" +
                roundNumber + ".csv");
            sw.WriteLine("Runde " + roundNumber + " resultater:");
            for (int i = 0; i < tempGames.Count; i++)
            {
                int homeGoals = rnd.Next(1, 6);
                int awayGoals = rnd.Next(1, 6);
                tempGames[i].Målhjemme = homeGoals;
                tempGames[i].Målude = awayGoals;
                sw.WriteLine(tempGames[i].Hjemmehold.Forkortelse + "-" + tempGames[i].Udehold.Forkortelse + "," 
                             + homeGoals + "," + awayGoals);
                for (int j = 0; j < tabel.Count; j++)
                {
                    if (tabel[j].Forkortelse.Equals(tempGames[i].Hjemmehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målhjemme;
                        tabel[j].Målimod += tempGames[i].Målude;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målhjemme > tempGames[i].Målude)
                        {
                            tabel[j].Point += 3;
                            tabel[j].Vundet += 1;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                            
                        }

                        if (tempGames[i].Målhjemme < tempGames[i].Målude)
                        {
                            tabel[j].Tabt += 1;
                        }
                        
                    } else if (tabel[j].Forkortelse.Equals(tempGames[i].Udehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målude;
                        tabel[j].Målimod += tempGames[i].Målhjemme;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målude > tempGames[i].Målhjemme)
                        {
                            tabel[j].Vundet += 1;
                            tabel[j].Point += 3;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                        }

                        if (tempGames[i].Målude < tempGames[i].Målhjemme)
                        {
                            tabel[j].Tabt += 1;
                        }
                    }
                }
            }

            return tabel;
        }

        public List<Hold> playChampionsPath(int roundNumber)
        {
            Random rnd = new Random();
            List<Hold> tabel = new List<Hold>();
            List<Kamp> tempGames = new List<Kamp>();
            tabel = hold.readPlayoffTeamsChamps(tabel);
            using StreamReader reader =
                new StreamReader(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/ChampionsPath/runde-" +
                    roundNumber + ".csv");
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string[] data = reader.ReadLine().Split("-");
                tempGames.Add(new Kamp(new Hold(data[0]), new Hold(data[1])));
                
            }
            reader.Close();
            using StreamWriter sw = new StreamWriter(
                "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/ChampionsPath/runde-" +
                roundNumber + ".csv");
            sw.WriteLine("Runde " + roundNumber + " championspath resultater:");
            for (int i = 0; i < tempGames.Count; i++)
            {
                int homeGoals = rnd.Next(1, 6);
                int awayGoals = rnd.Next(1, 6);
                tempGames[i].Målhjemme = homeGoals;
                tempGames[i].Målude = awayGoals;
                sw.WriteLine(tempGames[i].Hjemmehold.Forkortelse + "-" + tempGames[i].Udehold.Forkortelse + "," 
                             + homeGoals + "," + awayGoals);
                for (int j = 0; j < tabel.Count; j++)
                {
                    if (tabel[j].Forkortelse.Equals(tempGames[i].Hjemmehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målhjemme;
                        tabel[j].Målimod += tempGames[i].Målude;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målhjemme > tempGames[i].Målude)
                        {
                            tabel[j].Point += 3;
                            tabel[j].Vundet += 1;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                            
                        }

                        if (tempGames[i].Målhjemme < tempGames[i].Målude)
                        {
                            tabel[j].Tabt += 1;
                        }
                        
                    } else if (tabel[j].Forkortelse.Equals(tempGames[i].Udehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målude;
                        tabel[j].Målimod += tempGames[i].Målhjemme;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målude > tempGames[i].Målhjemme)
                        {
                            tabel[j].Vundet += 1;
                            tabel[j].Point += 3;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                        }

                        if (tempGames[i].Målude < tempGames[i].Målhjemme)
                        {
                            tabel[j].Tabt += 1;
                        }
                    }
                }
            }

            return tabel;
        }
        public List<Hold> playRelegationPath(int roundNumber)
        {
            Random rnd = new Random();
            List<Hold> tabel = new List<Hold>();
            List<Kamp> tempGames = new List<Kamp>();
            tabel = hold.readPlayoffTeamsRelegation(tabel);
            using StreamReader reader =
                new StreamReader(
                    "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/RelegationPath/runde-" +
                    roundNumber + ".csv");
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string[] data = reader.ReadLine().Split("-");
                tempGames.Add(new Kamp(new Hold(data[0]), new Hold(data[1])));
                
            }
            reader.Close();
            using StreamWriter sw = new StreamWriter(
                "C:/Users/emil_/RiderProjects/Superliga_Simulation/Superliga_Simulation/files/Playoffs/PlayOffRounds/RelegationPath/runde-" +
                roundNumber + ".csv");
            sw.WriteLine("Runde " + roundNumber + " RelegationPath resultater:");
            for (int i = 0; i < tempGames.Count; i++)
            {
                int homeGoals = rnd.Next(1, 6);
                int awayGoals = rnd.Next(1, 6);
                tempGames[i].Målhjemme = homeGoals;
                tempGames[i].Målude = awayGoals;
                sw.WriteLine(tempGames[i].Hjemmehold.Forkortelse + "-" + tempGames[i].Udehold.Forkortelse + "," 
                             + homeGoals + "," + awayGoals);
                for (int j = 0; j < tabel.Count; j++)
                {
                    if (tabel[j].Forkortelse.Equals(tempGames[i].Hjemmehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målhjemme;
                        tabel[j].Målimod += tempGames[i].Målude;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målhjemme > tempGames[i].Målude)
                        {
                            tabel[j].Point += 3;
                            tabel[j].Vundet += 1;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                            
                        }

                        if (tempGames[i].Målhjemme < tempGames[i].Målude)
                        {
                            tabel[j].Tabt += 1;
                        }
                        
                    } else if (tabel[j].Forkortelse.Equals(tempGames[i].Udehold.Forkortelse))
                    {
                        tabel[j].Målfor += tempGames[i].Målude;
                        tabel[j].Målimod += tempGames[i].Målhjemme;
                        tabel[j].KampeSpillet += 1;
                        if (tempGames[i].Målude > tempGames[i].Målhjemme)
                        {
                            tabel[j].Vundet += 1;
                            tabel[j].Point += 3;
                        }

                        if (tempGames[i].Målhjemme == tempGames[i].Målude)
                        {
                            tabel[j].Point += 1;
                            tabel[j].Uafgjort += 1;
                        }

                        if (tempGames[i].Målude < tempGames[i].Målhjemme)
                        {
                            tabel[j].Tabt += 1;
                        }
                    }
                }
            }

            return tabel;
        }

        public List<Hold> orderTabelByResults(List<Hold> tabel, int whichTable)
        {
            for (int i = 23; i <= 32; i++)
            {
                List<Hold> tempTabel = new List<Hold>();
                switch (whichTable)
                {
                    case 1:
                        tempTabel = playChampionsPath(i);
                        break;
                    case 2:
                        tempTabel = playRelegationPath(i);
                        break;
                }
                for (int j = 0; j < tempTabel.Count; j++)
                {
                    for (int k = 0; k < tabel.Count; k++)
                    {
                        if (tempTabel[j].Navn == tabel[k].Navn)
                        {
                            tabel[k].KampeSpillet += tempTabel[j].KampeSpillet;
                            tabel[k].Vundet += tempTabel[j].Vundet;
                            tabel[k].Uafgjort += tempTabel[j].Uafgjort;
                            tabel[k].Tabt += tempTabel[j].Tabt;
                            tabel[k].Målimod += tempTabel[j].Målimod;
                            tabel[k].Målfor += tempTabel[j].Målfor;
                            tabel[k].Point += tempTabel[j].Point;
                        }
                    }
                }
            }

            return tabel;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < runder.Count; i++)
            {
                sb.Append($" {runder[i].Hjemmehold.Forkortelse} - {runder[i].Udehold.Forkortelse} |"); 
            }

            return sb.ToString();
        }
    }
}

