using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Code
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Metodo para criar o QRCode
        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (picQRCode.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Images | *.jpg *.png";
                sfd.ShowDialog();
                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    picQRCode.Image.Save(sfd.FileName);
                }
                else
                {
                    MessageBox.Show("Selecione Um Caminho Para Salvar O Arquivo", "Erro Nome do Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Imagem Não Encontrada", "Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGerarQRCode_Click(object sender, EventArgs e)
        {
            if (txtTexto.Text == string.Empty || txtLargura.Text == string.Empty && txtLargura.Text == string.Empty)
            {
                MessageBox.Show("Informações inválidas. Complete as informações para gerar o QRCode...");
                txtTexto.Focus();
                return;
            }
            try
            {
                int largura = Convert.ToInt32(txtLargura.Text);
                int altura = Convert.ToInt32(txtAltura.Text);
                picQRCode.Image = GerarQRCode(largura, altura, txtTexto.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
