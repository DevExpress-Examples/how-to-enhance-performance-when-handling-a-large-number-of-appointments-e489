using System;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Configuration;


#region DataHelper
public static class DataHelper {
	public static void SetupDefaultMappings(ASPxScheduler control) {
		SetupDefaultMappingsSiteMode(control);
	}
	private static void SetupDefaultMappingsSiteMode(ASPxScheduler control) {
		ASPxSchedulerStorage storage = control.Storage;
		storage.BeginUpdate();
		try {
			ASPxResourceMappingInfo resourceMappings = storage.Resources.Mappings;
			resourceMappings.ResourceId = "ID";
			resourceMappings.Caption = "Model";

			ASPxAppointmentMappingInfo appointmentMappings = storage.Appointments.Mappings;
			appointmentMappings.AppointmentId = "ID";
			appointmentMappings.Start = "StartTime";
			appointmentMappings.End = "EndTime";
			appointmentMappings.Subject = "Subject";
			appointmentMappings.AllDay = "AllDay";
			appointmentMappings.Description = "Description";
			appointmentMappings.Label = "Label";
			appointmentMappings.Location = "Location";
			appointmentMappings.RecurrenceInfo = "RecurrenceInfo";
			appointmentMappings.ReminderInfo = "ReminderInfo";
			appointmentMappings.ResourceId = "CarId";
			appointmentMappings.Status = "Status";
			appointmentMappings.Type = "EventType";
		}
		finally {
			storage.EndUpdate();
		}
	}
	public static void ProvideRowInsertion(ASPxScheduler control, DataSourceControl dataSource) {
		ObjectDataSource objectDataSource = dataSource as ObjectDataSource;
		if (objectDataSource != null) {
			ObjectDataSourceRowInsertionProvider provider = new ObjectDataSourceRowInsertionProvider();
			provider.ProvideRowInsertion(control, objectDataSource);
		}
	}
}
#endregion
#region ObjectDataSourceRowInsertionProvider
public class ObjectDataSourceRowInsertionProvider {
	int lastInsertedAppointmentId;
	public void ProvideRowInsertion(ASPxScheduler control, ObjectDataSource dataSource) {
		dataSource.Inserted += new ObjectDataSourceStatusEventHandler(AppointmentsDataSource_Inserted);
		control.AppointmentRowInserted += new ASPxSchedulerDataInsertedEventHandler(ControlOnAppointmentRowInserted);
		control.AppointmentsInserted += new PersistentObjectsEventHandler(ControlOnAppointmentsInserted);
	}
	void ControlOnAppointmentRowInserted(object sender, ASPxSchedulerDataInsertedEventArgs e) {
		// Autoincremented primary key case
		e.KeyFieldValue = this.lastInsertedAppointmentId;
	}
	void AppointmentsDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
		// Autoincremented primary key case
		this.lastInsertedAppointmentId = (int)e.ReturnValue;
	}
	void ControlOnAppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
		//Autoincremented primary key case
		int count = e.Objects.Count;
		System.Diagnostics.Debug.Assert(count == 1);
		Appointment apt = (Appointment)e.Objects[0];
		ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
		storage.SetAppointmentId(apt, lastInsertedAppointmentId);
	}
}
#endregion
#region DbAdapterHelperBase
public class DbAdapterHelperBase {
	#region Fields
	OleDbDataAdapter adapter;
	OleDbConnection connection;
	#endregion

	protected DbAdapterHelperBase() {
		this.adapter = new OleDbDataAdapter();
		this.connection = CreateConnection();

		InitAdapter(Adapter, Connection);
	}

	#region Properties
	public OleDbDataAdapter Adapter { get { return adapter; } }
	public OleDbConnection Connection { get { return connection; } }
	#endregion

	#region CreateConnection
	protected OleDbConnection CreateConnection() {
		OleDbConnection connection = new OleDbConnection();
		connection.ConnectionString = ConfigurationManager.ConnectionStrings["SchedulerConnectionString"].ConnectionString; 
		return connection;
	}
	#endregion
	#region InitAdapter
	protected void InitAdapter(OleDbDataAdapter adapter, OleDbConnection connection) {
		adapter.SelectCommand = CreateSelectionCommand(connection);
		adapter.DeleteCommand = CreateDeleteCommand(connection);
		adapter.UpdateCommand = CreateUpdateCommand(connection);
		adapter.InsertCommand = CreateInsertCommand(connection);
	}
	#endregion
	#region CreateSelectionCommand
	protected virtual OleDbCommand CreateSelectionCommand(OleDbConnection connection) {
		return null;
	}
	#endregion
	#region CreateDeleteCommand
	protected virtual OleDbCommand CreateDeleteCommand(OleDbConnection connection) {
		return null;
	}
	#endregion
	#region CreateUpdateCommand
	protected virtual OleDbCommand CreateUpdateCommand(OleDbConnection connection) {
		return null;
	}
	#endregion
	#region CreateInsertCommand
	protected virtual OleDbCommand CreateInsertCommand(OleDbConnection connection) {
		return null;
	}
	#endregion
}
#endregion

