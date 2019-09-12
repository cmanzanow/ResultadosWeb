using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Resultados
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int[] Touchs = new int[5];
        string[] Nombres = new string[5];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CheckMysqlConnection();
                ObtenerDatos();
            }

        }

        private void CheckMysqlConnection()
        {
            string connectionString = @"Data Source=ulearnet.org; Database=ulearnet_reim_pilotaje; User ID=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT;";

            using(MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();
                //Response.Write("Mysql Connection Successful.");
                cn.Close();
            }
        }

        protected void ObtenerDatos()
        {
            int count = 0;
            string connectionString = @"Data Source=ulearnet.org; Database=ulearnet_reim_pilotaje; User ID=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT;";
            MySqlConnection cn = new MySqlConnection(connectionString);
            MySqlCommand query = new MySqlCommand("SELECT u.nombres, count(a.id_user) as CantidadTouch FROM alumno_respuesta_actividad a, usuario u where a.id_user= u.id group by id_user;", cn);

            cn.Open();
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                Touchs[count] = Convert.ToInt32(reader.GetString(1));
                Nombres[count] = reader.GetString(0);
                count++;
            }

            reader.Close();
            cn.Close();

            Grafico.Series["Serie"].Points.DataBindXY(Nombres, Touchs);

        }
    }
}