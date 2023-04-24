using System.IO;
namespace Superliga_Simulation
{
    public class MainSimulation
    {
        public static void Main(string[] args)
        {
            Hold team = new Hold();
            Runde runde = new Runde();
            Kamp kamp = new Kamp();
            List<Kamp> gamesPlannedMain = kamp.readGames();
            List<Kamp> playOffPlannedChampion = kamp.readPlayOffGamesChampions();
            List<Kamp> playOffPlannedRelegation = kamp.readPlayOffGamesRelegation();
            List<Hold> tabel = new List<Hold>();
            List<Hold> tempTabel = new List<Hold>();
            for (int i = 1; i <= 22; i++)
            {
                    Runde rundeN = runde.GenerateRound(gamesPlannedMain);
                    runde.SaveRound(rundeN, i);
                    gamesPlannedMain.AddRange(rundeN.Runder);
            }
            tabel = team.ReadTeams(tabel);
            for (int i = 1; i <= 22; i++)
            {
               tempTabel = runde.PlayRound(i);
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
            team.UpdateTabel(tabel);
            team.DivideTable();
            List<Hold> tabelChamps = new List<Hold>();
            List<Hold> tabelRelegation = new List<Hold>();
            List<Hold> tempChamps = new List<Hold>();
            List<Hold> tempRelegation = new List<Hold>();
            for (int i = 23; i <= 32; i++)
            {
                List<Runde> rounds = runde.generatePlayOffRounds(playOffPlannedChampion, playOffPlannedRelegation);
                runde.savePlayOffRounds(rounds, i);
                playOffPlannedChampion.AddRange(rounds[0].Runder);
                playOffPlannedRelegation.AddRange(rounds[1].Runder);
            }

            tabelChamps = team.readPlayoffTeamsChamps(tabelChamps);
            for (int i = 23; i <= 32; i++)
            {
                tempChamps = runde.playChampionsPath(i);
                for (int j = 0; j < tempChamps.Count; j++)
                {
                    for (int k = 0; k < tabelChamps.Count; k++)
                    {
                        if (tempChamps[j].Navn == tabelChamps[k].Navn)
                        {
                            tabelChamps[k].KampeSpillet += tempChamps[j].KampeSpillet;
                            tabelChamps[k].Vundet += tempChamps[j].Vundet;
                            tabelChamps[k].Uafgjort += tempChamps[j].Uafgjort;
                            tabelChamps[k].Tabt += tempChamps[j].Tabt;
                            tabelChamps[k].Målimod += tempChamps[j].Målimod;
                            tabelChamps[k].Målfor += tempChamps[j].Målfor;
                            tabelChamps[k].Point += tempChamps[j].Point;
                        }
                    }
                }
            }
            tabelRelegation = team.readPlayoffTeamsRelegation(tabelRelegation);
            for (int i = 23; i <= 32; i++)
            {
                tempRelegation = runde.playRelegationPath(i);
                for (int j = 0; j < tempRelegation.Count; j++)
                {
                    for (int k = 0; k < tabelRelegation.Count; k++)
                    {
                        if (tempRelegation[j].Navn == tabelRelegation[k].Navn)
                        {
                            tabelRelegation[k].KampeSpillet += tempRelegation[j].KampeSpillet;
                            tabelRelegation[k].Vundet += tempRelegation[j].Vundet;
                            tabelRelegation[k].Uafgjort += tempRelegation[j].Uafgjort;
                            tabelRelegation[k].Tabt += tempRelegation[j].Tabt;
                            tabelRelegation[k].Målimod += tempRelegation[j].Målimod;
                            tabelRelegation[k].Målfor += tempRelegation[j].Målfor;
                            tabelRelegation[k].Point += tempRelegation[j].Point;
                        }
                    }
                }
            }
            // tabelChamps = runde.orderTabelByResults( tabelChamps, 1);
            // tabelRelegation = runde.orderTabelByResults( tabelRelegation, 2);
            team.finalStandings(tabelChamps, tabelRelegation);
            team.printTable();
        }
    }
}
