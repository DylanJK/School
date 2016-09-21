<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="enrollment.aspx.cs" Inherits="appSchool.setup.enrollment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>

    <div style="width: 740px; margin: 0px auto; text-align: right;">
        Available Classes: &nbsp; &nbsp;
        <asp:DropDownList ID="cboClasses" CssClass="combobox" Width="300px" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="cboClasses_SelectedIndexChanged">
        </asp:DropDownList>

    </div>

    <div class="space16"></div>

    <div class="placeholder" style="width: 764px;">

        <div class="listbox" style="width: 740px">
            <div class="dialogpanel">

                <div class="space8"></div>
                <div class="h1black">ENROLLED STUDENTS</div>
                <div class="space12"></div>
                <asp:GridView ID="grdEnrollment" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdEnrollment_RowCommand">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                    <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="false" ForeColor="#585858" />
                    <HeaderStyle CssClass="gridheader" />

                    <Columns>
                        <asp:BoundField DataField="EnrollmentId">
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

                        <asp:BoundField DataField="CourseName" HeaderText="Course">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Enrollment" runat="server"
                                    CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                    CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to remove Class?');" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

    <div class="space16"></div>
    <div class="space8"></div>
    <div style="text-align: center;">
        <asp:Label ID="lblError" CssClass="errormessage" runat="server" Width="740px" Visible="false" Text=""></asp:Label>
    </div>
    <div class="space8"></div>

    <div class="placeholder" style="width: 764px;">


        <div class="listbox" style="width: 740px">

            <table style="width: 100%; padding: 8px 8px 0px 8px;">
                <tr>
                    <td class="h1black">STUDENTS    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="btnAddStudent" CssClass="button" Width="120px" runat="server" Text="Add Students" OnClick="btnAddStudent_Click" />
                    </td>
                </tr>
            </table>


            <div class="dialogpanel">

                <asp:GridView ID="grdStudent" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdStudent_RowCommand">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                    <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="false" ForeColor="#585858" />
                    <HeaderStyle CssClass="gridheader" />

                    <Columns>
                        <asp:BoundField DataField="StudentId">
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

                        <asp:BoundField DataField="Email" HeaderText="Email">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>



                        <%--  <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkselect" Width="70px" Height="26px" ToolTip="Select Student"
                                runat="server" CssClass="confirmimage" CausesValidation="False" CommandName="SelectRow"
                                CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%--<asp:Timer ID="timError" Interval="5000" runat="server" Enabled="False" OnTick="timError_Tick"></asp:Timer>--%>

    <asp:HiddenField ID="hdfEnrollmentId" Visible="false" runat="server" />

</asp:Content>
