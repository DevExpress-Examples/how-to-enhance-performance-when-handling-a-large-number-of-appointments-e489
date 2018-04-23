Imports Microsoft.VisualBasic
Imports System
Imports System.Data.OleDb
Imports System.Data

#Region "CarSchedulingDbAdapterHelper"
Public Class CarSchedulingDbAdapterHelper
	Inherits DbAdapterHelperBase
	#Region "CreateOleDbDataAdapter"
	Public Shared Function CreateOleDbDataAdapter() As OleDbDataAdapter
		Dim helper As New CarSchedulingDbAdapterHelper()
		Return helper.Adapter
	End Function
	#End Region

	#Region "CreateSelectionCommand"
	Protected Overrides Function CreateSelectionCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Dim command As New OleDbCommand()
		command.Connection = connection
		command.CommandText = "SELECT ID, CarId, UserId, Status, Subject, Description, Label, StartTime, EndTime," & "Location, AllDay, EventType, RecurrenceInfo, ReminderInfo, ContactInfo " & "FROM CarScheduling WHERE ((EndTime > @StartTime) AND (StartTime < @EndTime)) OR " & "(EventType = 1 AND StartTime < @StartTime) OR " & "ID IN (SELECT ID FROM CarScheduling WHERE StartTime IN (SELECT Max(StartTime) FROM CarScheduling WHERE StartTime < @StartTime GROUP BY CarId)) OR " & "ID IN (SELECT ID FROM CarScheduling WHERE EndTime IN (SELECT Min(EndTime) FROM CarScheduling WHERE EndTime > @EndTime GROUP BY CarId))"
		command.CommandType = CommandType.Text
		command.Parameters.Add(New OleDbParameter("StartTime", OleDbType.Date, 0, ParameterDirection.Input, (CByte(0)), (CByte(0)), "StartTime", DataRowVersion.Current, False, Nothing))
		command.Parameters.Add(New OleDbParameter("EndTime", OleDbType.Date, 0, ParameterDirection.Input, (CByte(0)), (CByte(0)), "EndTime", DataRowVersion.Current, False, Nothing))
		Return command
	End Function
	#End Region
	#Region "CreateDeleteCommand"
	Protected Overrides Function CreateDeleteCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Dim command As New OleDbCommand()
		command.Connection = connection
		command.CommandText = "DELETE FROM CarScheduling WHERE (ID = ?)"
		command.CommandType = CommandType.Text
		command.Parameters.Add(New OleDbParameter("ID", OleDbType.Integer, 0, ParameterDirection.Input, (CByte(0)), (CByte(0)), "ID", DataRowVersion.Original, False, Nothing))
		Return command
	End Function
	#End Region
	#Region "CreateUpdateCommand"
	Protected Overrides Function CreateUpdateCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Dim command As New OleDbCommand()
		command.Connection = connection
		command.CommandType = CommandType.Text
		command.CommandText = "UPDATE CarScheduling SET Subject = @Subject, StartTime = @StartTime, EndTime = @EndTime," & "EventType = @EventType, AllDay = @AllDay, Status = @Status, Label = @Label, Description = @Description," & "Location = @Location, RecurrenceInfo = @RecurrenceInfo, ReminderInfo = @ReminderInfo, CarId = @CarId " & "WHERE (ID = @ID)"
		command.Parameters.Add(New OleDbParameter("@Subject", OleDbType.VarWChar))
		command.Parameters.Add(New OleDbParameter("@StartTime", OleDbType.Date))
		command.Parameters.Add(New OleDbParameter("@EndTime", OleDbType.Date))
		command.Parameters.Add(New OleDbParameter("@EventType", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@AllDay", OleDbType.Boolean))
		command.Parameters.Add(New OleDbParameter("@Status", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@Label", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@Description", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@Location", OleDbType.VarWChar))
		command.Parameters.Add(New OleDbParameter("@RecurrenceInfo", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@ReminderInfo", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@CarId", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@ID", OleDbType.Integer))
		Return command
	End Function
	#End Region
	#Region "CreateInsertCommand"
	Protected Overrides Function CreateInsertCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Dim command As New OleDbCommand()
		command.Connection = connection
		command.CommandType = CommandType.Text
		command.CommandText = "INSERT INTO CarScheduling (Subject, StartTime, EndTime, EventType, AllDay, Status," & "Label, Description, Location, RecurrenceInfo, ReminderInfo, CarId) VALUES (@Subject, @StartTime, @EndTime," & "@EventType, @AllDay, @Status, @Label, @Description, @Location, @RecurrenceInfo, @ReminderInfo, @CarId)"
		command.Parameters.Add(New OleDbParameter("@Subject", OleDbType.VarWChar))
		command.Parameters.Add(New OleDbParameter("@StartTime", OleDbType.Date))
		command.Parameters.Add(New OleDbParameter("@EndTime", OleDbType.Date))
		command.Parameters.Add(New OleDbParameter("@EventType", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@AllDay", OleDbType.Boolean))
		command.Parameters.Add(New OleDbParameter("@Status", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@Label", OleDbType.Integer))
		command.Parameters.Add(New OleDbParameter("@Description", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@Location", OleDbType.VarWChar))
		command.Parameters.Add(New OleDbParameter("@RecurrenceInfo", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@ReminderInfo", OleDbType.LongVarWChar))
		command.Parameters.Add(New OleDbParameter("@CarId", OleDbType.Integer))
		Return command
	End Function
	#End Region
End Class
#End Region

#Region "CustomEventDataSource"
Public Class CustomEventDataSource
	Private adapter_Renamed As OleDbDataAdapter
	Private lastInsertedAppointmentId_Renamed As Integer

	Public Sub New()
		Me.adapter_Renamed = CarSchedulingDbAdapterHelper.CreateOleDbDataAdapter()
		Me.lastInsertedAppointmentId_Renamed = 0
	End Sub

	Protected ReadOnly Property Adapter() As OleDbDataAdapter
		Get
			Return adapter_Renamed
		End Get
	End Property
	Public ReadOnly Property LastInsertedAppointmentId() As Integer
		Get
			Return lastInsertedAppointmentId_Renamed
		End Get
	End Property

	#Region "ObjectDataSource methods"
	#Region "InsertMethodHandler"
	Public Function InsertMethodHandler(ByVal eventType As Nullable(Of Integer), ByVal startTime As Nullable(Of DateTime), ByVal endTime As Nullable(Of DateTime), ByVal allDay As Boolean, ByVal status As Nullable(Of Integer), ByVal label As Nullable(Of Integer), ByVal description As String, ByVal location As String, ByVal recurrenceInfo As String, ByVal reminderInfo As String, ByVal carId As Nullable(Of Integer), ByVal subject As String, ByVal ID As Nullable(Of Integer)) As Integer
		Dim insertCommand As OleDbCommand = Adapter.InsertCommand
		Dim parameters As OleDbParameterCollection = insertCommand.Parameters

		If subject Is Nothing Then
			parameters("@Subject").Value = DBNull.Value
		Else
			parameters("@Subject").Value = CStr(subject)
		End If
		If startTime.HasValue Then
			parameters("@StartTime").Value = startTime
		Else
			parameters("@StartTime").Value = DBNull.Value
		End If
		If endTime.HasValue Then
			parameters("@EndTime").Value = endTime
		Else
			parameters("@EndTime").Value = DBNull.Value
		End If
		If eventType.HasValue Then
			parameters("@EventType").Value = CInt(Fix(eventType))
		Else
			parameters("@EventType").Value = DBNull.Value
		End If
		parameters("@AllDay").Value = CBool(allDay)
		If status.HasValue Then
			parameters("@Status").Value = CInt(Fix(status))
		Else
			parameters("@Status").Value = DBNull.Value
		End If
		If label.HasValue Then
			parameters("@Label").Value = CInt(Fix(label))
		Else
			parameters("@Label").Value = DBNull.Value
		End If
		If description Is Nothing Then
			parameters("@Description").Value = DBNull.Value
		Else
			parameters("@Description").Value = CStr(description)
		End If
		If location Is Nothing Then
			parameters("@Location").Value = DBNull.Value
		Else
			parameters("@Location").Value = CStr(location)
		End If
		If recurrenceInfo Is Nothing Then
			parameters("@RecurrenceInfo").Value = DBNull.Value
		Else
			parameters("@RecurrenceInfo").Value = CStr(recurrenceInfo)
		End If
		If reminderInfo Is Nothing Then
			parameters("@ReminderInfo").Value = DBNull.Value
		Else
			parameters("@ReminderInfo").Value = CStr(reminderInfo)
		End If
		If carId.HasValue Then
			parameters("@CarId").Value = CInt(Fix(carId))
		Else
			parameters("@CarId").Value = DBNull.Value
		End If
		Try
			insertCommand.Connection.Open()
			insertCommand.ExecuteNonQuery()
			Return GetLastInsertedAppointmentId(insertCommand.Connection)
		Finally
			insertCommand.Connection.Close()
		End Try
	End Function
	#End Region
	#Region "DeleteMethodHandler"
	Public Function DeleteMethodHandler(ByVal id As Integer) As Integer
		Dim deleteCommand As OleDbCommand = Adapter.DeleteCommand
		deleteCommand.Parameters(0).Value = id
		Try
			deleteCommand.Connection.Open()
			Return deleteCommand.ExecuteNonQuery()
		Finally
			deleteCommand.Connection.Close()
		End Try
	End Function
	#End Region
	#Region "UpdateMethodHandler"
	Public Function UpdateMethodHandler(ByVal EventType As Nullable(Of Integer), ByVal StartTime As Nullable(Of DateTime), ByVal EndTime As Nullable(Of DateTime), ByVal AllDay As Boolean, ByVal Status As Nullable(Of Integer), ByVal Label As Nullable(Of Integer), ByVal Description As String, ByVal Location As String, ByVal RecurrenceInfo As String, ByVal ReminderInfo As String, ByVal CarId As Nullable(Of Integer), ByVal Subject As String, ByVal ID As Nullable(Of Integer)) As Integer
		Dim updateCommand As OleDbCommand = Adapter.UpdateCommand
		Dim parameters As OleDbParameterCollection = updateCommand.Parameters

		If Subject Is Nothing Then
			parameters("@Subject").Value = DBNull.Value
		Else
			parameters("@Subject").Value = CStr(Subject)
		End If
		If StartTime.HasValue Then
			parameters("@StartTime").Value = StartTime
		Else
			parameters("@StartTime").Value = DBNull.Value
		End If
		If EndTime.HasValue Then
			parameters("@EndTime").Value = EndTime
		Else
			parameters("@EndTime").Value = DBNull.Value
		End If
		If EventType.HasValue Then
			parameters("@EventType").Value = CInt(Fix(EventType))
		Else
			parameters("@EventType").Value = DBNull.Value
		End If
		parameters("@AllDay").Value = CBool(AllDay)
		If Status.HasValue Then
			parameters("@Status").Value = CInt(Fix(Status))
		Else
			parameters("@Status").Value = DBNull.Value
		End If
		If Label.HasValue Then
			parameters("@Label").Value = CInt(Fix(Label))
		Else
			parameters("@Label").Value = DBNull.Value
		End If
		If Description Is Nothing Then
			parameters("@Description").Value = DBNull.Value
		Else
			parameters("@Description").Value = CStr(Description)
		End If
		If Location Is Nothing Then
			parameters("@Location").Value = DBNull.Value
		Else
			parameters("@Location").Value = CStr(Location)
		End If
		If RecurrenceInfo Is Nothing Then
			parameters("@RecurrenceInfo").Value = DBNull.Value
		Else
			parameters("@RecurrenceInfo").Value = CStr(RecurrenceInfo)
		End If
		If ReminderInfo Is Nothing Then
			parameters("@ReminderInfo").Value = DBNull.Value
		Else
			parameters("@ReminderInfo").Value = CStr(ReminderInfo)
		End If
		If CarId.HasValue Then
			parameters("@CarId").Value = CInt(Fix(CarId))
		Else
			parameters("@CarId").Value = DBNull.Value
		End If
		parameters("@ID").Value = CInt(Fix(ID))
		Try
			updateCommand.Connection.Open()
			Return updateCommand.ExecuteNonQuery()
		Finally
			updateCommand.Connection.Close()
		End Try
	End Function
	#End Region
	#Region "SelectMethodHandler"
	Public Function SelectMethodHandler(ByVal startDate As DateTime, ByVal endDate As DateTime) As DataTable
		Adapter.SelectCommand.Parameters("StartTime").Value = (CDate(startDate))
		Adapter.SelectCommand.Parameters("EndTime").Value = (CDate(endDate))
		Dim table As New DataTable()
		Adapter.Fill(table)
		Return table
	End Function
	#End Region
	#End Region
	#Region "GetLastInsertedAppointmentId"
	Private Function GetLastInsertedAppointmentId(ByVal connection As OleDbConnection) As Integer
		Using cmd As New OleDbCommand("SELECT @@IDENTITY", connection)
			Return CInt(Fix(cmd.ExecuteScalar()))
		End Using
	End Function
	#End Region
End Class
#End Region
