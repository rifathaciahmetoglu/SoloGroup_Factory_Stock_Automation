using Npgsql;
using System;
using System.Windows.Forms;

namespace SoloGroup_Factory_Stock_Automation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection con;
        NpgsqlCommand com;
        NpgsqlDataReader dr;

        //Design
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
        }
        private void Txt_Username_Enter(object sender, EventArgs e)
        {
            if (Txt_Username.Text == "Username")
                Txt_Username.Text = "";
        }
        private void Txt_Username_Leave(object sender, EventArgs e)
        {
            if (Txt_Username.Text == "")
                Txt_Username.Text = "Username";
        }
        private void Txt_Password_Enter(object sender, EventArgs e)
        {
            if (Txt_Password.Text == "Password")
                Txt_Password.Text = "";
        }
        private void Txt_Password_Leave(object sender, EventArgs e)
        {
            if (Txt_Password.Text == "")
                Txt_Password.Text = "Password";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Txt_Username.MaxLength = 8;
            Txt_Password.MaxLength = 16;
            Txt_Password.isPassword = true;
            Txt_Username.characterCasing = CharacterCasing.Lower;
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool move;
        int mouse_x;
        int mouse_y;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = false;
            
            if(move==true)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
        //Design Fnish

        //Login Commands
        public string databilgileri = "server=localHost; port = 5432; dataBase=SoloGroup; user Id=postgres; password=solo";
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            con=dataBase.Baglanti(databilgileri);
            com = new NpgsqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText= "select*from login where username='" + Txt_Username.Text + "'and password='" + Txt_Password.Text + "'";
            dr = com.ExecuteReader();
            
            if (dr.Read())
            {
                    HomePage gecis = new HomePage();
                    gecis.Show();
                    this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
                Txt_Password.Text = "";
                Txt_Username.Text = "";
            }
            con.Close();

        }
        public void goster()
        {
            this.Show();
        }
    }
}