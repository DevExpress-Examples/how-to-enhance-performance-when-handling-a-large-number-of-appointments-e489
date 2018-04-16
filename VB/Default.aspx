<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<%@ Register Assembly="DevExpress.XtraScheduler.v17.1.Core, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="800px" AppointmentDataSourceID="SqlDataSource1" ClientIDMode="AutoID"
                Start="2017-12-22" ResourceDataSourceID="SqlDataSource2" GroupType="Resource" OnFetchAppointments="ASPxScheduler1_FetchAppointments">
                <Storage>
                    <Appointments AutoRetrieveId="True">
                        <Mappings AllDay="AllDay" AppointmentId="UniqueID" Description="Description" End="EndDate" Label="Label" Location="Location" RecurrenceInfo="RecurrenceInfo" ReminderInfo="ReminderInfo" ResourceId="ResourceID" Start="StartDate" Status="Status" Subject="Subject" Type="Type" OriginalOccurrenceEnd="OriginalOccurrenceEnd" OriginalOccurrenceStart="OriginalOccurrenceStart" />
                        <CustomFieldMappings>
                            <dxwschs:ASPxAppointmentCustomFieldMapping Member="CustomField1" Name="CustomField1" />
                        </CustomFieldMappings>
                    </Appointments>
                    <Resources>
                        <Mappings Caption="ResourceName" Color="Color" Image="Image" ResourceId="ResourceID" />
                        <CustomFieldMappings>
                            <dxwschs:ASPxResourceCustomFieldMapping Member="CustomField1" Name="CustomField1" />
                        </CustomFieldMappings>
                    </Resources>
                </Storage>
                <Views>
                    <DayView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                        <DayViewStyles ScrollAreaHeight="600px"></DayViewStyles>
                    </DayView>
                    <WorkWeekView>
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </WorkWeekView>
                    <WeekView Enabled="false"></WeekView>
                    <MonthView></MonthView>
                    <TimelineView></TimelineView>
                    <FullWeekView Enabled="true">
                        <TimeRulers>
                            <cc1:TimeRuler></cc1:TimeRuler>
                        </TimeRulers>
                        <AppointmentDisplayOptions ColumnPadding-Left="2" ColumnPadding-Right="4"></AppointmentDisplayOptions>
                    </FullWeekView>
                    <AgendaView></AgendaView>
                </Views>
                <OptionsToolTips AppointmentToolTipCornerType="None"></OptionsToolTips>
            </dxwschs:ASPxScheduler>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TestConnectionString %>"
                SelectCommand="SELECT * FROM [Appointments] WHERE ((OriginalOccurrenceStart >= @OriginalOccurrenceStart) AND (OriginalOccurrenceEnd <= @OriginalOccurrenceEnd)) OR ((OriginalOccurrenceStart >= @OriginalOccurrenceStart) AND (OriginalOccurrenceStart <= @OriginalOccurrenceEnd)) OR ((OriginalOccurrenceEnd >= @OriginalOccurrenceStart) AND (OriginalOccurrenceEnd <= @OriginalOccurrenceEnd)) OR ((OriginalOccurrenceStart < @OriginalOccurrenceStart) AND (OriginalOccurrenceEnd > @OriginalOccurrenceEnd)) OR (Type <> 0)"
                DeleteCommand="DELETE FROM [Appointments] WHERE [UniqueID] = @UniqueID"
                InsertCommand="INSERT INTO [Appointments] ([Type], [StartDate], [EndDate], [AllDay], [Subject], [Location], [Description], [Status], [Label], [ResourceID], [ResourceIDs], [ReminderInfo], [RecurrenceInfo], [CustomField1], [OriginalOccurrenceStart], [OriginalOccurrenceEnd]) VALUES (@Type, @StartDate, @EndDate, @AllDay, @Subject, @Location, @Description, @Status, @Label, @ResourceID, @ResourceIDs, @ReminderInfo, @RecurrenceInfo, @CustomField1, @OriginalOccurrenceStart, @OriginalOccurrenceEnd)"
                UpdateCommand="UPDATE [Appointments] SET [Type] = @Type, [StartDate] = @StartDate, [EndDate] = @EndDate, [AllDay] = @AllDay, [Subject] = @Subject, [Location] = @Location, [Description] = @Description, [Status] = @Status, [Label] = @Label, [ResourceID] = @ResourceID, [ResourceIDs] = @ResourceIDs, [ReminderInfo] = @ReminderInfo, [RecurrenceInfo] = @RecurrenceInfo, [CustomField1] = @CustomField1, [OriginalOccurrenceStart] = @OriginalOccurrenceStart, [OriginalOccurrenceEnd] = @OriginalOccurrenceEnd WHERE [UniqueID] = @UniqueID">
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
                    <asp:Parameter Name="OriginalOccurrenceStart" Type="DateTime" />
                    <asp:Parameter Name="OriginalOccurrenceEnd" Type="DateTime" />
                </InsertParameters>
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
                    <asp:Parameter Name="OriginalOccurrenceStart" Type="DateTime" />
                    <asp:Parameter Name="OriginalOccurrenceEnd" Type="DateTime" />
                    <asp:Parameter Name="UniqueID" Type="Int32" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:Parameter Name="OriginalOccurrenceStart" Type="DateTime" />
                    <asp:Parameter Name="OriginalOccurrenceEnd" Type="DateTime" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TestConnectionString %>"
                SelectCommand="SELECT * FROM [Resources]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>