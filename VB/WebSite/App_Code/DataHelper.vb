Imports Microsoft.VisualBasic
Imports System
Imports System.Data.OleDb
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.XtraScheduler
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.Configuration


#Region "DataHelper"
Public NotInheritable Class DataHelper
	Private Sub New()
	End Sub
	Public Shared Sub SetupDefaultMappings(ByVal control As ASPxScheduler)
		SetupDefaultMappingsSiteMode(control)
	End Sub
	Private Shared Sub SetupDefaultMappingsSiteMode(ByVal control As ASPxScheduler)
		Dim storage As ASPxSchedulerStorage = control.Storage
		storage.BeginUpdate()
		Try
			Dim resourceMappings As ASPxResourceMappingInfo = storage.Resources.Mappings
			resourceMappings.ResourceId = "ID"
			resourceMappings.Caption = "Model"

			Dim appointmentMappings As ASPxAppointmentMappingInfo = storage.Appointments.Mappings
			appointmentMappings.AppointmentId = "ID"
			appointmentMappings.Start = "StartTime"
			appointmentMappings.End = "EndTime"
			appointmentMappings.Subject = "Subject"
			appointmentMappings.AllDay = "AllDay"
			appointmentMappings.Description = "Description"
			appointmentMappings.Label = "Label"
			appointmentMappings.Location = "Location"
			appointmentMappings.RecurrenceInfo = "RecurrenceInfo"
			appointmentMappings.ReminderInfo = "ReminderInfo"
			appointmentMappings.ResourceId = "CarId"
			appointmentMappings.Status = "Status"
			appointmentMappings.Type = "EventType"
		Finally
			storage.EndUpdate()
		End Try
	End Sub
	Public Shared Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As DataSourceControl)
		Dim objectDataSource As ObjectDataSource = TryCast(dataSource, ObjectDataSource)
		If objectDataSource IsNot Nothing Then
			Dim provider As New ObjectDataSourceRowInsertionProvider()
			provider.ProvideRowInsertion(control, objectDataSource)
		End If
	End Sub
End Class
#End Region
#Region "ObjectDataSourceRowInsertionProvider"
Public Class ObjectDataSourceRowInsertionProvider
	Private lastInsertedAppointmentId As Integer
	Public Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As ObjectDataSource)
		AddHandler dataSource.Inserted, AddressOf AppointmentsDataSource_Inserted
		AddHandler control.AppointmentRowInserted, AddressOf ControlOnAppointmentRowInserted
		AddHandler control.AppointmentsInserted, AddressOf ControlOnAppointmentsInserted
	End Sub
	Private Sub ControlOnAppointmentRowInserted(ByVal sender As Object, ByVal e As ASPxSchedulerDataInsertedEventArgs)
		' Autoincremented primary key case
		e.KeyFieldValue = Me.lastInsertedAppointmentId
	End Sub
	Private Sub AppointmentsDataSource_Inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs)
		' Autoincremented primary key case
		Me.lastInsertedAppointmentId = CInt(Fix(e.ReturnValue))
	End Sub
	Private Sub ControlOnAppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
		'Autoincremented primary key case
		Dim count As Integer = e.Objects.Count
		System.Diagnostics.Debug.Assert(count = 1)
		Dim apt As Appointment = CType(e.Objects(0), Appointment)
		Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
		storage.SetAppointmentId(apt, lastInsertedAppointmentId)
	End Sub
End Class
#End Region
#Region "DbAdapterHelperBase"
Public Class DbAdapterHelperBase
	#Region "Fields"
	Private adapter_Renamed As OleDbDataAdapter
	Private connection_Renamed As OleDbConnection
	#End Region

	Protected Sub New()
		Me.adapter_Renamed = New OleDbDataAdapter()
		Me.connection_Renamed = CreateConnection()

		InitAdapter(Adapter, Connection)
	End Sub

	#Region "Properties"
	Public ReadOnly Property Adapter() As OleDbDataAdapter
		Get
			Return adapter_Renamed
		End Get
	End Property
	Public ReadOnly Property Connection() As OleDbConnection
		Get
			Return connection_Renamed
		End Get
	End Property
	#End Region

	#Region "CreateConnection"
	Protected Function CreateConnection() As OleDbConnection
		Dim connection As New OleDbConnection()
		connection.ConnectionString = ConfigurationManager.ConnectionStrings("SchedulerConnectionString").ConnectionString
		Return connection
	End Function
	#End Region
	#Region "InitAdapter"
	Protected Sub InitAdapter(ByVal adapter As OleDbDataAdapter, ByVal connection As OleDbConnection)
		adapter.SelectCommand = CreateSelectionCommand(connection)
		adapter.DeleteCommand = CreateDeleteCommand(connection)
		adapter.UpdateCommand = CreateUpdateCommand(connection)
		adapter.InsertCommand = CreateInsertCommand(connection)
	End Sub
	#End Region
	#Region "CreateSelectionCommand"
	Protected Overridable Function CreateSelectionCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Return Nothing
	End Function
	#End Region
	#Region "CreateDeleteCommand"
	Protected Overridable Function CreateDeleteCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Return Nothing
	End Function
	#End Region
	#Region "CreateUpdateCommand"
	Protected Overridable Function CreateUpdateCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Return Nothing
	End Function
	#End Region
	#Region "CreateInsertCommand"
	Protected Overridable Function CreateInsertCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Return Nothing
	End Function
	#End Region
End Class
#End Region

