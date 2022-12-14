using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloGroup_Factory_Stock_Automation
{
    internal class DataBase
    {
        public NpgsqlConnection Baglanti(string server)
        {
            NpgsqlConnection con = new NpgsqlConnection(server);
            return con;
        }
        public DataTable DataCek(NpgsqlConnection connection, string sqlsyntax)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sqlsyntax, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public void DataSil(NpgsqlConnection connection, string sorgu, string id, object convert)
        {
            NpgsqlCommand com = new NpgsqlCommand(sorgu, connection);
            com.Parameters.AddWithValue(id, convert);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();
        }
        public void DataDuzenle(NpgsqlConnection connection, string sorgu, string urun_adi, string urunadet,string urunkodu,string urunbarkod,string uruntanim,
            object txt_ad, object txt_adet, object txt_urunkodu, object txt_urunbarkod,object txt_uruntanim)
        {
            NpgsqlCommand com = new NpgsqlCommand(sorgu, connection);
            com.Parameters.AddWithValue(urun_adi, txt_ad);
            com.Parameters.AddWithValue(urunadet, txt_adet);
            com.Parameters.AddWithValue(urunkodu, txt_urunkodu);
            com.Parameters.AddWithValue(urunbarkod, txt_urunbarkod);
            com.Parameters.AddWithValue(uruntanim, txt_uruntanim);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();
        }
        public void DataEkle(NpgsqlConnection connection, string veriekle)
        {
            NpgsqlCommand com = new NpgsqlCommand();
            connection.Open();
            com.Connection = connection;
            com.CommandText = veriekle;
            com.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Kayıt eklendi");
        }
    }
}
