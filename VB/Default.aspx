<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v13.1.Core, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc3" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v13.1.Core"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v13.1" Namespace="DevExpress.Web.ASPxScheduler"
	TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v13.1.Core, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler"
	TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="100%" ActiveViewType="WorkWeek"
				Start="2008-09-15" ClientInstanceName="scheduler" ClientIDMode="AutoID" >
				<Storage>
					<Appointments>
						<Mappings AllDay="AllDay" AppointmentId="UniqueID" Description="Description" End="EndDate" Label="Label" Location="Location" RecurrenceInfo="RecurrenceInfo" ReminderInfo="ReminderInfo" ResourceId="ResourceID" Start="StartDate" Status="Status" Subject="Subject" Type="Type" />
					</Appointments>
					<Resources>
						<Mappings Caption="ResourceName" Color="Color" Image="Image" ResourceId="ResourceID" />
					</Resources>
				</Storage>
				<Views>
<DayView DayCount="3" ResourcesPerPage="3"><TimeRulers>
<cc3:TimeRuler></cc3:TimeRuler>
</TimeRulers>
</DayView>

<WorkWeekView DayCount="5" ResourcesPerPage="3"><TimeRulers>
<cc3:TimeRuler></cc3:TimeRuler>
</TimeRulers>
</WorkWeekView>

<MonthView ResourcesPerPage="3"></MonthView>

<TimelineView ResourcesPerPage="3"></TimelineView>
</Views>
			</dxwschs:ASPxScheduler>
			<asp:SqlDataSource ID="ResourceDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:CarSchedulingConnectionString %>" SelectCommand="SELECT * FROM [Resources]"></asp:SqlDataSource>
			<asp:SqlDataSource ID="AppointmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:CarSchedulingConnectionString %>" 
				DeleteCommand="DELETE FROM [Appointments] WHERE [UniqueID] = @UniqueID" InsertCommand="INSERT INTO [Appointments] ([Type], [StartDate], [EndDate], [AllDay], [Subject], [Location], [Description], [Status], [Label], [ResourceID], [ResourceIDs], [ReminderInfo], [RecurrenceInfo], [CustomField1]) VALUES (@Type, @StartDate, @EndDate, @AllDay, @Subject, @Location, @Description, @Status, @Label, @ResourceID, @ResourceIDs, @ReminderInfo, @RecurrenceInfo, @CustomField1)" 
				SelectCommand="SELECT [UniqueID], [Type], [StartDate], [EndDate], [AllDay], [Subject], [Location], [Description], [Status], [Label], [ResourceID], [ResourceIDs], [ReminderInfo], [RecurrenceInfo], [CustomField1] FROM [Appointments] WHERE (([EndDate] > @StartDate) AND ([StartDate] < @EndDate)) OR ([Type] = 1 AND [StartDate] < @StartDate) OR [UniqueID] IN (SELECT [UniqueID] FROM [Appointments] WHERE [StartDate] IN (SELECT Max([StartDate]) FROM [Appointments] WHERE [StartDate] < @StartDate GROUP BY [ResourceID])) OR [UniqueID] IN (SELECT [UniqueID] FROM [Appointments] WHERE [EndDate] IN (SELECT Min([EndDate]) FROM [Appointments] WHERE [EndDate] > @EndDate GROUP BY [ResourceID]))" 
				UpdateCommand="UPDATE [Appointments] SET [Type] = @Type, [StartDate] = @StartDate, [EndDate] = @EndDate, [AllDay] = @AllDay, [Subject] = @Subject, [Location] = @Location, [Description] = @Description, [Status] = @Status, [Label] = @Label, [ResourceID] = @ResourceID, [ResourceIDs] = @ResourceIDs, [ReminderInfo] = @ReminderInfo, [RecurrenceInfo] = @RecurrenceInfo, [CustomField1] = @CustomField1 WHERE [UniqueID] = @UniqueID">
				<DeleteParameters>
					<asp:Parameter Name="UniqueID" Type="Int32" />
				</DeleteParameters>
				<InsertParameters>
					<asp:Parameter Name="Type" Type="Int32" />
					<asp:Parameter Name="StartDate" Type="DateTime" />
					<asp:Parameter Name="EndDate" Type="DateTime" />
					<asp:Parameter Name="AllDay" Type="Boolean" />
					<asp:Parameter Name="Subject" Type="String" />
					<asp:Parameter Name="Location" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
					<asp:Parameter Name="Status" Type="Int32" />
					<asp:Parameter Name="Label" Type="Int32" />
					<asp:Parameter Name="ResourceID" Type="Int32" />
					<asp:Parameter Name="ResourceIDs" Type="String" />
					<asp:Parameter Name="ReminderInfo" Type="String" />
					<asp:Parameter Name="RecurrenceInfo" Type="String" />
					<asp:Parameter Name="CustomField1" Type="String" />
				</InsertParameters>
				<SelectParameters>
					<asp:Parameter Name="StartDate" />
					<asp:Parameter Name="EndDate" />
				</SelectParameters>
				<UpdateParameters>
					<asp:Parameter Name="Type" Type="Int32" />
					<asp:Parameter Name="StartDate" Type="DateTime" />
					<asp:Parameter Name="EndDate" Type="DateTime" />
					<asp:Parameter Name="AllDay" Type="Boolean" />
					<asp:Parameter Name="Subject" Type="String" />
					<asp:Parameter Name="Location" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
					<asp:Parameter Name="Status" Type="Int32" />
					<asp:Parameter Name="Label" Type="Int32" />
					<asp:Parameter Name="ResourceID" Type="Int32" />
					<asp:Parameter Name="ResourceIDs" Type="String" />
					<asp:Parameter Name="ReminderInfo" Type="String" />
					<asp:Parameter Name="RecurrenceInfo" Type="String" />
					<asp:Parameter Name="CustomField1" Type="String" />
					<asp:Parameter Name="UniqueID" Type="Int32" />
				</UpdateParameters>
			</asp:SqlDataSource>
		</div>
	</form>
</body>
</html>