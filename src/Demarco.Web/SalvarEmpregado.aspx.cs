using Demarco.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Demarco.Web
{
    public partial class SalvarEmpregado : System.Web.UI.Page
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["apiUrl"];
        public string titulo = "Cadastrar empregado";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["JwtToken"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    titulo = "Atualizar empregado";

                    CarregarEmpregado(Request.QueryString["id"]);
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                lblMensagem.Text = "O campo CPF é obrigatório.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensagem.Text = "O campo Nome é obrigatório.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
            {
                lblMensagem.Text = "O campo Data de nascimento é obrigatório.";
                return;
            }

            var empregadoDTO = new EmpregadoDTO
            {
                ID = string.IsNullOrWhiteSpace(txtID.Text) ? 0 : Convert.ToInt32(txtID.Text),
                CPF = txtCpf.Text.Replace(".", "").Replace("-", ""),
                Nome = txtNome.Text,
                DataNascimento = DateTime.Parse(txtDataNascimento.Text)
            };

            if (empregadoDTO.ID == 0)
            {
                IncluirEmpregado(empregadoDTO);
            }
            else
            {
                AtualizarEmpregado(empregadoDTO);
            }
        }

        private void IncluirEmpregado(EmpregadoDTO empregadoDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["JwtToken"] as string);

                var json = JsonConvert.SerializeObject(empregadoDTO);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("Empregado", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Empregado.aspx");
                }
            }
        }

        private void AtualizarEmpregado(EmpregadoDTO empregadoDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["JwtToken"] as string);

                var json = JsonConvert.SerializeObject(empregadoDTO);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync("Empregado", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("Empregado.aspx");
                }
            }
        }

        private void CarregarEmpregado(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["JwtToken"] as string);

                HttpResponseMessage response = client.GetAsync("Empregado/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var empregadoDTO = JsonConvert.DeserializeObject<EmpregadoDTO>(result);

                    txtID.Text = empregadoDTO.ID.ToString();
                    txtCpf.Text = empregadoDTO.CPF;
                    txtNome.Text = empregadoDTO.Nome;
                    txtDataNascimento.Text = empregadoDTO.DataNascimento.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}
