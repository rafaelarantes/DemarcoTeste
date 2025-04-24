<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SalvarEmpregado.aspx.cs" Inherits="Demarco.Web.SalvarEmpregado" %>  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
   
   <div class="salvar-empregado container mt-5">  
      <h2><%= this.titulo %></h2>  
      <div class="row"></div>
      
       <% if (!string.IsNullOrWhiteSpace(this.lblMensagem.Text)) { %>  
           <asp:Label ID="lblMensagem" runat="server" CssClass="alert alert-danger d-block text-center mb-3" role="alert"></asp:Label>
       <% }  %>  
   
       <asp:TextBox ID="txtID" runat="server" Visible="false" />

        <div class="form-group">  
            <asp:Label ID="lblCpf" runat="server" Text="CPF:" CssClass="form-label"></asp:Label>  
            <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control cpf" MaxLength="14"></asp:TextBox>  
        </div>  
        <div class="form-group">  
            <asp:Label ID="lblNome" runat="server" Text="Nome:" CssClass="form-label"></asp:Label>  
            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>  
        </div>  
        <div class="form-group">  
            <asp:Label ID="lblDataNascimento" runat="server" Text="Data de nascimento:" CssClass="form-label"></asp:Label>  
            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control data-aniversario" MaxLength="10"></asp:TextBox>  
        </div>  
        <div class="form-group">  
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />  
        </div>  
  </div>

    <script>
      $(document).ready(function () {
          $('.cpf').mask('000.000.000-00');
          $('.data-aniversario').mask('00/00/0000');
      });
    </script>
</asp:Content>
