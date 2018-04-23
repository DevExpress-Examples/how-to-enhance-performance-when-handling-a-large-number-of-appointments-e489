using System;
using System.Data.OleDb;
using System.Data;

#region CarSchedulingDbAdapterHelper
public class CarSchedulingDbAdapterHelper : DbAdapterHelperBase {
	#region CreateOleDbDataAdapter
	public static OleDbDataAdapter CreateOleDbDataAdapter() {
		CarSchedulingDbAdapterHelper helper = new CarSchedulingDbAdapterHelper();
		return helper.Adapter;
	}
	#endregion
	
	#region CreateSelectionCommand
    protected override OleDbCommand CreateSelectionCommand(OleDbConnection connection) {
        OleDbCommand command = new OleDbCommand();
        command.Connection = connection;
        command.CommandText = "SELECT ID, CarId, UserId, Status, Subject, Description, Label, StartTime, EndTime," +
                "Location, AllDay, EventType, RecurrenceInfo, ReminderInfo, ContactInfo " +
                "FROM CarScheduling WHERE ((EndTime > @StartTime) AND (StartTime < @EndTime)) OR " +
                "(EventType = 1 AND StartTime < @StartTime) OR " +
                "ID IN (SELECT ID FROM CarScheduling WHERE StartTime IN (SELECT Max(StartTime) FROM CarScheduling WHERE StartTime < @StartTime GROUP BY CarId)) OR " +
                "ID IN (SELECT ID FROM CarScheduling WHERE EndTime IN (SELECT Min(EndTime) FROM CarScheduling WHERE EndTime > @EndTime GROUP BY CarId))";
        command.CommandType = CommandType.Text;
        command.Parameters.Add(new OleDbParameter("StartTime", OleDbType.Date, 0, ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "StartTime", DataRowVersion.Current, false, null));
        command.Parameters.Add(new OleDbParameter("EndTime", OleDbType.Date, 0, ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EndTime", DataRowVersion.Current, false, null));
        return command;
    }
	#endregion
	#region CreateDeleteCommand
	protected override OleDbCommand CreateDeleteCommand(OleDbConnection connection) {
		OleDbCommand command = new OleDbCommand();
		command.Connection = connection;
		command.CommandText = "DELETE FROM CarScheduling WHERE (ID = ?)";
		command.CommandType = CommandType.Text;
		command.Parameters.Add(new OleDbParameter("ID", OleDbType.Integer, 0, ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ID", DataRowVersion.Original, false, null));
		return command;
	}
	#endregion
	#region CreateUpdateCommand
	protected override OleDbCommand CreateUpdateCommand(OleDbConnection connection) {
		OleDbCommand command = new OleDbCommand();
		command.Connection = connection;
		command.CommandType = CommandType.Text;
		command.CommandText = "UPDATE CarScheduling SET Subject = @Subject, StartTime = @StartTime, EndTime = @EndTime," +
			"EventType = @EventType, AllDay = @AllDay, Status = @Status, Label = @Label, Description = @Description," +
			"Location = @Location, RecurrenceInfo = @RecurrenceInfo, ReminderInfo = @ReminderInfo, CarId = @CarId " +
			"WHERE (ID = @ID)";
		command.Parameters.Add(new OleDbParameter("@Subject", OleDbType.VarWChar));
		command.Parameters.Add(new OleDbParameter("@StartTime", OleDbType.Date));
		command.Parameters.Add(new OleDbParameter("@EndTime", OleDbType.Date));
		command.Parameters.Add(new OleDbParameter("@EventType", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@AllDay", OleDbType.Boolean));
		command.Parameters.Add(new OleDbParameter("@Status", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@Label", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@Description", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@Location", OleDbType.VarWChar));
		command.Parameters.Add(new OleDbParameter("@RecurrenceInfo", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@ReminderInfo", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@CarId", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@ID", OleDbType.Integer));
		return command;
	}
	#endregion
	#region CreateInsertCommand
	protected override OleDbCommand CreateInsertCommand(OleDbConnection connection) {
		OleDbCommand command = new OleDbCommand();
		command.Connection = connection;
		command.CommandType = CommandType.Text;
		command.CommandText = @"INSERT INTO CarScheduling (Subject, StartTime, EndTime, EventType, AllDay, Status," +
			"Label, Description, Location, RecurrenceInfo, ReminderInfo, CarId) VALUES (@Subject, @StartTime, @EndTime," +
			"@EventType, @AllDay, @Status, @Label, @Description, @Location, @RecurrenceInfo, @ReminderInfo, @CarId)";
		command.Parameters.Add(new OleDbParameter("@Subject", OleDbType.VarWChar));
		command.Parameters.Add(new OleDbParameter("@StartTime", OleDbType.Date));
		command.Parameters.Add(new OleDbParameter("@EndTime", OleDbType.Date));
		command.Parameters.Add(new OleDbParameter("@EventType", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@AllDay", OleDbType.Boolean));
		command.Parameters.Add(new OleDbParameter("@Status", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@Label", OleDbType.Integer));
		command.Parameters.Add(new OleDbParameter("@Description", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@Location", OleDbType.VarWChar));
		command.Parameters.Add(new OleDbParameter("@RecurrenceInfo", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@ReminderInfo", OleDbType.LongVarWChar));
		command.Parameters.Add(new OleDbParameter("@CarId", OleDbType.Integer));
		return command;
	}
	#endregion
}
#endregion

#region CustomEventDataSource
public class CustomEventDataSource {
	OleDbDataAdapter adapter;
	int lastInsertedAppointmentId;

	public CustomEventDataSource() {
		this.adapter = CarSchedulingDbAdapterHelper.CreateOleDbDataAdapter();
		this.lastInsertedAppointmentId = 0;
	}

	protected OleDbDataAdapter Adapter { get { return adapter; } }
	public int LastInsertedAppointmentId { get { return lastInsertedAppointmentId; } }
	
	#region ObjectDataSource methods
	#region InsertMethodHandler
	public int InsertMethodHandler(Nullable<int> eventType, Nullable<DateTime> startTime, Nullable<DateTime> endTime, bool allDay, Nullable<int> status, Nullable<int> label, string description, string location, string recurrenceInfo, string reminderInfo, Nullable<int> carId, string subject, Nullable<int> ID) {
		OleDbCommand insertCommand = Adapter.InsertCommand;
		OleDbParameterCollection parameters = insertCommand.Parameters;
		
		if (subject == null)
			parameters["@Subject"].Value = DBNull.Value;
		else
			parameters["@Subject"].Value = (string)subject;
		if (startTime.HasValue)
			parameters["@StartTime"].Value = startTime;
		else
			parameters["@StartTime"].Value = DBNull.Value;
		if (endTime.HasValue)
			parameters["@EndTime"].Value = endTime;
		else
			parameters["@EndTime"].Value = DBNull.Value;
		if (eventType.HasValue)
			parameters["@EventType"].Value = (int)eventType;
		else
			parameters["@EventType"].Value = DBNull.Value;
		parameters["@AllDay"].Value = (bool)allDay;
		if (status.HasValue)
			parameters["@Status"].Value = (int)status;
		else
			parameters["@Status"].Value = DBNull.Value;
		if (label.HasValue)
			parameters["@Label"].Value = (int)label;
		else
			parameters["@Label"].Value = DBNull.Value;
		if (description == null)
			parameters["@Description"].Value = DBNull.Value;
		else
			parameters["@Description"].Value = (string)description;
		if (location == null)
			parameters["@Location"].Value = DBNull.Value;
		else
			parameters["@Location"].Value = (string)location;
		if (recurrenceInfo == null)
			parameters["@RecurrenceInfo"].Value = DBNull.Value;
		else
			parameters["@RecurrenceInfo"].Value = (string)recurrenceInfo;
		if (reminderInfo == null)
			parameters["@ReminderInfo"].Value = DBNull.Value;
		else
			parameters["@ReminderInfo"].Value = (string)reminderInfo;
		if (carId.HasValue)
			parameters["@CarId"].Value = (int)carId;
		else
			parameters["@CarId"].Value = DBNull.Value;
		try {
			insertCommand.Connection.Open();
			insertCommand.ExecuteNonQuery();
			return GetLastInsertedAppointmentId(insertCommand.Connection);
		}
		finally {
			insertCommand.Connection.Close();
		}
	}
	#endregion
	#region DeleteMethodHandler
	public int DeleteMethodHandler(int id) {
		OleDbCommand deleteCommand = Adapter.DeleteCommand;
		deleteCommand.Parameters[0].Value = id;
		try {
			deleteCommand.Connection.Open();
			return deleteCommand.ExecuteNonQuery();
		}
		finally {
			deleteCommand.Connection.Close();
		}
	}
	#endregion
	#region UpdateMethodHandler
	public int UpdateMethodHandler(Nullable<int> EventType, Nullable<DateTime> StartTime, Nullable<DateTime> EndTime, bool AllDay, Nullable<int> Status, Nullable<int> Label, string Description, string Location, string RecurrenceInfo, string ReminderInfo, Nullable<int> CarId, string Subject, Nullable<int> ID) {
		OleDbCommand updateCommand = Adapter.UpdateCommand;
		OleDbParameterCollection parameters = updateCommand.Parameters;
		
		if (Subject == null)
			parameters["@Subject"].Value = DBNull.Value;
		else
			parameters["@Subject"].Value = (string)Subject;
		if (StartTime.HasValue)
			parameters["@StartTime"].Value = StartTime;
		else
			parameters["@StartTime"].Value = DBNull.Value;
		if (EndTime.HasValue)
			parameters["@EndTime"].Value = EndTime;
		else
			parameters["@EndTime"].Value = DBNull.Value;
		if (EventType.HasValue)
			parameters["@EventType"].Value = (int)EventType;
		else
			parameters["@EventType"].Value = DBNull.Value;
		parameters["@AllDay"].Value = (bool)AllDay;
		if (Status.HasValue)
			parameters["@Status"].Value = (int)Status;
		else
			parameters["@Status"].Value = DBNull.Value;
		if (Label.HasValue)
			parameters["@Label"].Value = (int)Label;
		else
			parameters["@Label"].Value = DBNull.Value;
		if (Description == null)
			parameters["@Description"].Value = DBNull.Value;
		else
			parameters["@Description"].Value = (string)Description;
		if (Location == null)
			parameters["@Location"].Value = DBNull.Value;
		else
			parameters["@Location"].Value = (string)Location;
		if (RecurrenceInfo == null)
			parameters["@RecurrenceInfo"].Value = DBNull.Value;
		else
			parameters["@RecurrenceInfo"].Value = (string)RecurrenceInfo;
		if (ReminderInfo == null)
			parameters["@ReminderInfo"].Value = DBNull.Value;
		else
			parameters["@ReminderInfo"].Value = (string)ReminderInfo;
		if (CarId.HasValue)
			parameters["@CarId"].Value = (int)CarId;
		else
			parameters["@CarId"].Value = DBNull.Value;
		parameters["@ID"].Value = (int)ID;
		try {
			updateCommand.Connection.Open();
			return updateCommand.ExecuteNonQuery();
		}
		finally {
			updateCommand.Connection.Close();
		}
	}
	#endregion
	#region SelectMethodHandler
	public DataTable SelectMethodHandler(DateTime startDate, DateTime endDate) {
		Adapter.SelectCommand.Parameters["StartTime"].Value = ((DateTime)(startDate));
		Adapter.SelectCommand.Parameters["EndTime"].Value = ((DateTime)(endDate));
		DataTable table = new DataTable();
		Adapter.Fill(table);
		return table;
	}
	#endregion
	#endregion
	#region GetLastInsertedAppointmentId
	int GetLastInsertedAppointmentId(OleDbConnection connection) {
		using (OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY", connection)) {
			return (int)cmd.ExecuteScalar();
		}
	}
	#endregion
}
#endregion
