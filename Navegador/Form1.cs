using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegador
{
    public partial class Form1 : Form
    {
        private readonly string historialFile = @"../../historial.txt";
        public Form1()
        {
            InitializeComponent();
            InicializarWebView();
        }

        public async void InicializarWebView()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.Source = new Uri("https://www.google.com");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
            CargarHistorial();

        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            NavegarDesdeTextBox();

        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://www.google.com");
        }

        private void haciaAtrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoBack) webView21.GoBack();
        }

        private void haciaDelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (webView21.CanGoForward) webView21.GoForward();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string url = ObtenerURLValida(comboBox1.SelectedItem.ToString());
                txtDireccion.Text = url;
            }
        }
        private void NavegarDesdeTextBox()
        {
            string url = ObtenerURLValida(txtDireccion.Text);
            webView21.Source = new Uri(url);
            txtDireccion.Text = url;
            Guardar(url);
        }

        private string ObtenerURLValida(string entrada)
        {
            entrada = entrada.Trim();

            if (!entrada.Contains("."))
            {
                return "https://www.google.com/search?q=" + Uri.EscapeDataString(entrada);
            }

            if (!entrada.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
                !entrada.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                entrada = "https://" + entrada;
            }

            return entrada;
        }
        private void Guardar(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            List<string> historial = new List<string>();

            // Leer historial si el archivo existe
            if (File.Exists(historialFile))
            {
                historial = File.ReadAllLines(historialFile).ToList();
            }

            // Evitar duplicados y agregar nueva URL al inicio
            historial.Remove(url);
            historial.Insert(0, url);

            // Mantener solo las últimas 10 entradas
            if (historial.Count > 10)
            {
                historial = historial.Take(10).ToList();
            }

            // Guardar en el archivo
            File.WriteAllLines(historialFile, historial);

            // Refrescar historial en ComboBox
            CargarHistorial();

        }
        private void CargarHistorial()
        {
            comboBox2.Items.Clear();

            if (File.Exists(historialFile))
            {
                var historial = File.ReadAllLines(historialFile);
                comboBox2.Items.AddRange(historial);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string url = ObtenerURLValida(comboBox1.SelectedItem.ToString());
                txtDireccion.Text = url;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

