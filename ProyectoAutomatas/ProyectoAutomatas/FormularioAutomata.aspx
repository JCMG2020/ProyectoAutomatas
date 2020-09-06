<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioAutomata.aspx.cs" Inherits="ProyectoAutomatas.FormularioAutomata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TxtCadena" runat="server"></asp:TextBox>
            <asp:Button ID="BtnEvaluaCadena" Text="Evaluar Cadena" OnClick="BtnEvaluaCadena_Click" runat="server" />
        </div>
    </form>
</body>
</html>
