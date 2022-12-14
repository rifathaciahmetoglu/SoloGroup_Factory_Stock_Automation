using Npgsql;
using System;
using System.Windows.Forms;



namespace SoloGroup_Factory_Stock_Automation
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }
        NpgsqlConnection con;
        DataBase Fonksiyon = new DataBase();
        public string databilgileri = "server=localHost; port = 5432; dataBase=SoloGroup; user Id=postgres; password=solo";
        public string gunluk_tablo = "SELECT *FROM gunluk_urunler";
        public string isg_tablo = "SELECT *FROM isg_urunler";
        public string yemekhane_tablo = "SELECT *FROM yemekhane_urunler";
        public string bahce_tablo = "SELECT *FROM bahce_urunler";
        public int categoryNo;

        private void DataShow(string tabloadi)
        {
            con = Fonksiyon.Baglanti(databilgileri);
            con.Open();
            dataGridView1.DataSource = Fonksiyon.DataCek(con, tabloadi);
            con.Close();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HomePage_Load_1(object sender, EventArgs e)
        {
            timer1.Start();
            SarfPanel.Visible = true;
            Home_Panel.Visible = true;
            CustomizeDesign();
            Add_Panel.Visible = false;

        }

        private void BunifuFlatButton3_Click(object sender, EventArgs e)
        {
            SarfPanel.Visible = true;
            Home_Panel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Edit.Visible = true;
            Btn_Retry.Visible = true;
            Btn_Delete.Visible = true;
            ShowSubMenu(PanelCategory);
        }

        private void BunifuFlatButton1_Click(object sender, EventArgs e)//Home Button
        {
            SarfPanel.Visible = true;
            Home_Panel.Visible = true;
            Btn_Add.Visible = false;
            Btn_Edit.Visible = false;
            Btn_Retry.Visible = false;
            Btn_Delete.Visible = false;
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToLongTimeString();
            DateLabel.Text=DateTime.Now.ToLongDateString();
        }

        private void Btn_Maximized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void Btn_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CustomizeDesign()
        {
            PanelCategory.Visible = false;
        }

        private void HideSubMenu()
        {
            if (PanelCategory.Visible == true)
                PanelCategory.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        public int islem;
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            Add_Panel.Visible = true;
            Txt_Clear();
            islem = 1;
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            Add_Panel.Visible = true;
            Txt_Clear();
            islem = 2;


            string ad = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string adet = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string kodu = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string barkod= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            string tanim = dataGridView1.CurrentRow.Cells[5].Value.ToString();


            Txt_productName.Text = ad;
            Txt_productNumber.Text = adet;
            Txt_productCode.Text = kodu;
            Txt_productBarcode.Text = barkod;
            Txt_productDescription.Text = tanim;


        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            string id = string.Empty;
                
            try
      
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Txt_id_goster.Text = id;
            if (categoryNo == 1)
            {
                con = Fonksiyon.Baglanti(databilgileri);
                Fonksiyon.DataSil
                    (con,"DELETE FROM gunluk_urunler WHERE id=@id",
                    "@id",
                    Convert.ToInt32(Txt_id_goster.Text));
                DataShow(gunluk_tablo);
            }
            if(categoryNo == 2)
            {
                con = Fonksiyon.Baglanti(databilgileri);
                Fonksiyon.DataSil
                    (con, "DELETE FROM isg_urunler WHERE id=@id",
                    "@id",
                    Convert.ToInt32(Txt_id_goster.Text));
                DataShow(isg_tablo);
            }
            if (categoryNo == 3)
            {
                con = Fonksiyon.Baglanti(databilgileri);
                Fonksiyon.DataSil
                    (con, "DELETE FROM yemekhane_urunler WHERE id=@id",
                    "@id",
                    Convert.ToInt32(Txt_id_goster.Text));
                DataShow(yemekhane_tablo);
            }
            if (categoryNo == 4)
            {
                con = Fonksiyon.Baglanti(databilgileri);
                Fonksiyon.DataSil
                    (con, "DELETE FROM bahce_urunler WHERE id=@id",
                    "@id",
                    Convert.ToInt32(Txt_id_goster.Text));
                DataShow(bahce_tablo);
            }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Btn_Retry_Click(object sender, EventArgs e)
        {
            if(categoryNo==1)
            {
                DataShow(gunluk_tablo);
            }
            else if(categoryNo==2)
            {
                DataShow(isg_tablo);
            }
            else if(categoryNo==3)
            {
                DataShow(yemekhane_tablo);
            }
            else if(categoryNo==4)
            {
                DataShow(bahce_tablo);
            }
        }

        private void Btn_LogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Btn_Menu_Click_2(object sender, EventArgs e)
        {
            if(SideMenu.Width==50)
            {
                SideMenu.Visible = false;
                SideMenu.Width = 300;
                PanelTransition.ShowSync(SideMenu);
                LogoTransition.ShowSync(Logo);
            }
            else
            {
                LogoTransition.Hide(Logo);
                SideMenu.Visible = false;
                SideMenu.Width = 50;
                PanelTransition.ShowSync(SideMenu);
            }
        }
        //Category Buton Ayarları
        private void BtnCategory1_Click(object sender, EventArgs e)
        {
            categoryNo = 1;
            DataShow(gunluk_tablo);
            SarfPanel.Visible = true;
            Home_Panel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Edit.Visible = true;
            Btn_Retry.Visible = true;
            Btn_Delete.Visible = true;
            HideSubMenu();
        }

        private void BtnCategory2_Click(object sender, EventArgs e)
        {
            categoryNo = 2;
            DataShow(isg_tablo);
            SarfPanel.Visible = true;
            Home_Panel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Edit.Visible = true;
            Btn_Retry.Visible = true;
            Btn_Delete.Visible = true;
            HideSubMenu();
        }

        private void Category3_Click(object sender, EventArgs e)
        {
            categoryNo = 3;
            DataShow(yemekhane_tablo);
            SarfPanel.Visible = true;
            Home_Panel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Edit.Visible = true;
            Btn_Retry.Visible = true;
            Btn_Delete.Visible = true;
            HideSubMenu();
        }

        private void Category4_Click(object sender, EventArgs e)
        {
            categoryNo = 4;
            DataShow(bahce_tablo);
            SarfPanel.Visible = true;
            Home_Panel.Visible = false;
            Btn_Add.Visible = true;
            Btn_Edit.Visible = true;
            Btn_Retry.Visible = true;
            Btn_Delete.Visible = true;
            HideSubMenu();
        }
        //Category Buton ayarları bitti

        private void Txt_Clear() 
        {
            Txt_productName.Text = "Ürün Adı:";
            Txt_productNumber.Text = "Ürün Sayısı:";
            Txt_productCode.Text = "Ürün kodu:";
            Txt_productBarcode.Text = "Ürün Barkod Numarası:";
            Txt_productDescription.Text = "Ürün Tanımı:";
        }

        public string sarf_sorgu = "Update gunluk_urunler Set urun_adi=@urun_adi, urun_adet=@urun_adet,urun_kodu=@urun_kodu,urun_barkod_no=@urun_barkod_no,urun_tanimi=@urun_tanimi Where urun_adi=@urun_adi";
        public string isg_sorgu = "Update isg_urunler Set urun_adi=@urun_adi,urun_adet=@urun_adet,urun_kodu=@urun_kodu,urun_barkod_no=@urun_barkod_no,urun_tanimi=@urun_tanimi Where urun_adi=@urun_adi";
        public string yemekhane_sorgu = "Update yemekhane_urunler Set urun_adi=@urun_adi,urun_adet=@urun_adet,urun_kodu=@urun_kodu,urun_barkod_no=@urun_barkod_no,urun_tanimi=@urun_tanimi Where urun_adi=@urun_adi";
        public string bahce_sorgu = "Update bahce_urunler Set urun_adi=@urun_adi,urun_adet=@urun_adet,urun_kodu=@urun_kodu,urun_barkod_no=@urun_barkod_no,urun_tanimi=@urun_tanimi Where urun_adi=@urun_adi";
        public string urun_adi = "@urun_adi";
        public string urun_adet = "@urun_adet";
        public string urun_kodu = "@urun_kodu";
        public string urun_barkod = "@urun_barkod_no";
        public string urun_tanimi = "@urun_tanimi";
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //Ürün ekleme
            if (islem==1)
            {
                if (categoryNo == 1)
                {
                    string veriekle = "insert into gunluk_urunler (urun_adi,urun_adet,urun_kodu,urun_barkod_no,urun_tanimi) "
                        + "values ('" + Txt_productName.Text + "','" + Txt_productNumber.Text + "','" + Txt_productCode.Text + "'" + "," +
                        "'" + Txt_productBarcode.Text + "','" + Txt_productDescription.Text + "')";
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataEkle(con, veriekle);
                    Txt_Clear();
                    DataShow(gunluk_tablo);
                    Add_Panel.Visible = false;
                }
                else if (categoryNo == 2)
                {
                    string veriekle1 = "insert into isg_urunler (urun_adi,urun_adet,urun_kodu,urun_barkod_no,urun_tanimi) "
                         + "values ('" + Txt_productName.Text + "','" + Txt_productNumber.Text + "','" + Txt_productCode.Text + "'" + "," +
                         "'" + Txt_productBarcode.Text + "','" + Txt_productDescription.Text + "')";
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataEkle(con, veriekle1);
                    Txt_Clear();
                    DataShow(isg_tablo);
                    Add_Panel.Visible = false;
                }
                else if (categoryNo == 3)
                {
                    string veriekle1 = "insert into yemekhane_urunler (urun_adi,urun_adet,urun_kodu,urun_barkod_no,urun_tanimi) "
                         + "values ('" + Txt_productName.Text + "','" + Txt_productNumber.Text + "','" + Txt_productCode.Text + "'" + "," +
                         "'" + Txt_productBarcode.Text + "','" + Txt_productDescription.Text + "')";
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataEkle(con, veriekle1);
                    Txt_Clear();
                    DataShow(yemekhane_tablo);
                    Add_Panel.Visible = false;
                }
                else if (categoryNo == 4)
                {
                    string veriekle1 = "insert into bahce_urunler (urun_adi,urun_adet,urun_kodu,urun_barkod_no,urun_tanimi) "
                         + "values ('" + Txt_productName.Text + "','" + Txt_productNumber.Text + "','" + Txt_productCode.Text + "'" + "," +
                         "'" + Txt_productBarcode.Text + "','" + Txt_productDescription.Text + "')";
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataEkle(con, veriekle1);
                    Txt_Clear();
                    DataShow(bahce_tablo);
                    Add_Panel.Visible = false;
                }
            }
            //Ürün ekleme bitti

            //Ürün Düzenleme
            if (islem == 2)
            {
                if (categoryNo == 1)
                {
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataDuzenle(con, sarf_sorgu, urun_adi, urun_adet, urun_kodu, urun_barkod, urun_tanimi, Txt_productName.Text, Txt_productNumber.Text, Txt_productCode.Text, Txt_productBarcode.Text, Txt_productDescription.Text);
                    MessageBox.Show("Düzenleme Başarılı");
                    DataShow(gunluk_tablo);
                    Add_Panel.Visible = false;
                }
                else if (categoryNo == 2)
                {
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataDuzenle(con, isg_sorgu, urun_adi, urun_adet, urun_kodu, urun_barkod, urun_tanimi, Txt_productName.Text, Txt_productNumber.Text, Txt_productCode.Text, Txt_productBarcode.Text, Txt_productDescription.Text);
                    MessageBox.Show("Düzenleme Başarılı");
                    DataShow(isg_tablo);
                    Add_Panel.Visible = false;
                }
                else if (categoryNo==3)
                {
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataDuzenle(con, yemekhane_sorgu, urun_adi, urun_adet, urun_kodu, urun_barkod, urun_tanimi, Txt_productName.Text, Txt_productNumber.Text, Txt_productCode.Text, Txt_productBarcode.Text, Txt_productDescription.Text);
                    MessageBox.Show("Düzenleme Başarılı");
                    DataShow(yemekhane_tablo);
                    Add_Panel.Visible = false;
                }
                else if(categoryNo==4)
                {
                    con = Fonksiyon.Baglanti(databilgileri);
                    Fonksiyon.DataDuzenle(con, bahce_sorgu, urun_adi, urun_adet, urun_kodu, urun_barkod, urun_tanimi, Txt_productName.Text, Txt_productNumber.Text, Txt_productCode.Text, Txt_productBarcode.Text, Txt_productDescription.Text);
                    MessageBox.Show("Düzenleme Başarılı");
                    DataShow(bahce_tablo);
                    Add_Panel.Visible = false;
                }
            }
            //Ürün Düzenleme bitti
        }

        // Textbox Metin ayarları
        private void Txt_productName_Enter(object sender, EventArgs e)
        {
            if (Txt_productName.Text == "Ürün Adı:")
                Txt_productName.Text = "";
        }

        private void Txt_productName_Leave(object sender, EventArgs e)
        {
            if (Txt_productName.Text == "")
                Txt_productName.Text = "Ürün Adı:";
        }

        private void Txt_productNumber_Enter(object sender, EventArgs e)
        {
            if (Txt_productNumber.Text == "Ürün Sayısı:")
                Txt_productNumber.Text = "";
        }

        private void Txt_productNumber_Leave(object sender, EventArgs e)
        {
            if(Txt_productNumber.Text == "")
                Txt_productNumber.Text = "Ürün Sayısı:";
        }

        private void Txt_productCode_Enter(object sender, EventArgs e)
        {
            if (Txt_productCode.Text == "Ürün kodu:")
                Txt_productCode.Text = "";
        }

        private void Txt_productCode_Leave(object sender, EventArgs e)
        {
            if (Txt_productCode.Text == "")
                Txt_productCode.Text = "Ürün kodu:";
        }

        private void Txt_productBarcode_Enter(object sender, EventArgs e)
        {
            if (Txt_productBarcode.Text == "Ürün Barkod Numarası:")
                Txt_productBarcode.Text = "";
        }

        private void Txt_productBarcode_Leave(object sender, EventArgs e)
        {
            if (Txt_productBarcode.Text == "")
                Txt_productBarcode.Text = "Ürün Barkod Numarası:";
        }

        private void Txt_productDescription_Enter(object sender, EventArgs e)
        {
            if (Txt_productDescription.Text == "Ürün Tanımı:")
                Txt_productDescription.Text = "";
        }

        private void Txt_productDescription_Leave(object sender, EventArgs e)
        {
            if (Txt_productDescription.Text == "")
                Txt_productDescription.Text = "Ürün Tanımı:";
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Txt_Clear();
            Add_Panel.Visible = false;
        }
        //Textbox metin ayarları bitti
    }
}
