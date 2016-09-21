<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="courseclass.aspx.cs" Inherits="appSchool.setup.courseclass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script type='text/javascript'>

        $(document).ready(function () {
            $(".datePicker").datepicker({ dateFormat: 'dd M yy' });
        });

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="server">

    <div class="space16"></div>

    <div class="dialogbox" style="width: 740px;">
        <div class="dialogtitlebar">
            <table style="width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="h1">CLASS</td>
                    <td style="text-align: right; width: 154px">
                        <asp:DropDownList ID="cboActive" Width="150px" CssClass="combobox" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="cboActive_SelectedIndexChanged">
                            <asp:ListItem Text="All Classes" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Open Classes" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Closed Classes" Value="3"></asp:ListItem>
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
                                <td class="labelright width120">Course</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="cboCourse" CssClass="combobox" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Teacher</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="cboTeacher" CssClass="combobox" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Start Date</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtStartDate" CssClass="textbox datePicker" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">End Date</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtEndDate" CssClass="textbox datePicker" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Course Day</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtCourseDay" CssClass="textbox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td class="labelright width120">Start Time</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="cboStartTime" CssClass="combobox" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">End Time</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="cboEndTime" CssClass="combobox" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Course Open</td>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="chkIsOpen" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="labelright width120">Completed</td>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="chkIsComplete" runat="server" />
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

                <asp:GridView ID="grdCourseClass" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    GridLines="None" CssClass="grid" Width="100%" OnRowCommand="grdCourseClass_RowCommand">
                    <RowStyle BackColor="#ffffff" HorizontalAlign="Left" Height="26px" />
                    <SelectedRowStyle BackColor="#DCE6F4" Font-Bold="false" ForeColor="#585858" />
                    <HeaderStyle CssClass="gridheader" />

                    <Columns>
                        <asp:BoundField DataField="CourseClassId">
                            <HeaderStyle CssClass="griddisplaynone" />
                            <ItemStyle CssClass="griddisplaynone" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CourseName" HeaderText="Course">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TeacherName" HeaderText="Teacher">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="StartDate" HeaderText="Start" DataFormatString="{0:dd MMM yyyy}">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="EndDate" HeaderText="End" DataFormatString="{0:dd MMM yyyy}">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <asp:BoundField DataField="StartTimeLabel" HeaderText="Start Time">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>


                        <asp:BoundField DataField="EndTimeLabel" HeaderText="End Time">
                            <HeaderStyle CssClass="gridheader" />
                            <ItemStyle CssClass="gridrow" />
                        </asp:BoundField>

                        <%--   <asp:TemplateField ShowHeader="False" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove CourseClass" runat="server"
                                CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to remove Class?');" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridheadercenter" />
                        <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                    </asp:TemplateField>--%>

                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Remove" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkremove" Width="70px" Height="26px" ToolTip="Remove Course Class"
                                    runat="server" CssClass="removeimage" CausesValidation="False" CommandName="RemoveRow"
                                    CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridheadercenter" />
                            <ItemStyle CssClass="gridrowcenter" Width="70px"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkedit" Width="70px" Height="26px" ToolTip="Edit CourseClass"
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

    <asp:HiddenField ID="hdfCourseClassId" Visible="false" runat="server" />

</asp:Content>
