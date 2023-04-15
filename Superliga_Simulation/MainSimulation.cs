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
            List<Kamp> gamesPlayed = kamp.readGames();
            List<Hold> tabel = new List<Hold>();
            List<Hold> tempTabel = new List<Hold>();
            //team.DivideTable();
            // for (int i = 1; i <= 22; i++)
            // {
            //     Runde rundeN = runde.GenerateRound(gamesPlayed);
            //     runde.SaveRound(rundeN, i);
            //     gamesPlayed.AddRange(rundeN.Runder);
            // }
            //
            // tabel = team.ReadTeams(tabel);
            // for (int i = 1; i <= 22; i++)
            // {
            //    tempTabel = runde.PlayRound(i);
            //    for (int j = 0; j < tempTabel.Count; j++)
            //    {
            //        for (int k = 0; k < tabel.Count; k++)
            //        {
            //            if (tempTabel[j].Navn == tabel[k].Navn)
            //            {
            //                tabel[k].KampeSpillet += tempTabel[j].KampeSpillet;
            //                tabel[k].Vundet += tempTabel[j].Vundet;
            //                tabel[k].Uafgjort += tempTabel[j].Uafgjort;
            //                tabel[k].Tabt += tempTabel[j].Tabt;
            //                tabel[k].Målimod += tempTabel[j].Målimod;
            //                tabel[k].Målfor += tempTabel[j].Målfor;
            //                tabel[k].Point += tempTabel[j].Point;
            //            }
            //        }
            //    }
            // }
            // team.UpdateTabel(tabel);
            // team.printTable();
        }
    }
}
