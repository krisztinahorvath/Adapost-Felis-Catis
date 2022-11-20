using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace adapost_atestat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxNumeVet.Focus();
            
        }
        
        string parola_vet;
        int idv;
        private void diagnosticBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.diagnosticBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.adapostDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapostDataSet.EnforceConstraints = false; 
            // TODO: This line of code loads data into the 'adapostDataSet.veterinar' table. You can move, or remove it, as needed.
            //this.veterinarTableAdapter.Fill(this.adapostDataSet.veterinar);
            // TODO: This line of code loads data into the 'adapostDataSet.vaccin' table. You can move, or remove it, as needed.
            //this.vaccinTableAdapter.Fill(this.adapostDataSet.vaccin);
            // TODO: This line of code loads data into the 'adapostDataSet.pisica' table. You can move, or remove it, as needed.
            //this.pisicaTableAdapter.Fill(this.adapostDataSet.pisica);
            // TODO: This line of code loads data into the 'adapostDataSet.diagnostic' table. You can move, or remove it, as needed.
            //this.diagnosticTableAdapter.Fill(this.adapostDataSet.diagnostic);
            
        }

        private void homepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;

        }

        private void veterinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //VETERINAR
            tabControl1.SelectedTab = tabPage2;
            textBoxNumeVet.Focus();
            
        }

        private void pacientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //autentificare veterinar
            richTextBox1.Clear();
            comboBox1.Text = "";
            veterinarTableAdapter.AutentificareVeterinar(adapostDataSet.veterinar, textBoxNumeVet.Text, textBoxPrenumeVet.Text, textBoxParolaVet.Text);
            DataTable dt = adapostDataSet.veterinar;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Date incorecte");
                checkBoxParolaUitata.Visible=true;
            }
            else 
            {
                tabControl1.SelectTab(2);
                parola_vet = textBoxParolaVet.Text;
                veterinarTableAdapter.GenerareVeterinarID(adapostDataSet.veterinar, parola_vet);
                DataTable nr = adapostDataSet.veterinar;
                idv = Convert.ToInt32(nr.Rows[0]["idv"]);
                textBoxNumeVet.Clear();
                textBoxPrenumeVet.Clear();
                textBoxParolaVet.Clear();
                textBoxNumeVet.Focus();
            }
        }

        /**private void pacientiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Pacienți")
            {
                richTextBox1.Clear();
                pisicaTableAdapter.PacientiiUnuiVeterinar(adapostDataSet.pisica, parola_vet);
                DataTable dt = adapostDataSet.pisica;
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        richTextBox1.Text += dt.Rows[i]["idp"] + "  " + dt.Rows[i]["nume"] + "  " + dt.Rows[i]["rasa"] + "  " + dt.Rows[i]["datan"].ToString() + "  " + dt.Rows[i]["sex"] + '\n';
                }
                else MessageBox.Show("Nu exista date despre optiunea selectata.");
            }
            else if (comboBox1.Text == "Pacientii care trebuie tratati imediat")
            {
                richTextBox1.Clear();
                pisicaTableAdapter.Med(adapostDataSet.pisica, idv);
                DataTable dt1 = adapostDataSet.pisica;
                if (dt1.Rows.Count != 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                        richTextBox1.Text += dt1.Rows[i]["idp"] + "  " + dt1.Rows[i]["nume"] + '\n';
                }
                else MessageBox.Show("Nu exista date despre optiunea selectata.");
            }
            else if (comboBox1.Text == "Numele pisicilor femele")
            {
                richTextBox1.Clear();
                pisicaTableAdapter.NumelePisicilorFemele(adapostDataSet.pisica, idv);
                DataTable dt = adapostDataSet.pisica;
                if (dt.Rows.Count != 0)
                { 
                    for(int i=0;i<dt.Rows.Count;i++)
                    richTextBox1.Text += dt.Rows[i]["nume"].ToString() + '\n';
                }
                else MessageBox.Show("Nu exista date despre optiunea selectata.");
                
            }
            else if (comboBox1.Text == "Cate pisici sunt de rasa")
            {
                richTextBox1.Clear();
                int nr=Convert.ToInt32(this.pisicaTableAdapter.NrPisiciRasa(idv));
                richTextBox1.Text += nr.ToString();
            }
           
        }

        private void buttonAfisaPacient_Click(object sender, EventArgs e)
        {
            try
            {
                veterinarTableAdapter.ExistaPisicaID(adapostDataSet.veterinar, Convert.ToInt32(textBoxPacientID.Text), parola_vet);
                DataTable dt = adapostDataSet.veterinar;
                if (dt.Rows.Count != 0)
                    tabControl1.SelectTab(3);
                else
                {
                    MessageBox.Show("ID invalid sau nu este al pacientului dumneavoastra");
                    textBoxPacientID.Clear();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Date invalide");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            textBoxPacientID.Clear();
            richTextBoxInformatiiPisica.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxInformatiiPisica.Clear();
            int idpisica = Convert.ToInt32(textBoxPacientID.Text);
            if (comboBox2.Text == "Informatii generale")
            {
                pisicaTableAdapter.PacientDateGenerale(adapostDataSet.pisica, idpisica, parola_vet);
                DataTable dt = adapostDataSet.pisica;
                for (int i = 0; i < dt.Rows.Count; i++)
                    richTextBoxInformatiiPisica.Text += dt.Rows[i]["idp"] + "  " + dt.Rows[i]["nume"] + "  " + dt.Rows[i]["rasa"] + "  " + dt.Rows[i]["datan"].ToString() + "  " + dt.Rows[i]["sex"] + '\n';
            }
            else if (comboBox2.Text == "Diagnostice")
            {
                diagnosticTableAdapter.AfisareDiagnostice(adapostDataSet.diagnostic, idpisica);
                DataTable dt = adapostDataSet.diagnostic;
                for (int i = 0; i < dt.Rows.Count; i++)
                    richTextBoxInformatiiPisica.Text += dt.Rows[i]["boala"] + "  " + dt.Rows[i]["data_diagnostic"].ToString() + "  " + dt.Rows[i]["tratament"] + "  " + dt.Rows[i]["grad_letalitate"]+"%" + " " + '\n';
                if (dt.Rows.Count == 0)
                    MessageBox.Show("Nu exista date despre diagnostice");
            }
            else if (comboBox2.Text == "Dieta")
            {
                pisicaTableAdapter.InformatiiDieta(adapostDataSet.pisica, idpisica);
                DataTable dt = adapostDataSet.pisica;
                for (int i = 0; i < dt.Rows.Count; i++)
                    richTextBoxInformatiiPisica.Text += "Greutate:" + dt.Rows[i]["greutate"]+ "kg" + '\n' + dt.Rows[i]["lungime"].ToString() + "cm" + "  " + dt.Rows[i]["hrana_rec"] + "  " + dt.Rows[i]["gramaj"] + "gr/zi" + '\n';
                if (dt.Rows.Count == 0)
                    MessageBox.Show("Nu exista date despre dieta");
            }
            else if (comboBox2.Text == "Vaccin")
            {
                vaccinTableAdapter.InformatiiVaccinuri(adapostDataSet.vaccin, idpisica);
                DataTable dt = adapostDataSet.vaccin;
                for (int i = 0; i < dt.Rows.Count; i++)
                    richTextBoxInformatiiPisica.Text += dt.Rows[i]["tip_vaccin"] + " " + dt.Rows[i]["data"].ToString() + "  " + '\n';
                if (dt.Rows.Count == 0)
                    MessageBox.Show("Nu exista date despre vaccinuri");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text == "Vaccin")
            {
                tabControl1.SelectTab(5);
                textBoxTipVaccin.Focus();
            }
            else if (listBox1.Text == "Pisica")
            {
                tabControl1.SelectTab(6);
                textBoxAdaugareNume.Focus();
            }
            else if (listBox1.Text == "Diagnostic")
            {
                tabControl1.SelectTab(7);
                textBoxBoala.Focus();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //adaugare vaccin
            try
            {
               int cod_carnet = Convert.ToInt32(textBoxVaccinIDC.Text);
               veterinarTableAdapter.ExistaPisicaID(adapostDataSet.veterinar, cod_carnet, parola_vet);
               DataTable dt = adapostDataSet.veterinar;
               if (dt.Rows.Count != 0)
               {
                   this.vaccinTableAdapter.AdaugareVaccin(dateTimePickerVaccin.Value.ToString(), textBoxTipVaccin.Text, cod_carnet);
                   vaccinTableAdapter.Update(adapostDataSet);
                   MessageBox.Show("Date adaugate");
                   textBoxTipVaccin.Clear();
                   textBoxVaccinIDC.Clear();
                   textBoxTipVaccin.Focus();
               }
               else
               {
                   MessageBox.Show("ID invalid sau nu este al pacientului dumneavoastra");
                   textBoxVaccinIDC.Clear();
               }
           }
           catch (Exception E)
           {
               MessageBox.Show("Date invalide");
           }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //adaugare pisica
            try
            {
                int idc = Convert.ToInt32(pisicaTableAdapter.IDCurmator()) + 1;
                MessageBox.Show(idv.ToString());

                string nume = textBoxAdaugareNume.Text;
                string rasa = textBoxAdaugareRasa.Text;
                double greutate = Convert.ToDouble(textBoxAdaugareGreutate.Text);
                int lungime = Convert.ToInt32(numericUpDownLungime.Value);
                string sex = comboBox3.Text;
                string comportament = textBoxAdaugareComportament.Text;
                string hrana = textBoxHrana.Text;
                int gramaj = Convert.ToInt32(textBoxAdaugareGramaj.Text);
                string data = dateTimePickerPisica.Value.ToString();
                this.pisicaTableAdapter.AdaugarePisica(idc, nume, rasa, data, greutate, lungime, sex, comportament, idv, idc, hrana, gramaj);
                pisicaTableAdapter.Update(adapostDataSet);
                MessageBox.Show("Date adaugate");
                textBoxAdaugareNume.Clear();
                textBoxAdaugareNume.Focus();
                textBoxAdaugareRasa.Clear();
                textBoxAdaugareGreutate.Clear();
                textBoxAdaugareGramaj.Clear();
                textBoxHrana.Clear();
                textBoxAdaugareComportament.Clear();
                numericUpDownLungime.Value = 0;
                comboBox3.Text = "";

            }
            catch (Exception E)
            {
                MessageBox.Show("Date invalide");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //adaugare diagnostic
            try
            {
                int cod_carnet = Convert.ToInt32(textBoxDiagnosticCarnet.Text);
                veterinarTableAdapter.ExistaPisicaID(adapostDataSet.veterinar, cod_carnet, parola_vet);
                DataTable dt = adapostDataSet.veterinar;
                if (dt.Rows.Count != 0)
                {
                    this.diagnosticTableAdapter.AdaugareDiagnostic(textBoxBoala.Text, dateTimePickerDiagnostic.Value.ToString(), textBoxTratament.Text, Convert.ToInt32(numericUpDownLetatlitate.Value), cod_carnet);
                    diagnosticTableAdapter.Update(adapostDataSet);
                    MessageBox.Show("Date adaugate");
                    textBoxBoala.Clear();
                    textBoxBoala.Focus();
                    textBoxTratament.Clear();
                    numericUpDownLetatlitate.Value = 0;
                    textBoxDiagnosticCarnet.Clear();
                }
                else 
                {
                    MessageBox.Show("ID invalid sau nu este al pacientului dumneavoastra");
                    textBoxDiagnosticCarnet.Clear();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Date invalide");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ///selectare ce tip de date se introduce
            tabControl1.SelectTab(4);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ///selectare ce tip de date se introduce
            tabControl1.SelectTab(4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///selectare ce tip de date se introduce
            tabControl1.SelectTab(4);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxParolaUitata_CheckedChanged(object sender, EventArgs e)
        {
           ///// verificare parola uitata
           this.veterinarTableAdapter.GenerareParola(adapostDataSet.veterinar,textBoxNumeVet.Text, textBoxPrenumeVet.Text);
           DataTable x = adapostDataSet.veterinar;
           if (x.Rows.Count != 0)
           {
               parola_vet = x.Rows[0]["parola"].ToString();
               MessageBox.Show("Parola dumneavoastră este: " + parola_vet.ToString());
           }
           else
           {
               MessageBox.Show("Nume utilizator incorect.");
           }
        }

    }
}
