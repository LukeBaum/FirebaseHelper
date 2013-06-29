Namespace Firebase

    ' Firebase Helper is a class that lets you access data that you have stored in a Firebase
    ' from managed code. While the real time benefit of Firebase can really only be experienced
    ' from javascript, it is occasionally useful to access Firebase from managed code.

    Public Class FirebaseHelper

        ' The name of the Firebase is always used in accessing it.
        Private pFirebaseName As String
        Public ReadOnly Property FirebaseName As String
            Get
                Return pFirebaseName
            End Get
        End Property

        ' The base URL for all requests is always built on the firebase name.
        Public ReadOnly Property FirebaseUrl As String
            Get
                Return "https://" & pFirebaseName & ".firebaseio.com/"
            End Get
        End Property

        Private pFirebaseSecret As String
        Public ReadOnly Property FirebaseSecret As String
            Get
                Return pFirebaseSecret
            End Get
        End Property

        Sub New(ByVal FirebaseName As String, ByVal FirebaseSecret As String)

            ' Make a new Firebase Helper for the Firebase specified.
            Me.pFirebaseName = FirebaseName
            Me.pFirebaseSecret = FirebaseSecret

        End Sub

        Public Function ReadData(ByVal NodePath As String) As String

            ' Read the data at a particular point in the Firebase.

            Try

                Dim ReadDataRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(FirebaseUrl & NodePath & ".json")

                With ReadDataRequest
                    .Method = "GET"
                End With

                Dim ReadDataResponse As Net.HttpWebResponse = ReadDataRequest.GetResponse

                Dim Reader As New IO.StreamReader(ReadDataResponse.GetResponseStream())
                Dim Result As String = Reader.ReadToEnd

                Reader.Close()
                Reader.Dispose()

                Return Result

            Catch ex As Exception

                Return Nothing

            End Try

        End Function

        Public Function WriteData(ByVal NodePath As String, ByVal JsonData As String) As WriteDataResult

            ' Write the data at a particular point in the Firebase.

            Try

                Dim WriteDataRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(FirebaseUrl & NodePath & ".json?auth=" & FirebaseSecret)

                With WriteDataRequest
                    .Method = "PUT"
                    Dim Writer As New IO.StreamWriter(.GetRequestStream)
                    Writer.Write(JsonData)
                    Writer.Close()
                    Writer.Dispose()
                End With

                Dim WriteDataResponse As Net.HttpWebResponse = WriteDataRequest.GetResponse

                Dim Reader As New IO.StreamReader(WriteDataResponse.GetResponseStream())
                Dim Result As String = Reader.ReadToEnd

                Reader.Close()
                Reader.Dispose()

                If Result = JsonData Then
                    Return WriteDataResult.DataWrittenSuccessfully
                Else
                    Return WriteDataResult.DataWriteFailed
                End If

            Catch ex As Exception

                Return WriteDataResult.DataWriteFailed

            End Try

        End Function

        Public Function PushData(ByVal NodePath As String, ByVal JsonData As String) As String

            ' Push the data at a particular point in the Firebase.
            ' Return the child name.

            Try

                Dim PushDataRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(FirebaseUrl & NodePath & ".json?auth=" & FirebaseSecret)

                With PushDataRequest
                    .Method = "POST"
                    Dim Writer As New IO.StreamWriter(.GetRequestStream)
                    Writer.Write(JsonData)
                    Writer.Close()
                    Writer.Dispose()
                End With

                Dim PushDataResponse As Net.HttpWebResponse = PushDataRequest.GetResponse

                If PushDataResponse.StatusCode = Net.HttpStatusCode.OK Then

                    Dim Reader As New IO.StreamReader(PushDataResponse.GetResponseStream())
                    Dim Result As String = Reader.ReadToEnd

                    Reader.Close()
                    Reader.Dispose()

                    Result = Result.Substring(Result.IndexOf("-"))
                    Result = Result.Substring(0, Result.IndexOf(""""))

                    Return Result

                Else

                    Return Nothing

                End If

            Catch ex As Exception

                Return Nothing

            End Try

        End Function

        Public Function UpdateData(ByVal NodePath As String, ByVal JsonData As String) As UpdateDataResult

            ' Update the data at a particular point in the Firebase.

            Try

                Dim UpdateDataRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(FirebaseUrl & NodePath & ".json?auth=" & FirebaseSecret)

                With UpdateDataRequest
                    .Method = "PATCH"
                    Dim Writer As New IO.StreamWriter(.GetRequestStream)
                    Writer.Write(JsonData)
                    Writer.Close()
                    Writer.Dispose()
                End With

                Dim UpdateDataResponse As Net.HttpWebResponse = UpdateDataRequest.GetResponse

                If UpdateDataResponse.StatusCode = Net.HttpStatusCode.OK Then
                    Return UpdateDataResult.DataSuccessfullyUpdated
                Else
                    Return UpdateDataResult.DataUpdateFailed
                End If

            Catch ex As Exception

                Return UpdateDataResult.DataUpdateFailed

            End Try

        End Function

        Public Function DeleteData(ByVal NodePath As String) As DeleteDataResult

            ' Delete some data.

            Try

                Dim DeleteDataRequest As Net.HttpWebRequest = Net.HttpWebRequest.Create(FirebaseUrl & NodePath & ".json?auth=" & FirebaseSecret)

                With DeleteDataRequest
                    .Method = "DELETE"
                End With

                Dim DeleteDataResponse As Net.HttpWebResponse = DeleteDataRequest.GetResponse

                If DeleteDataResponse.StatusCode = Net.HttpStatusCode.OK Then
                    Return DeleteDataResult.DataDeletedSuccessfully
                Else
                    Return DeleteDataResult.DataDeletionFailed
                End If

            Catch ex As Exception

                Return DeleteDataResult.DataDeletionFailed

            End Try

        End Function

    End Class

End Namespace


