using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotInstagramSelenium
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        ChromeDriver driver; Thread th;
        string url = "https://www.instagram.com/";
        public string usuario;
        public string senha;

        private void TestNullTxtBox(object sender, EventArgs e)
        {
            usuario = txt_usuario.Text;
            senha = txt_senha.Text;

            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(senha))
                btn_login.ForeColor = Color.Lime;
            else
                btn_login.ForeColor = Color.Red;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (btn_login.ForeColor == Color.Lime)
            {
                th = new Thread(Login_Insta);
                th.Start();
            }
        }

        private void Login_Insta()
        {
            Thread.Sleep(3000);
            try
            {
                //Campo usuário
                driver.FindElement(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']")).SendKeys(usuario);
                Thread.Sleep(3000);

                //Campo senha
                driver.FindElement(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']")).SendKeys(senha);
                Thread.Sleep(3000);

                //botão login
                driver.FindElement(By.XPath("//button[@class = 'sqdOP  L3NKy   y3zKF     ']")).Click();
                Thread.Sleep(5000);

                string erroSenha = driver.FindElementByClassName("eiCW-")?.Text;
                if (erroSenha != null)
                {
                    MessageBox.Show("Senha incorreta!");
                    driver.Close();
                    Application.Exit();
                }
                else
                    ValidacaoPosLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
        }

        private void ValidacaoPosLogin()
        {
            driver.FindElement(By.XPath("//button[@class = 'sqdOP yWX7d    y3zKF     ']")).Click();
            Thread.Sleep(6000);

            driver.FindElement(By.XPath("//button[@class = 'aOOlW   HoLwm ']")).Click();
            Thread.Sleep(2000);

            // Ir para a pagina solicitada
            //try
            //{
            //    driver.Navigate().GoToUrl(linkPost); Thread.Sleep(2000);
            //    // Método sem retorno para escolha aleatória do comentário a ser digitado
            //    comentarios();
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            driver.Quit();
        }
    }
}
