<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.3, Version=8.3.0.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v8.3, Version=8.3.0.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v8.3.Core, Version=8.3.0.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc3" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v8.3.Core"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v8.3" Namespace="DevExpress.Web.ASPxScheduler"
	TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v8.3.Core, Version=8.3.0.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler"
	TagPrefix="cc1" %>
<%@ Register Src="~/DefaultDataSources.ascx" TagName="DefaultDataSources" TagPrefix="dds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dds:DefaultDataSources runat="server" ID="DataSource1" InitAppointments="false" />
			&nbsp; &nbsp; &nbsp;
			<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="100%" ActiveViewType="WorkWeek"
				Start="2008-09-16" ClientInstanceName="scheduler">
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
		</div>
	</form>
</body>
</html>
