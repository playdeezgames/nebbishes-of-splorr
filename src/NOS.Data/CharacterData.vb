﻿Public Class CharacterData
    Public Property Name As String
    Public Property LocationId As Integer
    Public Property Statistics As New Dictionary(Of Integer, Integer)
    Public Property Effects As New HashSet(Of Integer)
End Class
