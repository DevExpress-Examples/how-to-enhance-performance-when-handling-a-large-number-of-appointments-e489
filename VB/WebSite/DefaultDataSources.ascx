<%@ Control Language="vb" AutoEventWireup="true" CodeFile="DefaultDataSources.ascx.vb" Inherits="DefaultDataSources" %>
<asp:ObjectDataSource ID="innerAppointmentDataSource" runat="server"  TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler" OnObjectCreated="innerAppointmentDataSource_ObjectCreated" UpdateMethod="UpdateMethodHandler" InsertMethod="InsertMethodHandler" >
	<SelectParameters>
		<asp:Parameter Name="StartDate" Type="dateTime" />
		<asp:Parameter Name="EndDate" Type="dateTime" />
	</SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="innerResourceDataSource" runat="server" TypeName="CustomResourceDataSource" SelectMethod="SelectMethodHandler" OnObjectCreated="innerResourceDataSource_ObjectCreated">
</asp:ObjectDataSource>
