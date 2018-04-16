Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub ASPxScheduler1_FetchAppointments(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.FetchAppointmentsEventArgs)
        SetAppointmentDataSourceSelectCommandParameters(e.Interval)
        e.ForceReloadAppointments = True
    End Sub
    Protected Sub SetAppointmentDataSourceSelectCommandParameters(ByVal interval As TimeInterval)
        SqlDataSource1.SelectParameters("OriginalOccurrenceStart").DefaultValue = interval.Start.ToString()
        SqlDataSource1.SelectParameters("OriginalOccurrenceEnd").DefaultValue = interval.End.ToString()
    End Sub
End Class