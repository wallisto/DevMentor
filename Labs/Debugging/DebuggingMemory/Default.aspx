<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        Refresh this page to leak more memory</h1>
        
        <input type="submit" value="submit" />
        Current operating temperature: <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> degrees
        <br /><h2><font color="red"><asp:Label ID="Label2" runat="server" Text=""></asp:Label></font></h2>
    </form>
</body>
</html>
