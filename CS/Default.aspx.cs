using System;
using System.Web.UI;
using System.IO;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler;
using System.Web.UI.WebControls;
using DevExpress.XtraScheduler;

public partial class _Default : System.Web.UI.Page 
{
	TimeInterval fetchInterval = TimeInterval.Empty;    
    
    protected void Page_Load(object sender, EventArgs e) {
        ASPxScheduler1.FetchAppointments += new FetchAppointmentsEventHandler(ASPxScheduler1_FetchAppointments);    
        SetDataSource(ASPxScheduler1);

            if (Request.Url.Host.Contains("devexpress"))
            {
                ASPxScheduler1.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;
                ASPxScheduler1.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;
                ASPxScheduler1.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;
            }
	}

    public void SetDataSource(ASPxScheduler control)
    {
        control.Storage.Appointments.CommitIdToDataSource = false;
        control.Storage.Appointments.AutoRetrieveId = true;
        control.ResourceDataSource = this.ResourceDataSource;
        control.AppointmentDataSource = this.AppointmentDataSource;
        control.DataBind();
    }

#region #fetchappointments	
protected void ASPxScheduler1_FetchAppointments(object sender, DevExpress.XtraScheduler.FetchAppointmentsEventArgs e) {
		if (this.fetchInterval.Contains(e.Interval) || e.Interval.Start == DateTime.MinValue)
			return;
		SetAppointmentDataSourceSelectCommandParameters(e.Interval);
		e.ForceReloadAppointments = true;
		this.fetchInterval = e.Interval;
	}
	protected void SetAppointmentDataSourceSelectCommandParameters(TimeInterval interval) {
        AppointmentDataSource.SelectParameters["StartDate"].DefaultValue = interval.Start.ToString("yyyyMMdd HH:mm:ss");
        AppointmentDataSource.SelectParameters["EndDate"].DefaultValue = interval.End.ToString("yyyyMMdd HH:mm:ss");
	}
#endregion #fetchappointments
}