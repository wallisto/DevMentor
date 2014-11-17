<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Best .NET Books</title>
    <script type="text/javascript">
    
    function getTitles()
    {
        var proxy = new develop.com.ibookservice();
        proxy.GetBookTitles(onGetTitles, onFail, null);
    }

    function onGetTitles(titles)
    {
        var list = document.getElementById("titlesList");
        for(var i = 0; i < titles.length; i++)
        {
            list[i] = new Option(titles[i].Title, titles[i].Isbn);
        }
        getBook(list[0].value);
    }

    function getBook(isbn)
    {
        var proxy = new develop.com.ibookservice();
        proxy.GetBookInfo(isbn, onGetBook, onFail, null);
        getPhoto(isbn);
        document.getElementById("msg").innerHTML = "";
    }
    
    function onGetBook(book)
    {
        document.getElementById("isbn").innerHTML = "<b>" + book.Isbn + "</b>";
        document.getElementById("category").value = book.Category;
        document.getElementById("topic").value = book.Topic;
        document.getElementById("price").value = book.Price;
        document.getElementById("authors").innerHTML = book.Authors;
        document.getElementById("url").innerHTML = book.WebSite;
        document.getElementById("url").href = book.WebSite;
    }
    
    function saveBook()
    {
        var isbn = document.getElementById("titlesList")[0].value;
        var category = document.getElementById("category").value;
        var topic = document.getElementById("topic").value;
        var price = document.getElementById("price").value;

        var book = new develop.com.bookinfo();
        book.Isbn = isbn;
        book.Category = category;
        book.Topic = topic;
        book.Price = price;
            
        var proxy = new develop.com.ibookservice()
        proxy.SaveBookInfo(book, onSaveBook, onFail, null);
    }
    
    function onSaveBook()
    {
        setLabelMsg("Book saved");
    }
    
    function getPhoto(isbn)
    {
        var url = "http://localhost/WcfAjax/Service.svc/photos/" + isbn;
        document.getElementById("bookCover").src=url;
    }
    
    function onFail(error)
    {
        setLabelError(error.get_message());
    }
    
    function setLabelMsg(text)
    {
        var msg = document.getElementById("msg");
        msg.innerHTML = "<b>Message: <font color='blue'>" + text + "</font></b>";
    }

    function setLabelError(text)
    {
        var msg = document.getElementById("msg");
        msg.innerHTML = "<b>Error: <font color='red'>" + text + "</font></b>";
    }

    </script>
    <style type="text/css">
        #titlesList
        {
            width: 296px;
        }
        .style1
        {
            width: 92px;
        }
        #Text1
        {
            width: 347px;
        }
        #Text2
        {
            width: 347px;
        }
        #Text3
        {
            width: 347px;
        }
        #Text4
        {
            width: 347px;
        }
        #Text5
        {
            width: 347px;
        }
        #Text6
        {
            width: 347px;
        }
        #category
        {
            width: 276px;
        }
        #topic
        {
            width: 276px;
        }
        #isbn
        {
            width: 276px;
        }
        #price
        {
            width: 276px;
        }
        #authors
        {
            width: 276px;
        }
        #url
        {
            width: 338px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/Service.svc" />
        </Services>
    </asp:ScriptManager>
    <div>
        <h1>The Best .NET Books</h1>
        Book Titles:&nbsp;
        <select id="titlesList" onchange="getBook(this.value)">
            <option></option>
        </select>&nbsp;
        <input id="getBtn" type="button" value="Get Books" 
            onclick="getTitles()"/><br />
        <hr />
        <table style="width: 77%;">
            <tr>
                <td class="style1">
                    Isbn:</td>
                <td>
                    <div id="isbn"></div></td>
            </tr>
            <tr>
                <td class="style1">
                    Category:</td>
                <td>
                    <input id="category" type="text" /></td>
            </tr>
            <tr>
                <td class="style1">
                    Topic:</td>
                <td>
                    <input id="topic" type="text" /></td>
            </tr>
            <tr>
                <td class="style1">
                    Price:</td>
                <td>
                    <input id="price" type="text" /></td>
            </tr>
            <tr>
                <td class="style1">
                    Authors:</td>
                <td>
                    <div id="authors"></div></td>
            </tr>
            <tr>
                <td class="style1">
                    Web Site:</td>
                <td><a id="url" href="" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    <input id="savBtn" type="button" 
                    value="Save Book" onclick="saveBook()" /></td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td><div id="msg"></div>
                </td>
            </tr>
        </table>
        <img alt="" src="" id="bookCover"/>
        </div>
    </form>
</body>
</html>
