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
		DataHelper.SetupDefaultMappings(ASPxScheduler1);
		DataHelper.ProvideRowInsertion(ASPxScheduler1, DataSource1.AppointmentDataSource);


		this.fetchInterval = ASPxScheduler1.ActiveView.GetVisibleIntervals().Interval;
		SetAppointmentDataSourceSelectCommandParameters(this.fetchInterval);

		DataSource1.AttachTo(ASPxScheduler1);
		
		ASPxScheduler1.FetchAppointments += new FetchAppointmentsEventHandler(ASPxScheduler1_FetchAppointments);

		if (Request.Url.Host.Contains("devexpress")) 
		{
		ASPxScheduler1.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None;
		ASPxScheduler1.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None;
		ASPxScheduler1.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None;
		}

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
		DataSource1.AppointmentDataSource.SelectParameters["StartDate"].DefaultValue = interval.Start.ToString("yyyyMMdd HH:mm:ss");
		DataSource1.AppointmentDataSource.SelectParameters["EndDate"].DefaultValue = interval.End.ToString("yyyyMMdd HH:mm:ss");
	}
#endregion #fetchappointments
}