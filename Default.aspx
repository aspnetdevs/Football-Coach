<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="FootballCoach.Environment" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainPlaceHolder" runat="Server">
    <div class="gameProcess">
        <asp:UpdatePanel ID="CreatingGameProcessUpdatePanel" runat="server">
            <ContentTemplate>
                <asp:ListView ID="CreatingGameProcessList" runat="server" DataSourceID="CreatingGameProcess">
                    <AlternatingItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label ID="CreatedOnLabel" runat="server" Text='<%# Eval("CreatedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ModifiedOnLabel" runat="server" Text='<%# Eval("ModifiedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server">
                            <tr>
                                <td>Нет игровых процессов</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label ID="CreatedOnLabel" runat="server" Text='<%# Eval("CreatedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ModifiedOnLabel" runat="server" Text='<%# Eval("ModifiedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server">
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th1" runat="server">CreatedOn</th>
                                            <th id="Th2" runat="server">ModifiedOn</th>
                                            <th id="Th3" runat="server">Name</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NumericPagerField />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label ID="CreatedOnLabel" runat="server" Text='<%# Eval("CreatedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ModifiedOnLabel" runat="server" Text='<%# Eval("ModifiedOn") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
                <% if (GameEnvironment.IsAuthenticated)
                   { %>
                <input id="CreateGameProcess" type="button" value="Создать игровой процесс" />
                <div id="CreateGamePanel" class="hide">
                    <input type="text" id="gameNameInput" runat="server" placeholder="Имя процесса" /><br />
                    <input type="text" id="moveDurationInput" runat="server" placeholder="Длительность хода" /><br />
                    <input type="text" id="gameDurationInput" runat="server" placeholder="Длительность игры" /><br />
                    <!--Когда появлятся команды, то позволить выбирать второй пункт-->
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Text="Своя команда" Value="newTeam" Selected="True" />
                        <asp:ListItem Text="Существующая команда" Value="existTeam" Enabled="false" />
                    </asp:RadioButtonList>
                    <asp:Button ID="SaveGameProcess" runat="server" Text="Создать" OnClick="SaveGameProcess_Click" />
                </div>
                <%} %>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:SqlDataSource ID="CreatingGameProcess" runat="server" ConnectionString="<%$ ConnectionStrings:FootballCoachConnectionString %>" SelectCommand="SELECT [CreatedOn], [ModifiedOn], [Name] FROM [CreatingGameProcess] ORDER BY [ModifiedOn], [CreatedOn], [Name]"></asp:SqlDataSource>
</asp:Content>

