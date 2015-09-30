'*******************************************************************************************************
'  AnalogValue.vb - IEEE C37.118 Analog value
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

Namespace EE.Phasor.IEEEC37_118

    Public Class AnalogValue

        Inherits AnalogValueBase

        Public Sub New(ByVal parent As IDataCell, ByVal analogDefinition As IAnalogDefinition, ByVal value As Double)

            MyBase.New(parent, analogDefinition, value)

        End Sub

        Public Sub New(ByVal parent As IDataCell, ByVal analogDefinition As IAnalogDefinition, ByVal unscaledValue As Int16)

            MyBase.New(parent, analogDefinition, unscaledValue)

        End Sub

        Public Sub New(ByVal parent As IDataCell, ByVal analogDefinition As IAnalogDefinition, ByVal binaryImage As Byte(), ByVal startIndex As Integer)

            MyBase.New(parent, analogDefinition, binaryImage, startIndex)

        End Sub

        Public Sub New(ByVal analogValue As IAnalogValue)

            MyBase.New(analogValue)

        End Sub

        Public Overrides ReadOnly Property InheritedType() As System.Type
            Get
                Return Me.GetType
            End Get
        End Property

    End Class

End Namespace