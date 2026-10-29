using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace projetStarGate
{
    public class MesDatas
    {
        private static DataSet dsGlobal = new DataSet();

        public static DataSet DsGlobal { get { return MesDatas.dsGlobal; } }

        public static DataTable ChargerTable(string nomTable, string requete)
        {
            DataTable dt = new DataTable(nomTable);

            using (SQLiteDataAdapter da = new SQLiteDataAdapter(requete, Connexion.Connec))
            {
                da.Fill(dt);
            }

            if (DsGlobal.Tables.Contains(nomTable))
                DsGlobal.Tables.Remove(nomTable);

            DsGlobal.Tables.Add(dt);
            return dt;
        }


    }
}
