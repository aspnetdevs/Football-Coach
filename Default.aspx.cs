using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using FootballCoach.DbEngine;
using FootballCoach.Environment;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this);
        
    }
    protected void SaveGameProcess_Click(object sender, EventArgs e)
    {
        if (GameEnvironment.IsAuthenticated)
        {
            Dictionary<string, string> processParameters = new Dictionary<string, string>();
            processParameters.Add("gameDuration", gameDurationInput.Value);
            processParameters.Add("moveDuration", moveDurationInput.Value);
            DbHelper.CreateProcess("CreatingGameProcess", gameNameInput.Value, Request.Cookies["user"].Value, new byte[0], processParameters);
            CreatingGameProcessList.DataBind();
            gameNameInput.Value = gameDurationInput.Value = moveDurationInput.Value = string.Empty;
        }
    }
}