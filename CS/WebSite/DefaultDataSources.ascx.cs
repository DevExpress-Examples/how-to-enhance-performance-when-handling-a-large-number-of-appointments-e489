
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;

public partial class DefaultDataSources : System.Web.UI.UserControl {
	public ObjectDataSource AppointmentDataSource { get { return innerAppointmentDataSource; } }
	public ObjectDataSource ResourceDataSource { get { return innerResourceDataSource; } }

	public void AttachTo(ASPxScheduler control) {
		control.ResourceDataSource = this.ResourceDataSource;
		control.AppointmentDataSource = this.AppointmentDataSource;
		control.DataBind();
	}

	#region Site Mode implementation
	protected void innerAppointmentDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
		e.ObjectInstance = new CustomEventDataSource();
	}
	protected void innerResourceDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
		e.ObjectInstance = new CustomResourceDataSource();
	}
	#endregion
}
