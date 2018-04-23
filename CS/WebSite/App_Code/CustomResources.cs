using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data;

#region CarsDbAdapterHelper
public class CarsDbAdapterHelper : DbAdapterHelperBase {
	#region CreateOleDbDataAdapter
	public static OleDbDataAdapter CreateOleDbDataAdapter() {
		CarsDbAdapterHelper helper = new CarsDbAdapterHelper();
		return helper.Adapter;
	}
	#endregion

	#region CreateSelectionCommand
	protected override OleDbCommand CreateSelectionCommand(OleDbConnection connection) {
		OleDbCommand command = new OleDbCommand();
		command.Connection = connection;
		command.CommandText = "SELECT ID, Model FROM Cars";
		command.CommandType = CommandType.Text;
		return command;
	}
	#endregion
}
#endregion

#region CustomResourceDataSource
public class CustomResourceDataSource {
	OleDbDataAdapter adapter;

	public CustomResourceDataSource() {
		this.adapter = CarsDbAdapterHelper.CreateOleDbDataAdapter();
	}

	public OleDbDataAdapter Adapter { get { return adapter; } }

	#region ObjectDataSource methods
	public DataTable SelectMethodHandler() {
		DataTable table = new DataTable();
		Adapter.Fill(table);
		return table;
	}
	#endregion
}
#endregion