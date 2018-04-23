Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Data

#Region "CarsDbAdapterHelper"
Public Class CarsDbAdapterHelper
	Inherits DbAdapterHelperBase
	#Region "CreateOleDbDataAdapter"
	Public Shared Function CreateOleDbDataAdapter() As OleDbDataAdapter
		Dim helper As New CarsDbAdapterHelper()
		Return helper.Adapter
	End Function
	#End Region

	#Region "CreateSelectionCommand"
	Protected Overrides Function CreateSelectionCommand(ByVal connection As OleDbConnection) As OleDbCommand
		Dim command As New OleDbCommand()
		command.Connection = connection
		command.CommandText = "SELECT ID, Model FROM Cars"
		command.CommandType = CommandType.Text
		Return command
	End Function
	#End Region
End Class
#End Region

#Region "CustomResourceDataSource"
Public Class CustomResourceDataSource
	Private adapter_Renamed As OleDbDataAdapter

	Public Sub New()
		Me.adapter_Renamed = CarsDbAdapterHelper.CreateOleDbDataAdapter()
	End Sub

	Public ReadOnly Property Adapter() As OleDbDataAdapter
		Get
			Return adapter_Renamed
		End Get
	End Property

	#Region "ObjectDataSource methods"
	Public Function SelectMethodHandler() As DataTable
		Dim table As New DataTable()
		Adapter.Fill(table)
		Return table
	End Function
	#End Region
End Class
#End Region