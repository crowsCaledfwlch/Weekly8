Class MainWindow
    Private _intSizeOfArray As Integer = 11
    Private _strSavings(_intSizeOfArray) As String
    Private _decBill(_intSizeOfArray) As Decimal
    Private Sub comboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxMonth.SelectionChanged
        If Not cbxMonth.SelectedIndex = -1 Then
            lblSavings.Content = "The electric savings for " & _strSavings(cbxMonth.SelectedIndex) & " is " & _decBill(cbxMonth.SelectedIndex).ToString("c")
            panelStuff.Visibility = Visibility.Visible
            panelIn.Visibility = Visibility.Collapsed
        Else
            panelStuff.Visibility = Visibility.Collapsed
            panelIn.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub frmLoaded(sender As Object, e As RoutedEventArgs)
        Dim objReader As IO.StringReader
        Dim strSavingsAmount As String
        Dim intCount As Integer = 0
        Dim intFill As Integer
        Try
            If My.Resources.savings.Length > 0 Then
                objReader = New IO.StringReader(My.Resources.savings)
                Do While objReader.Peek <> -1
                    _strSavings(intCount) = objReader.ReadLine()
                    strSavingsAmount = objReader.ReadLine()
                    _decBill(intCount) = Convert.ToDecimal(strSavingsAmount)
                    intCount += 1
                Loop
                objReader.Close()
                For intFill = 0 To _strSavings.Length - 1
                    cbxMonth.Items.Add(_strSavings(intFill))
                Next
            Else
                MsgBox("The file is not available, please try again when the file is available, thank you.",, "Error")
                Close()
            End If
        Catch ex As IO.IOException

        End Try
    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Close()
    End Sub

    Private Sub btnStats_Click(sender As Object, e As RoutedEventArgs) Handles btnStats.Click
        ComputeAverage()
        ComputeMaxSavings()
        panelIn.Visibility = Visibility.Visible
    End Sub

    Private Sub ComputeAverage()
        Dim CountYears As Integer
        Dim intYears As Integer = 0
        Dim decTotalBill As Decimal = 0D
        Dim decAverageAs As Decimal = 0D
        For Each CountYears In _decBill
            decTotalBill += _decBill(intYears)
            intYears += 1
        Next
        decAverageAs = decTotalBill / Convert.ToDecimal(_decBill.Length)
        lblAvgSave.Content = "The average monthly savings: " & decAverageAs.ToString("c")
    End Sub

    Private Sub ComputeMaxSavings()
        Dim intMonths As Integer
        Dim intLargest As Integer = 0
        Dim intindex As Integer = 0
        Dim strYear As String = ""
        For Each intMonths In _decBill
            intLargest = Math.Max(intLargest, intMonths)
            If intMonths >= intLargest Then
                strYear = _strSavings(intindex)
            End If
            intindex += 1
        Next
        lblBestMonth.Content = strYear & " had the most significant monthly savings."
    End Sub
End Class
