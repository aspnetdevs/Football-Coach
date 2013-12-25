using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FootballCoach.DbEngine;
using FootballCoach.Environment;

public partial class Main : System.Web.UI.MasterPage
{
    private HttpCookie userCookie = null;
    private HttpCookie GetNewUserCookie(string login)
    {
        HttpCookie cookie = new HttpCookie("user", login);
        cookie.Expires = DateTime.Now.AddDays(1);
        return cookie;
    }
    private bool IsEmptyFields()
    {
        bool isEmpty = loginInput.Value == string.Empty || passwordInput.Value == string.Empty;
        if (isEmpty)
            errorLoginLabel.InnerText = "Поля должны быть заполнены";
        else
            errorLoginLabel.InnerText = string.Empty;
        return isEmpty;
    }
    private void UpdatePanelFromAnotherPage(string panelId)
    {
        var updatePanel = MainPlaceHolder.FindControl(panelId) as UpdatePanel;
        if (updatePanel != null)
            updatePanel.Update();
    }
    private void Authenticate(string login)
    {
        Response.Cookies.Add(GetNewUserCookie(login));
        GameEnvironment.IsAuthenticated = true;
        UpdatePanelFromAnotherPage("GameProcessUpdatePanel");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        userCookie = Request.Cookies["user"];
        GameEnvironment.IsAuthenticated = userCookie != null;
    }
    protected void ExitButton_Click(object sender, EventArgs e)
    {
        userCookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(userCookie);
        GameEnvironment.IsAuthenticated = false;
        UpdatePanelFromAnotherPage("GameProcessUpdatePanel");
    }
    protected void EnterButton_Click(object sender, EventArgs e)
    {
        if (IsEmptyFields())
            return;
        string login = loginInput.Value;
        string password = passwordInput.Value;
        if (DbHelper.IsUser(login) && DbHelper.IsUserPassword(login, password))
            Authenticate(login);
        else
            errorLoginLabel.InnerText = "Неправильный логин или пароль";
    }
    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (IsEmptyFields())
            return;
        string login = loginInput.Value;
        string password = passwordInput.Value;
        if (!DbHelper.IsUser(login))
        {
            DbHelper.CreateUser(login, password);
            Authenticate(login);
        }
        else
            errorLoginLabel.InnerText = "Логин занят";
    }
}
