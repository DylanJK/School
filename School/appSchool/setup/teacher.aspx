<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="teacher.aspx.cs" Inherits="appSchool.setup.teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>

    <div class="dialogbox" style="width: 740px;">
        <div class="dialogtitlebar">

            <table style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="h1">TEACHERS</td>
                    <td style="text-align: right; width: 154px">
                        <asp:DropDownList ID="cboActive" Width="150px" CssClass="combobox" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="cboActive_SelectedIndexChanged">
                            <asp:ListItem Text="All Teachers" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Active Teachers" Value="2" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive Teachers" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

        </div>

        <div class="inputpanel">

            <div class="space8"></div>
            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright width120">First Name</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtFirstName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Last Name</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLastName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Phone</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Mobile</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtMobile" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">

                            <tr>
                                <td class="labelright width120">Email</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Active</td>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="chkActive" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <div class="space16"></div>

            <div style="text-align: right;">
                <asp:Button ID="btnSave" CssClass="button" Width="120px" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>

    <div class="space16"></div>

    <div class="placeholder" style="width: 764px;">

        <div class="listbox" style="width: 740px">
            <div class="dialogpanel">

                <asp:GridView ID="grdTeacher" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdTeacher_RowCommand">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                    <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="false" ForeColor="#585858" />
                    <HeaderStyle CssClass="gridheader" />

                    <Columns>
                        <asp:BoundField DataField="TeacherId">
                            <HeaderStyle CssClass="griddisplaynone" />
                            <ItemStyle CssClass="griddisplaynone" />
                        </asp:BoundField>

                        <asp:BoundField DataField="FirstName" HeaderText="First Name">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="LastName" HeaderText="Last Name">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Phone" HeaderText="Phone">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>


                        <asp:BoundField DataField="Mobile" HeaderText="Mobile">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Teacher" runat="server"
                                    CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                    CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to remove Teacher?');" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkedit" Width="70px" Height="26px" ToolTip="Edit Teacher"
                                    runat="server" CssClass="editimage" CausesValidation="False" CommandName="EditRow"
                                    CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdfTeacherId" Visible="false" runat="server" />
    <asp:HiddenField ID="hdfPassword" Visible="false" runat="server" />

</asp:Content>
