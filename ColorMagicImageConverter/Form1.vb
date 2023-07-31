Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(openFileDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PictureBox1.Image IsNot Nothing Then
            Dim Image As Bitmap = ConvertImage(CType(PictureBox1.Image, Bitmap))
            PictureBox1.Image = Image
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If PictureBox1.Image IsNot Nothing Then
            Dim ConverterImage As Bitmap = ConvertImage(CType(PictureBox1.Image, Bitmap))
            SaveConverted("test.bmp", ConverterImage)
        End If
    End Sub

    Private Function ConvertImage(originalImage As Bitmap) As Bitmap
        Dim newImage As New Bitmap(originalImage.Width, originalImage.Height)

        Dim partWidth As Integer = originalImage.Width \ 5

        For y As Integer = 0 To originalImage.Height - 1
            For x As Integer = 0 To originalImage.Width - 1
                Dim partIndex As Integer = x \ partWidth ' Determine the part index based on the x-coordinate

                Dim originalColor As Color = originalImage.GetPixel(x, y)
                Dim newColor As Color

                Select Case partIndex
                    Case 0
                        newColor = Color.FromArgb(originalColor.A, originalColor.R, 165, 0)
                    Case 1
                        newColor = Color.FromArgb(originalColor.A, originalColor.R, 192, 203)
                    Case 2
                        newColor = Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, 102)
                    Case 3
                        newColor = Color.FromArgb(originalColor.A, originalColor.R, 69, 0)
                    Case 4
                        newColor = Color.FromArgb(originalColor.A, 221, 160, originalColor.B) ' Opacity (alpha) set to 128 (0.5 * 255)
                    Case Else
                        newColor = originalColor
                End Select

                newImage.SetPixel(x, y, newColor)
            Next
        Next

        Dim watermarkText As String = "M. Haikal Irhamna"
        Dim watermarkFont As New Font("Arial", 16)
        Dim watermarkBrush As New SolidBrush(Color.White)
        Dim watermarkPosition As New PointF(10, 10)

        Using watermarkGraphics As Graphics = Graphics.FromImage(newImage)
            watermarkGraphics.DrawString(watermarkText, watermarkFont, watermarkBrush, watermarkPosition)
        End Using

        Return newImage
    End Function
    Private Sub SaveConverted(filename As String, convertedImage As Bitmap)
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Bitmap Image|*.bmp"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            convertedImage.Save(saveFileDialog.FileName, Imaging.ImageFormat.Bmp)
        End If
    End Sub


End Class
