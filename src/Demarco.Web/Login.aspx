<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Demarco.Web.Login" %>  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
  <form>   
      <div class="login container mt-5">  
          <h2>Login</h2>  

          <div class="row"></div>

            <% if (!string.IsNullOrWhiteSpace(this.lblMensagem.Text)) { %>  
                <asp:Label ID="lblMensagem" runat="server" CssClass="alert alert-danger d-block text-center mb-3" role="alert"></asp:Label>
            <% }  %>  
          
          <div class="form-group">
              <asp:Label ID="lblUsuario" runat="server" Text="Usuário:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
           
          <div class="form-group">
              <asp:Label ID="lblSenha" runat="server" Text="Senha:" CssClass="form-label"></asp:Label>
              <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
          </div>
            
          <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary mt-3" OnClick="btnLogin_Click" />  
      </div>  
  </form>  
</asp:Content>
