<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="searchresults.aspx.cs" Inherits="appSchool.setup.searchresults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space32"></div>

    <div class="listbox" style="width: 840px">
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



                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkedit" Width="70px" Height="26px" ToolTip="Edit Student"
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

    <asp:HiddenField ID="hdfSearchFor" runat="server" />


</asp:Content>
