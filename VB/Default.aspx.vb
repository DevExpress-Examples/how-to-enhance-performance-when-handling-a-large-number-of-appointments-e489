Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports System.IO
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler
Imports System.Web.UI.WebControls
Imports DevExpress.XtraScheduler

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private fetchInterval As TimeInterval = TimeInterval.Empty

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		AddHandler ASPxScheduler1.FetchAppointments, AddressOf ASPxScheduler1_FetchAppointments
		SetDataSource(ASPxScheduler1)

			If Request.Url.Host.Contains("devexpress") Then
				ASPxScheduler1.OptionsCustomization.AllowAppointmentCreate = UsedAppointmentType.None
				ASPxScheduler1.OptionsCustomization.AllowAppointmentDelete = UsedAppointmentType.None
				ASPxScheduler1.OptionsCustomization.AllowAppointmentEdit = UsedAppointmentType.None
			End If
	End Sub

	Public Sub SetDataSource(ByVal control As ASPxScheduler)
		control.Storage.Appointments.CommitIdToDataSource = False
		control.Storage.Appointments.AutoRetrieveId = True
		control.ResourceDataSource = Me.ResourceDataSource
		control.AppointmentDataSource = Me.AppointmentDataSource
		control.DataBind()
	End Sub

#Region "#fetchappointments    "
Protected Sub ASPxScheduler1_FetchAppointments(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.FetchAppointmentsEventArgs)
		If Me.fetchInterval.Contains(e.Interval) OrElse e.Interval.Start = DateTime.MinValue Then
			Return
		End If
		SetAppointmentDataSourceSelectCommandParameters(e.Interval)
		e.ForceReloadAppointments = True
		Me.fetchInterval = e.Interval
End Sub
	Protected Sub SetAppointmentDataSourceSelectCommandParameters(ByVal interval As TimeInterval)
		AppointmentDataSource.SelectParameters("StartDate").DefaultValue = interval.Start.ToString("yyyyMMdd HH:mm:ss")
		AppointmentDataSource.SelectParameters("EndDate").DefaultValue = interval.End.ToString("yyyyMMdd HH:mm:ss")
	End Sub
#End Region ' #fetchappointments
End Class