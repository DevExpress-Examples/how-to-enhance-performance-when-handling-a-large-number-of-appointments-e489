Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler

Partial Public Class DefaultDataSources
	Inherits System.Web.UI.UserControl
	Public ReadOnly Property AppointmentDataSource() As ObjectDataSource
		Get
			Return innerAppointmentDataSource
		End Get
	End Property
	Public ReadOnly Property ResourceDataSource() As ObjectDataSource
		Get
			Return innerResourceDataSource
		End Get
	End Property

	Public Sub AttachTo(ByVal control As ASPxScheduler)
		control.ResourceDataSource = Me.ResourceDataSource
		control.AppointmentDataSource = Me.AppointmentDataSource
		control.DataBind()
	End Sub

	#Region "Site Mode implementation"
	Protected Sub innerAppointmentDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		e.ObjectInstance = New CustomEventDataSource()
	End Sub
	Protected Sub innerResourceDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		e.ObjectInstance = New CustomResourceDataSource()
	End Sub
	#End Region
End Class
