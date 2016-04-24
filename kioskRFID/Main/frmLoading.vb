Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Code.Card
Imports System.IO
Imports System.Threading
Imports kioskRFID.Org.Mentalis.Files
Imports System.Drawing.Printing

Public Class frmLoading
    'Friend WithEvents m_reader As CodeDualiReader
    'Dim INIFName As String = Application.StartupPath & "\Kiosk.ini"
    'Dim ini As New IniReader(INIFName)
    'Public mID As String = ""
    'Dim PrintQueue As Print
    'Dim t As Thread
    'Dim strCusType As String
    'Dim dDate As Date
    'Dim printHeight As Integer

    'Public Structure Print
    '    Dim QueueNo As String
    '    Dim Mobile As String
    '    Dim WaitingTime As String
    '    Dim ListService As String
    '    Dim CountQueue As String
    'End Structure

    Private Sub pbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbClose.Click
        System.Environment.Exit(0)
    End Sub

    Private Sub frmLoading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim FormWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim FormHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Me.Size = New Size(FormWidth, FormHeight)
    End Sub


End Class
