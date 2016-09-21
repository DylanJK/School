<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="appSchool.setup.course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>

    <div class="dialogbox" style="width: 740px;">
        <div class="dialogtitlebar">
            <table style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="h1">COURSE INFO</td>
                    <td style="text-align: right; width: 154px">
                        <asp:DropDownList ID="cboActive" Width="150px" CssClass="combobox" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="cboActive_SelectedIndexChanged">
                            <asp:ListItem Text="All Courses" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Active Courses" Value="2" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive Courses" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

            </table>

        </div>

        <div class="inputpanel">

            <div class="space8"></div>
            <table style="width: 100%; border-collapse: collapse; border-spacing: 0px;">
                <tr>
                    <td style="width: 40%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright width120">Code</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtCode" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Name</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Fee</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtFee" CssClass="textbox" runat="server"></asp:TextBox>
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
                    <td style="width: 60%; vertical-align: top;">
                        <table style="width: 100%">

                            <tr>
                                <td class="labelright width120" style="vertical-align: top;">Description</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDescription" CssClass="textbox" Width="360px" TextMode="MultiLine" Rows="6" runat="server"></asp:TextBox>
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

    <div class="space8"></div>
    <div style="text-align: center;">
        <asp:Label ID="lblError" CssClass="errormessage" runat="server" Width="840px" Visible="false" Text=""></asp:Label>
    </div>
    <div class="space8"></div>

    <div class="placeholder" style="width: 864px;">

        <div class="listbox" style="width: 840px">
            <div class="dialogpanel">

                <asp:GridView ID="grdCourse" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdCourse_RowCommand">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                    <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="false" ForeColor="#585858" />
                    <HeaderStyle CssClass="gridheader" />

                    <Columns>
                        <asp:BoundField DataField="CourseId">
                            <HeaderStyle CssClass="griddisplaynone" />
                            <ItemStyle CssClass="griddisplaynone" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Code" HeaderText="Code">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Name" HeaderText="Name">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Description" HeaderText="Description">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Fee" HeaderText="Fee" DataFormatString="{0:C}">
                            <HeaderStyle CssClass="gridheaderright" />
                            <ItemStyle CssClass="gridrowright" Width="80px" />
                        </asp:BoundField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Course" runat="server"
                                    CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                    CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to remove Course?');" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkedit" Width="70px" Height="26px" ToolTip="Edit Course"
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

    <asp:Timer ID="timError" Interval="5000" runat="server" Enabled="False" OnTick="timError_Tick"></asp:Timer>

    <asp:HiddenField ID="hdfCourseId" Visible="false" runat="server" />

</asp:Content>


