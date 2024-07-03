<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebGrafo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Grafo Web</title>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/vis/4.21.0/vis.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function drawGraph(nodesData, edgesData) {
            var container = document.getElementById('mynetwork');
            var data = {
                nodes: new vis.DataSet(nodesData),
                edges: new vis.DataSet(edgesData)
            };
            var options = {
                layout: {
                    hierarchical: false
                },
                edges: {
                    color: "#000000"
                }
            };
            var network = new vis.Network(container, data, options);
        }
    </script>
    <style type="text/css">
        .auto-style3 {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Manejo de Grafo</h2>
        <div>
            <asp:TextBox ID="TextBox8" runat="server" CssClass="auto-style3" Width="562px" Height="80px"></asp:TextBox>
            <br /><br /><br />
            <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style3" Width="300px" placeholder="Nombre Ciudad"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="auto-style3" Width="100px" placeholder="Habitantes"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="auto-style3" Width="100px" placeholder="Superficie (km²)"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Agregar Ciudad" CssClass="auto-style1" OnClick="Button1_Click" />
        </div>
        <br />
        <div>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="auto-style3" Width="300px" placeholder="Origen"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" CssClass="auto-style3" Width="300px" placeholder="Destino"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server" CssClass="auto-style3" Width="100px" placeholder="Costo"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Agregar Arista" OnClick="Button2_Click" />
        </div>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="auto-style2" Width="523px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        Aristas del vértice seleccionado<br />
        <asp:Button ID="Button3" runat="server" Text="Mostrar Aristas" OnClick="Button3_Click" />
        <br /><br />
        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="auto-style4" Width="650px">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="auto-style5" Width="649px">
        </asp:DropDownList>
        <br />
        <asp:TextBox ID="TextBox7" runat="server" CssClass="auto-style3" Width="562px"></asp:TextBox>
        <br /><br /><br />
        <asp:Button ID="Button4" runat="server" Text="Recorrido DFS" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Recorrido BFS" OnClick="Button5_Click" />
         <br /><br />
        <asp:TextBox ID="TextBox11" runat="server" CssClass="auto-style3" Width="562px" Height="80px"></asp:TextBox>
        <br /><br />
        <asp:Button ID="Button6" runat="server" Text="Buscar Orden Topológico" OnClick="Button6_Click" />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox9" runat="server" CssClass="auto-style3" Width="562px" ReadOnly="true" Height="56px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="auto-style5" Width="649px">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="auto-style5" Width="649px">
        </asp:DropDownList>
        <asp:Button ID="Button7" runat="server" Text="Calcular Dijkstra" OnClick="Button7_Click" />
        <br />
        <br />
        
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="Button8" runat="server" Text="Mostrar Grafo" OnClick="Button8_Click" />
        <div id="mynetwork" style="height: 500px; border: 1px solid lightgray;"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    </form>
</body>
</html>
