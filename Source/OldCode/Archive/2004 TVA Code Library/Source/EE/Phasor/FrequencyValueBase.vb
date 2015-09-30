'*******************************************************************************************************
'  FrequencyValueBase.vb - Frequency and DfDt value base class
'  Copyright � 2005 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2003
'  Primary Developer: James R Carroll, System Analyst [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  11/12/2004 - James R Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Imports TVA.Interop

Namespace EE.Phasor

    ' This class represents the protocol independent a frequency and dfdt value.
    Public MustInherit Class FrequencyValueBase

        Inherits ChannelValueBase
        Implements IFrequencyValue

        Private m_frequency As Double
        Private m_dfdt As Double

        Protected Sub New(ByVal parent As IDataCell)

            MyBase.New(parent)

        End Sub

        ' Derived classes are expected expose a Public Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal frequency As Double, ByVal dfdt As Double)
        Protected Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal frequency As Double, ByVal dfdt As Double)

            MyBase.New(parent, frequencyDefinition)

            m_frequency = frequency
            m_dfdt = dfdt

        End Sub

        ' Derived classes are expected expose a Public Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal unscaledFrequency As Int16, ByVal unscaledDfDt As Int16)
        Protected Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal unscaledFrequency As Int16, ByVal unscaledDfDt As Int16)

            Me.New(parent, frequencyDefinition, unscaledFrequency / frequencyDefinition.ScalingFactor + frequencyDefinition.Offset, _
                unscaledDfDt / frequencyDefinition.DfDtScalingFactor + frequencyDefinition.DfDtOffset)

        End Sub

        ' Derived classes are expected expose a Public Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal binaryImage As Byte(), ByVal startIndex As Integer)
        Protected Sub New(ByVal parent As IDataCell, ByVal frequencyDefinition As IFrequencyDefinition, ByVal binaryImage As Byte(), ByVal startIndex As Integer)

            MyBase.New(parent, frequencyDefinition)

            If DataFormat = Phasor.DataFormat.FixedInteger Then
                UnscaledFrequency = EndianOrder.BigEndian.ToInt16(binaryImage, startIndex)
                UnscaledDfDt = EndianOrder.BigEndian.ToInt16(binaryImage, startIndex + 2)
            Else
                With frequencyDefinition
                    m_frequency = EndianOrder.BigEndian.ToSingle(binaryImage, startIndex) + .Offset
                    m_dfdt = EndianOrder.BigEndian.ToSingle(binaryImage, startIndex + 4) + .DfDtOffset
                End With
            End If

        End Sub

        ' Derived classes are expected to expose a Public Sub New(ByVal frequencyValue As IFrequencyValue)
        Protected Sub New(ByVal frequencyValue As IFrequencyValue)

            Me.New(frequencyValue.Parent, frequencyValue.Definition, frequencyValue.Frequency, frequencyValue.DfDt)

        End Sub

        Public Overridable Shadows Property Definition() As IFrequencyDefinition Implements IFrequencyValue.Definition
            Get
                Return MyBase.Definition
            End Get
            Set(ByVal Value As IFrequencyDefinition)
                MyBase.Definition = Value
            End Set
        End Property

        Public Overridable Property Frequency() As Double Implements IFrequencyValue.Frequency
            Get
                Return m_frequency
            End Get
            Set(ByVal Value As Double)
                m_frequency = Value
            End Set
        End Property

        Public Overridable Property DfDt() As Double Implements IFrequencyValue.DfDt
            Get
                Return m_dfdt
            End Get
            Set(ByVal Value As Double)
                m_dfdt = Value
            End Set
        End Property

        Public Overridable Property UnscaledFrequency() As Int16 Implements IFrequencyValue.UnscaledFrequency
            Get
                Try
                    With Definition
                        Return Convert.ToInt16((m_frequency - .Offset) * .ScalingFactor)
                    End With
                Catch
                    Return 0
                End Try
            End Get
            Set(ByVal Value As Int16)
                With Definition
                    m_frequency = Value / .ScalingFactor + .Offset
                End With
            End Set
        End Property

        Public Overridable Property UnscaledDfDt() As Int16 Implements IFrequencyValue.UnscaledDfDt
            Get
                Try
                    With Definition
                        Return Convert.ToInt16((m_dfdt - .DfDtOffset) * .DfDtScalingFactor)
                    End With
                Catch
                    Return 0
                End Try
            End Get
            Set(ByVal Value As Int16)
                With Definition
                    m_dfdt = Value / .DfDtScalingFactor + .DfDtOffset
                End With
            End Set
        End Property

        Public Overrides ReadOnly Property Values() As Double()
            Get
                Return New Double() {m_frequency, m_dfdt}
            End Get
        End Property

        Public Overrides ReadOnly Property IsEmpty() As Boolean
            Get
                Return (m_frequency = 0 And m_dfdt = 0)
            End Get
        End Property

        Protected Overrides ReadOnly Property BodyLength() As Int16
            Get
                If DataFormat = Phasor.DataFormat.FixedInteger Then
                    Return 4
                Else
                    Return 8
                End If
            End Get
        End Property

        Protected Overrides ReadOnly Property BodyImage() As Byte()
            Get
                Dim buffer As Byte() = Array.CreateInstance(GetType(Byte), BodyLength)

                If DataFormat = Phasor.DataFormat.FixedInteger Then
                    EndianOrder.BigEndian.CopyBytes(UnscaledFrequency, buffer, 0)
                    EndianOrder.BigEndian.CopyBytes(UnscaledDfDt, buffer, 2)
                Else
                    With Definition
                        EndianOrder.BigEndian.CopyBytes(Convert.ToSingle(m_frequency - .Offset), buffer, 0)
                        EndianOrder.BigEndian.CopyBytes(Convert.ToSingle(m_dfdt - .DfDtOffset), buffer, 4)
                    End With
                End If

                Return buffer
            End Get
        End Property

    End Class

End Namespace