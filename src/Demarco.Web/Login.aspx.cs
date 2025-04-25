using Demarco.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Demarco.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var usuario = txtUsuario.Text;
            var senha = txtSenha.Text;

            var httpClient = new HttpClient();

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    usuario,
                    senha
                }),
                Encoding.UTF8,
                "application/json"
            );

            string apiUrl = ConfigurationManager.AppSettings["apiUrl"];

            var response = httpClient.PostAsync(apiUrl + "Login", content).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                Session["JwtToken"] = responseString;
                Session["Usuario"] = usuario;

                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblMensagem.Text = "Usuário ou senha inválidos.";
            }
        }
    }
}