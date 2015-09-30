Namespace Scheduling

    Partial Class ScheduleManager
        Inherits System.ComponentModel.Component

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New(ByVal Container As System.ComponentModel.IContainer)
            MyClass.New()

            'Required for Windows.Forms Class Composition Designer support
            Container.Add(Me)

        End Sub

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

            m_enabled = DefaultEnabled
            m_persistSettings = DefaultPersistSettings
            m_settingsCategoryName = DefaultSettingsCategoryName

            m_timer = New System.Timers.Timer(60000)
            m_schedules = New List(Of Schedule)()
            m_scheduleDueEventHandlerList = New List(Of EventHandler(Of ScheduleEventArgs))()

        End Sub

        'Component overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            [Stop]()        ' Stop the schedule manager.
            SaveSettings()  ' Saves settings to the config file.
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

    End Class

End Namespace