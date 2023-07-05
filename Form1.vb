
Imports MySql.Data.MySqlClient
Imports System.Math
Imports System.Globalization


Public Class Form1


    Dim SQLcon As MySqlConnection = New MySqlConnection("SERVER=srv-bdd1;Port=3306;DATABASE=test;UID=user;PASSWORD=Cpw5pU2Q5esbAFDb")




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click




        If Button1.Text = "Connexion" Then


            Try
                If SQLcon.State = ConnectionState.Closed Then
                    SQLcon.Open()
                    Button1.Text = "Se déconnecter de la Base de Donnée"
                    Button1.BackColor = Color.Green
                Else
                    SQLcon.Close()
                    Button1.Text = "Connexion"
                    Button1.BackColor = Color.Yellow
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf Button1.Text = "Se déconnecter de la Base de Donnée" Then


            Try
                If SQLcon.State = ConnectionState.Open Then
                    SQLcon.Close()
                    Button1.Text = "Connexion"
                    Button1.BackColor = Color.Yellow
                Else
                    SQLcon.Open()
                    Button1.Text = "Se déconnecter de la Base de Donnée"
                    Button1.BackColor = Color.Green
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If SQLcon.State = ConnectionState.Open Then


            If TextBox1.Text.Trim() = "" Then
                MessageBox.Show("Entrez LE N° du Batch")
            ElseIf ComboBox2.Text.Trim() = "" Then
                MessageBox.Show("Entrez Un Canister")
            ElseIf TextBox2.Text.Trim() = "" Then
                MessageBox.Show("Entrez Le Nb de wafers")
            ElseIf ComboBox1.Text.Trim() = "" Then
                MessageBox.Show("Entrez un Visa")
            ElseIf TextBox13.Text.Trim() = "" Then
                MessageBox.Show("Entrez un Commentaire")
            ElseIf TextBox3.Text.Trim() = "" Then
                MessageBox.Show("Entrez Une Date Expiration")
            ElseIf TextBox10.Text.Trim() = "" Then
                MessageBox.Show("Entrez La Date")

            Else

                Dim cmd As New MySqlCommand("INSERT INTO `suivi_solvant_MC204` (Date, DateExpiration, Batch, Canister, NbWafer, Visa, Commentaire) VALUES(@Date,@DateExpiration,@Batch,@Canister,@NbWafer,@Visa,@Commentaire)", SQLcon)
                cmd.Parameters.AddWithValue("@Date", DateTime.Parse(TextBox10.Text).ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@DateExpiration", DateTime.Parse(TextBox3.Text).ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@Batch", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Canister", ComboBox2.Text)
                cmd.Parameters.AddWithValue("@NbWafer", TextBox2.Text)
                cmd.Parameters.AddWithValue("@Visa", ComboBox1.Text)
                cmd.Parameters.AddWithValue("@Commentaire", TextBox13.Text)

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox13.Clear()
                TextBox10.Clear()
                ComboBox1.Text = " "
                ComboBox2.Text = " "
                MessageBox.Show("Ajouté à Base de Donnée")

            End If

        Else
            MessageBox.Show("La connexion n'est pas ouverte!", "erreur")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If SQLcon.State = ConnectionState.Open Then
            ListView1.Items.Clear()
            Button3.BackColor = Color.Green
            Dim cmd As New MySqlCommand("SELECT * FROM `suivi_solvant_MC204`", SQLcon)
            Using L As MySqlDataReader = cmd.ExecuteReader()

                While L.Read()
                    Dim dates As String = L("Date").ToString()
                    Dim datesexpiration As String = L("DateExpiration").ToString()
                    Dim batch As String = L("Batch")
                    Dim canister As String = L("Canister")
                    Dim nbwafer As String = L("NbWafer")
                    Dim visa As String = L("Visa")
                    Dim commentaire As String = L("Commentaire")


                    ListView1.Items.Add(New ListViewItem(New String() {dates, datesexpiration, batch, canister, nbwafer, visa, commentaire}))
                End While

            End Using
        ElseIf SQLcon.State = ConnectionState.Closed Then
            ListView1.Items.Clear()
            Button3.BackColor = Color.Yellow
            TextBox1.Text = " "
            TextBox2.Text = " "
            TextBox3.Text = " "
            TextBox10.Text = " "
            TextBox13.Text = " "
            ComboBox1.Text = " "
            ComboBox2.Text = " "

        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If SQLcon.State = ConnectionState.Open Then
            ListView1.Items.Clear()
            Button13.BackColor = Color.Green

            Dim cmd As New MySqlCommand("SELECT * FROM `suivi_solvant_MC204`where`Date` between' " + DateTimePicker4.Value.ToString("yyyy-MM-dd 00:00:00") + "' AND '" + DateTimePicker3.Value.ToString("yyyy-MM-dd 23:59:59") + "'", SQLcon)

            Using L As MySqlDataReader = cmd.ExecuteReader()

                While L.Read()
                    Dim dates As String = L("Date").ToString()
                    Dim datesexpiration As String = L("DateExpiration").ToString()
                    Dim batch As String = L("Batch")
                    Dim canister As String = L("Canister")
                    Dim nbwafer As String = L("NbWafer")
                    Dim visa As String = L("Visa")
                    Dim commentaire As String = L("Commentaire")


                    ListView1.Items.Add(New ListViewItem(New String() {dates, datesexpiration, batch, canister, nbwafer, visa, commentaire}))



                End While

            End Using



        ElseIf SQLcon.State = ConnectionState.Closed Then
            ListView1.Items.Clear()
            Button13.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        TextBox10.Text = DateTimePicker1.Value
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        TextBox3.Text = DateTimePicker2.Value
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count.ToString("N2") Then
            TextBox10.Text = ListView1.SelectedItems(0).SubItems(0).Text
            TextBox3.Text = ListView1.SelectedItems(0).SubItems(1).Text
            TextBox1.Text = ListView1.SelectedItems(0).SubItems(2).Text
            ComboBox2.Text = ListView1.SelectedItems(0).SubItems(3).Text
            TextBox2.Text = ListView1.SelectedItems(0).SubItems(4).Text
            ComboBox1.Text = ListView1.SelectedItems(0).SubItems(5).Text
            TextBox13.Text = ListView1.SelectedItems(0).SubItems(6).Text
        End If
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        TextBox1.Text = " "
        TextBox2.Text = " "
        TextBox3.Text = " "
        TextBox10.Text = " "
        TextBox13.Text = " "
        ComboBox1.Text = " "
        ComboBox2.Text = " "
    End Sub
End Class
