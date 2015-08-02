<%@ Page Language="C#" AutoEventWireup="true" CodeFile="page1.aspx.cs" Inherits="page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .hide {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divData" runat="server">
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                <br />
                <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false"
                    PageSize="10"
                    AllowPaging="true"
                    OnPageIndexChanging="grdData_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID">
                            <ItemStyle CssClass="hide" />
                            <HeaderStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeptId" HeaderText="DeptID">
                            <ItemStyle CssClass="hide" />
                            <HeaderStyle CssClass="hide" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Active" HeaderText="Active" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="  Edit  " 
                                    OnClick="lnkEdit_Click"
                                    CommandArgument = '<%# Eval("Id")%>'
                                ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="  Delete  " 
                                    OnClientClick="return fnConfirm();"  OnClick="lnkDelete_Click"
                                    CommandArgument = '<%# Eval("Id")%>'
                                ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                     
            </div>
            <div id="divAdd"  runat="server" visible="false"> 
                Name -
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
                <br />
                Department -
                <asp:DropDownList ID="drpDepartment" runat="server"></asp:DropDownList>
                <br />
                Active - <asp:RadioButtonList ID="OptActive" runat="server">
                             <asp:ListItem Text="Yes" Value="true" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="No" Value="false"></asp:ListItem>
                         </asp:RadioButtonList>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"  />
            </div>

            <asp:LinkButton ID="lnkWangle" runat="server"></asp:LinkButton>
        </div>

        <script type="text/javascript">
            function fnConfirm() {
                if (confirm("Are you sure want to delete this record?"))
                    return true;
                else return false;
            }
             
        </script>
    </form>
</body>
</html>
