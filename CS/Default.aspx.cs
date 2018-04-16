using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ASPxScheduler1_FetchAppointments(object sender, DevExpress.XtraScheduler.FetchAppointmentsEventArgs e)
    {
        SetAppointmentDataSourceSelectCommandParameters(e.Interval);
        e.ForceReloadAppointments = true;
    }
    protected void SetAppointmentDataSourceSelectCommandParameters(TimeInterval interval)
    {
        SqlDataSource1.SelectParameters["OriginalOccurrenceStart"].DefaultValue = interval.Start.ToString();
        SqlDataSource1.SelectParameters["OriginalOccurrenceEnd"].DefaultValue = interval.End.ToString();
    }
}